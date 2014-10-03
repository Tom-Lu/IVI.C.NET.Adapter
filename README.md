# IVI.C.NET.Adapter

IVI.C.NET.Adapter is an .NET assembly support using IVI-C driver from .NET application 
without interop with individual driver DLL. 

[![Class Diagram](https://github.com/Tom-Lu/IVI.C.NET.Adapter/blob/master/UML/Classdiagram.png)](https://github.com/Tom-Lu/IVI.C.NET.Adapter/blob/master/UML/Classdiagram.png)

Currently following Ivi C driver been supported:

		- IviCounter
		- IviDCPwr
		- IviDigitizer
		- IviDmm
		- IviDownconverter
		- IviFgen
		- IviPwrMeter
		- IviRFSigGen
		- IviScope
		- IviSpecAn
		- IviSwtch
		- IviUpconverter

Due to no definition about `IviACPwr` in IVI.NET Shared Components yet. So `IviACPwr` driver is
not supported in current release.

## Getting Started Guide
[Getting Started Guide](https://github.com/Tom-Lu/IVI.C.NET.Adapter/wiki/Getting-Started-Guide)

## Development Environment Set up
1. Install IVI Shared Components
2. Install IVI.NET Shared Components
3. Build Solution with Visual Studio

[IVI Shared Components](http://www.ivifoundation.org/shared_components/Default.aspx)

## Test
In order to successful run the tests with Nunit, you need install several Ivi drivers from 
Keysight(formerly Agilent). Those driver can be download from [Keysight](http://www.keysight.com/main/facet.jspx?t=80126.k.3&lc=chi&sm=g)
or [National Instruments](http://www.ni.com/downloads/instrument-drivers).

		- Agilent Ag34401
		- Agilent AgE36xx
		- Agilent ag5313xni
		- Agilent agl453xdni
		- Agilent ag1000ni
		- Agilent agpsa
		- Agilent age1442a

## Ivi Versions
- IVI Shared Components version 2.2.1 or greater.
- IVI.NET Shared Components version 2.2.1 or greater.

## NOTICE: 
IVI.C.NET.Adapter only verified with few physical instruments, the concept seems working very well.
Use at your own risk, the author will not response for any cost because of using IVI.C.NET.Adapter.