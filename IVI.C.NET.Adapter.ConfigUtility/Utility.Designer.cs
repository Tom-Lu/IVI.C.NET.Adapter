namespace IVI.C.NET.Adapter.ConfigUtility
{
    partial class Utility
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.AdapterConfigList = new System.Windows.Forms.DataGridView();
            this.UpdateConfig = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.SoftwareModule = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CurrentAdapterClass = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NewAdapterClass = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.refreshBtn = new System.Windows.Forms.Button();
            this.saveBtn = new System.Windows.Forms.Button();
            this.exitBtn = new System.Windows.Forms.Button();
            this.autoSetupBtn = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.clearAdapterSetup = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.AdapterConfigList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // AdapterConfigList
            // 
            this.AdapterConfigList.AllowUserToAddRows = false;
            this.AdapterConfigList.AllowUserToDeleteRows = false;
            this.AdapterConfigList.AllowUserToResizeRows = false;
            this.AdapterConfigList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AdapterConfigList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.AdapterConfigList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.UpdateConfig,
            this.SoftwareModule,
            this.CurrentAdapterClass,
            this.NewAdapterClass});
            this.AdapterConfigList.Location = new System.Drawing.Point(12, 12);
            this.AdapterConfigList.MultiSelect = false;
            this.AdapterConfigList.Name = "AdapterConfigList";
            this.AdapterConfigList.RowHeadersVisible = false;
            this.AdapterConfigList.RowTemplate.Height = 23;
            this.AdapterConfigList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.AdapterConfigList.Size = new System.Drawing.Size(650, 429);
            this.AdapterConfigList.TabIndex = 0;
            // 
            // UpdateConfig
            // 
            this.UpdateConfig.Frozen = true;
            this.UpdateConfig.HeaderText = "  ";
            this.UpdateConfig.Name = "UpdateConfig";
            this.UpdateConfig.Width = 30;
            // 
            // SoftwareModule
            // 
            this.SoftwareModule.HeaderText = "Software Module";
            this.SoftwareModule.Name = "SoftwareModule";
            this.SoftwareModule.ReadOnly = true;
            this.SoftwareModule.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.SoftwareModule.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.SoftwareModule.Width = 200;
            // 
            // CurrentAdapterClass
            // 
            this.CurrentAdapterClass.HeaderText = "Current Adapter Class";
            this.CurrentAdapterClass.Name = "CurrentAdapterClass";
            this.CurrentAdapterClass.ReadOnly = true;
            this.CurrentAdapterClass.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.CurrentAdapterClass.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.CurrentAdapterClass.Width = 200;
            // 
            // NewAdapterClass
            // 
            this.NewAdapterClass.HeaderText = "New Adapter Class";
            this.NewAdapterClass.Name = "NewAdapterClass";
            this.NewAdapterClass.Width = 200;
            // 
            // refreshBtn
            // 
            this.refreshBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.refreshBtn.Location = new System.Drawing.Point(425, 447);
            this.refreshBtn.Name = "refreshBtn";
            this.refreshBtn.Size = new System.Drawing.Size(75, 23);
            this.refreshBtn.TabIndex = 1;
            this.refreshBtn.Text = "Refresh";
            this.refreshBtn.UseVisualStyleBackColor = true;
            this.refreshBtn.Click += new System.EventHandler(this.refreshBtn_Click);
            // 
            // saveBtn
            // 
            this.saveBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.saveBtn.Location = new System.Drawing.Point(506, 447);
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.Size = new System.Drawing.Size(75, 23);
            this.saveBtn.TabIndex = 1;
            this.saveBtn.Text = "Save";
            this.saveBtn.UseVisualStyleBackColor = true;
            this.saveBtn.Click += new System.EventHandler(this.saveBtn_Click);
            // 
            // exitBtn
            // 
            this.exitBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.exitBtn.Location = new System.Drawing.Point(587, 447);
            this.exitBtn.Name = "exitBtn";
            this.exitBtn.Size = new System.Drawing.Size(75, 23);
            this.exitBtn.TabIndex = 1;
            this.exitBtn.Text = "Exit";
            this.exitBtn.UseVisualStyleBackColor = true;
            this.exitBtn.Click += new System.EventHandler(this.exitBtn_Click);
            // 
            // autoSetupBtn
            // 
            this.autoSetupBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.autoSetupBtn.Location = new System.Drawing.Point(12, 447);
            this.autoSetupBtn.Name = "autoSetupBtn";
            this.autoSetupBtn.Size = new System.Drawing.Size(131, 23);
            this.autoSetupBtn.TabIndex = 1;
            this.autoSetupBtn.Text = "Auto Setup";
            this.autoSetupBtn.UseVisualStyleBackColor = true;
            this.autoSetupBtn.Click += new System.EventHandler(this.autoSetupBtn_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 12);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(575, 326);
            this.dataGridView1.TabIndex = 0;
            // 
            // clearAdapterSetup
            // 
            this.clearAdapterSetup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.clearAdapterSetup.Location = new System.Drawing.Point(149, 447);
            this.clearAdapterSetup.Name = "clearAdapterSetup";
            this.clearAdapterSetup.Size = new System.Drawing.Size(131, 23);
            this.clearAdapterSetup.TabIndex = 1;
            this.clearAdapterSetup.Text = "Clear Adapter Setup";
            this.clearAdapterSetup.UseVisualStyleBackColor = true;
            this.clearAdapterSetup.Click += new System.EventHandler(this.clearAdapterSetup_Click);
            // 
            // Utility
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(674, 482);
            this.Controls.Add(this.exitBtn);
            this.Controls.Add(this.saveBtn);
            this.Controls.Add(this.clearAdapterSetup);
            this.Controls.Add(this.autoSetupBtn);
            this.Controls.Add(this.refreshBtn);
            this.Controls.Add(this.AdapterConfigList);
            this.Name = "Utility";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "IVI Adapter Config Utility";
            this.Shown += new System.EventHandler(this.Utility_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.AdapterConfigList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView AdapterConfigList;
        private System.Windows.Forms.Button refreshBtn;
        private System.Windows.Forms.Button saveBtn;
        private System.Windows.Forms.Button exitBtn;
        private System.Windows.Forms.Button autoSetupBtn;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn UpdateConfig;
        private System.Windows.Forms.DataGridViewTextBoxColumn SoftwareModule;
        private System.Windows.Forms.DataGridViewTextBoxColumn CurrentAdapterClass;
        private System.Windows.Forms.DataGridViewComboBoxColumn NewAdapterClass;
        private System.Windows.Forms.Button clearAdapterSetup;
    }
}

