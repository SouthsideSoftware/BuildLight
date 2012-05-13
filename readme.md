Please update this document when you make changes that impact the build, prerequisites for development or use of this repository.

##Prerequisites

* Visual Studio 2010
* [Nuget 1.6 or Higher](http://www.nuget.org)

These items can all be installed using [Microsoft's Web Platform Installer](http://www.microsoft.com/web/downloads/platform.aspx):

* Visual Studio 2010 SP1

##Building

* Double click on clicktobuild.bat or open a command window and type "build<return>".

This project uses Nuget to pull packages and the packages are not placed into the repository.  Therefore, you should build as shown above after a clone and before opening the solution in VS.NET to get the latest packages.  NuGet will check for latest packages whenever you build in VS.NET or at the command line.  

## Supported Versions of TeamCity

* 7.x and higher

## Supported Devices

The base project supports the [Delcom USB HID Visual Signal Indicator RGB](http://www.delcomproducts.com/products_USBLMP.asp).  It will indicate status as follows:

* Off when it cannot reach the configured TeamCity server or some other error has occured
* Flashing blue when any configuration is building on the server
* Flashing red when no configuration is building and at least one configuration's most recent build has failed
* Green when no configuration is building and all configuration's most recent builds have succeeeded

