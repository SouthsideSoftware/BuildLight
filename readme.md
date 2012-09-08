Framework for connecting a build server status to a build light/lamp, audible notification and other sorts of things like mobile alerts.  Right now it only supports TeamCity and a red/green/blue LED light made by Delcom that connects to the USB port and is supported on Mac, Windows and Linux.  See below for more information.

Pretty soon you will be able to create your own plugins to provide other sorts of notification.

We will probably also add support for CruiseControl.Net and Jenkins.

##What we're working on

See our [Trello Board](https://trello.com/board/team-city-build-light/504b4a2e72e2d9db2e3ede6e) to see what we have in the pipeline.  You can also vote on features there.  Send your suggestions, questions and bugs to [teamcitybuildlight@southsidesoft.com](mailto://teamcitybuildlight@southsidesoft.com)


##Prerequisites

* Visual Studio 2012
* [Nuget 2.0 or Higher](http://www.nuget.org)

##Building

* Double click on clicktobuild.bat or open a command window and type "build<return>".

This project uses Nuget to pull packages and the packages are not placed into the repository.  Therefore, you should build as shown above after a clone and before opening the solution in VS.NET to get the latest packages.  NuGet will check for latest packages whenever you build in VS.NET or at the command line.  

## Configuring

* In Nlog.config, adjust the location of the log file.  Default to a folder in \logs.
* Set TeamCityUser, TeamCityPassword and TeamCityUrl in the configuraton file of the service
* Install the service using TopShelf.  You can perform the default install after building in your bin\debug (or release) directory using default settings:

```
TeamCityBuildLight.Service install
```

Consult the [TopShelf Wiki](https://github.com/Topshelf/Topshelf/wiki/Command-Line) for more configuration options.

## Supported Versions of TeamCity

* 7.x and higher

## Supported Devices

The base project supports the [Delcom USB HID Visual Signal Indicator RGB](http://www.delcomproducts.com/products_USBLMP.asp).  It will indicate status as follows:

* Off when it cannot reach the configured TeamCity server or some other error has occured
* Flashing blue when any configuration is building on the server
* Flashing red when no configuration is building and at least one configuration's most recent build has failed
* Green when no configuration is building and all configuration's most recent builds have succeeeded

