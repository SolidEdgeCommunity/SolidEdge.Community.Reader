SolidEdge.Community.Reader
================

Library for reading Solid Edge files

## [SolidEdge.Community.Reader NuGet Package](https://www.nuget.org/packages/SolidEdge.Community.Reader)

SolidEdge.Community.Reader.dll is a stand-alone .NET 4.0 assembly that allows you to read Solid Edge files without using Solid Edge COM based APIs. Since Solid Edge files are [compound files](http://msdn.microsoft.com/library/windows/desktop/aa378938.aspx), we can use the [structured storage API](http://msdn.microsoft.com/library/windows/desktop/aa380369.aspx) to read them. The assembly has many features beyond Solid Edge file APIs. For instance, you can read created version, last saved version and extract Draft sheets in EMF format.