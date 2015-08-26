//--------------------------------------------------------------------------------------------------
//
//   Copyright Tom Lu <luqiang1983@gmail.com>
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
//
//       http://www.apache.org/licenses/LICENSE-2.0
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License.
//
//--------------------------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Ivi.ConfigServer.Interop;


namespace IVI.C.NET.Adapter.ConfigUtility
{
    public partial class Utility : Form
    {
        private IVIHandler IviHandler = IVIHandler.Instance;
        private string[] IviCAdapterList = new string[] {"IviACPwrAdapter", "IviCounterAdapter", "IviDCPwrAdapter", "IviDigitizerAdapter", "IviDmmAdapter", 
                                                      "IviDownconverterAdapter", "IviFgenAdapter", "IviPwrMeterAdapter", "IviRFSigGenAdapter", "IviScopeAdapter",
                                                      "IviSpecAnAdapter", "IviSwtchAdapter", "IviUpconverterAdapter" };

        private string[] PublishedAPIList = new string[] {"IviACPwr", "IviCounter", "IviDCPwr", "IviDigitizer", "IviDmm", 
                                                      "IviDownconverter", "IviFgen", "IviPwrMeter", "IviRFSigGen", "IviScope",
                                                      "IviSpecAn", "IviSwtch", "IviUpconverter" };

        private const string AssemblyQualifiedClassNameTemplatePfx = "IVI.C.NET.Adapter.{0}, IVI.C.NET.Adapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=55d3badc1a673a0b";
        private const string AssemblyQualifiedClassNameTemplateSnk = "IVI.C.NET.Adapter.{0}, IVI.C.NET.Adapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=d25ace325c488ee2";
        private string AssemblyQualifiedClassNameTemplateToUse = null;

        public Utility()
        {
            InitializeComponent();
        }

        private void Utility_Shown(object sender, EventArgs e)
        {
            if (Type.GetType(string.Format(AssemblyQualifiedClassNameTemplateSnk, "IviDmmAdapter"), false) != null)
                AssemblyQualifiedClassNameTemplateToUse = AssemblyQualifiedClassNameTemplateSnk;
            else if (Type.GetType(string.Format(AssemblyQualifiedClassNameTemplatePfx, "IviDmmAdapter"), false) != null)
                AssemblyQualifiedClassNameTemplateToUse = AssemblyQualifiedClassNameTemplatePfx;
            else AssemblyQualifiedClassNameTemplateToUse = null;

            if (String.IsNullOrEmpty(AssemblyQualifiedClassNameTemplateToUse))
            {
                MessageBox.Show("IVI.C.NET.Adapter assembly not installed in GAC.\r\nPlease try reinstall IVI.C.NET.Adapter to fix this issue!", "Assembly not in GAC", MessageBoxButtons.OK);
                Close();
            }
            else
            {
                UpdateDisplay();
                autoSetupBtn_Click(null, null);
            }
        }

        private void UpdateDisplay()
        {
            IVIHandler.Reset();
            IviHandler = IVIHandler.Instance;
            IviHandler.IviConfigStore.Deserialize(IviHandler.IviConfigStore.MasterLocation);

            AdapterConfigList.Rows.Clear();
            foreach (IIviSoftwareModule2 SoftwareModule in IviHandler.IviConfigStore.SoftwareModules)
            {
                if (!SoftwareModule.Name.StartsWith("nis"))
                {
                    DataGridViewRow Row = new DataGridViewRow();
                    DataGridViewCheckBoxCell UpdateCheckBox = new DataGridViewCheckBoxCell();
                    DataGridViewTextBoxCell SoftwareModuleTextBox = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell CurrentAdapterClassTextBox = new DataGridViewTextBoxCell();
                    DataGridViewComboBoxCell NewAdapterClassComboBox = new DataGridViewComboBoxCell();

                    UpdateCheckBox.Value = false;
                    SoftwareModuleTextBox.Value = SoftwareModule.Name;
                    string className = SoftwareModule.AssemblyQualifiedClassName;

                    if (!className.Equals(string.Empty))
                    {
                        Type type = Type.GetType(SoftwareModule.AssemblyQualifiedClassName);
                        if (type != null)
                        {
                            CurrentAdapterClassTextBox.Value = type.Name;
                        }
                    }

                    NewAdapterClassComboBox.Items.Add(string.Empty);
                    NewAdapterClassComboBox.Items.AddRange(IviCAdapterList);
                    NewAdapterClassComboBox.Value = NewAdapterClassComboBox.Items[0];

                    Row.Cells.Add(UpdateCheckBox);
                    Row.Cells.Add(SoftwareModuleTextBox);
                    Row.Cells.Add(CurrentAdapterClassTextBox);
                    Row.Cells.Add(NewAdapterClassComboBox);

                    AdapterConfigList.Rows.Add(Row);
                }
            }
        }

        private void refreshBtn_Click(object sender, EventArgs e)
        {
            UpdateDisplay();
        }

        private void autoSetupBtn_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow Row in AdapterConfigList.Rows)
            {
                string SoftwareModuleName = (string)Row.Cells[1].Value;
                IIviSoftwareModule2 SoftwareModule = (IIviSoftwareModule2)IviHandler.GetSoftwareModule(SoftwareModuleName);
                string AdapterClass = GetSuitableAdapter(SoftwareModule.PublishedAPIs);
                if (AdapterClass != null && !AdapterClass.Equals(Row.Cells[2].Value))
                {
                    Row.Cells[0].Value = true;
                    Row.Cells[3].Value = AdapterClass;
                }
                else
                {
                    Row.Cells[0].Value = false;
                    Row.Cells[3].Value = string.Empty;
                }
            }
        }

        private string GetSuitableAdapter(IIviPublishedAPICollection PublishedAPIs)
        {
            ArrayList PubAPIList = new ArrayList(PublishedAPIList);
            foreach (IIviPublishedAPI PublishedAPI in PublishedAPIs)
            {
                if (PublishedAPI.Type.Equals("IVI-C") && PubAPIList.Contains(PublishedAPI.Name))
                {
                    return IviCAdapterList[PubAPIList.IndexOf(PublishedAPI.Name)];
                }
            }
            return null;
        }

        private void clearAdapterSetup_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow Row in AdapterConfigList.Rows)
            {
                if (Row.Cells[2].Value != null && !Row.Cells[2].Value.Equals(string.Empty))
                {
                    Row.Cells[0].Value = true;
                    Row.Cells[3].Value = string.Empty;
                }
                else
                {
                    Row.Cells[0].Value = false;
                    Row.Cells[3].Value = string.Empty;
                }
            }
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow Row in AdapterConfigList.Rows)
            {
                if (Row.Cells[0].Value != null && (bool)Row.Cells[0].Value)
                {
                    IIviSoftwareModule2 SoftwareModule = (IIviSoftwareModule2)IviHandler.GetSoftwareModule((string)Row.Cells[1].Value);
                    if (SoftwareModule != null)
                    {
                        if (Row.Cells[3].Value == null || Row.Cells[3].Value.Equals(string.Empty))
                        {
                            SoftwareModule.AssemblyQualifiedClassName = string.Empty;
                        }
                        else
                        {
                            SoftwareModule.AssemblyQualifiedClassName = string.Format(AssemblyQualifiedClassNameTemplateToUse, Row.Cells[3].Value);
                        }
                    }
                }
            }

            IviHandler.IviConfigStore.Serialize(IviHandler.IviConfigStore.MasterLocation);

            UpdateDisplay();
        }

        private void exitBtn_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
