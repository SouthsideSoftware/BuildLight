Please update this document when you make changes that impact the build, prerequisites for development or use of this repository.

## Getting the Latest

This project uses tools contained submodule.  Either clone recursively or initialize all submodule after a clone.  You should update submodule whenever you pull a revision with "[SUBMODULE UPDATE]" in the comment.

````dos
git clone git@github.com:SouthsideSoftware/TeamCityBuildLight.git [path] --recursive
````

##Prerequisites

* Visual Studio 2010
* [Nuget 1.6 or Higher](http://www.nuget.org)

These items can all be installed using [Microsoft's Web Platform Installer](http://www.microsoft.com/web/downloads/platform.aspx):

* Visual Studio 2010 SP1

##Building

* Double click on clicktobuild.bat or open a command window and type "build<return>".

This project uses Nuget to pull packages and the packages are not placed into the repository.  Therefore, you should build as shown above after a clone and before opening the solution in VS.NET to get the latest packages.  NuGet will check for latest packages whenever you build in VS.NET or at the command line.  

## Supported Devices

The base project supports the [Delcom USB HID Visual Signal Indicator RGB](http://www.delcomproducts.com/products_USBLMP.asp).  It will show blue when a build is in progress, red when one or more configuration have failed their most recent build and green when no build configurations are in the failed state.