Framework for connecting a build server status to a build light/lamp, audible notification and other sorts of things like mobile alerts.  Right now it only supports TeamCity and a red/green/blue LED light made by Delcom that connects to the USB port and is supported on Mac, Windows and Linux.  See below for more information.

Pretty soon you will be able to create your own plugins to provide other sorts of notification.

We will probably also add support for CruiseControl.Net and Jenkins.

##Contact Us

See our [Trello Board](https://trello.com/board/team-city-build-light/504b4a2e72e2d9db2e3ede6e) to see what we have in the pipeline.  You can also vote on features there.  Send your suggestions, questions and bugs to [teamcitybuildlight@southsidesoft.com](mailto://teamcitybuildlight@southsidesoft.com)

## Nuget

You can obtain the latest pre-build version from [Nuget](https://nuget.org/packages/BuildLight)

##Prerequisites

* Visual Studio 2012
* [Nuget 2.0 or Higher](http://www.nuget.org)

##Building

* Open in Visual Studio 2012 and build OR
* Double click on clicktobuild.bat OR
* Open a command window and type "build<return>".

This project uses Nuget to pull packages and the packages are not placed into the repository.  NuGet will check for latest packages whenever you build in VS.NET or at the command line.  

## Configuring and Installing the Service

* In Nlog.config, adjust the location of the log file.  Defaults to a folder in \logs.
* Install the service using the following command line:

```
BuildLight.Service install
```

* Go to the service control manager and start the BuildLight service.

Consult the [TopShelf Wiki](https://github.com/Topshelf/Topshelf/wiki/Command-Line) for more configuration options.

## Supported Versions of TeamCity

* 7.x and higher

## Supported Devices

The base project supports the [Delcom USB HID Visual Signal Indicator RGB](http://www.delcomproducts.com/products_USBLMP.asp).  It will indicate status as follows:

* Off when it cannot reach the configured TeamCity server or some other error has occured
* Flashing blue when any configuration is building on the server
* Flashing red when no configuration is building and at least one configuration's most recent build has failed
* Green when no configuration is building and all configuration's most recent builds have succeeeded

##License

The application and source code are licensed under the [MIT License](http://opensource.org/licenses/MIT):

Copyright (c) 2012 Southside Software, LLC

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

