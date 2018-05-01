# Integrated Air Information Platform (IAIP)

## Application

The IAIP is a Windows Forms Application and currently targets the .NET Framework version 4.5.2. 

## Prerequisites for developing

+ [Visual Studio](https://www.visualstudio.com/)

+ [Microsoft .NET Framework 4.5.2 Developer Pack](http://www.microsoft.com/en-us/download/details.aspx?id=42637)

+ [SAP Crystal Reports, developer version for Microsoft Visual Studio SP 20](http://downloads.businessobjects.com/akdlm/cr4vs2010/CRforVS_13_0_20.exe) and [Crystal Reports runtime engine SP 20](http://downloads.businessobjects.com/akdlm/cr4vs2010/CRforVS_redist_install_32bit_13_0_20.zip)
 
+ Some NuGet packages are required. They should be restored automatically. If not, open the NuGet Package Manager and click Restore to install them.

**Important Note:** Crystal Reports SP 21 supports Visual Studio 2017, but SP 21 of the CR runtime engine is *not compatible* with previous versions. Since the IAIP was originally deployed using an earlier version of the runtime, in order to avoid needing to update all users' machines, SP 21 cannot be used. Instead, *install SP 20* and use [Visual Studio 2015](https://my.visualstudio.com/Downloads?pid=1881) when editing Crystal Reports documents.

## Setup

* Before publishing, you will need to [create a Test Certificate](https://msdn.microsoft.com/en-us/library/che5h906%28v=vs.120%29.aspx) for signing the ClickOnce manifest.

* Before you can open any Forms in design view in Visual Studio, you must build the project. (Each form inherits from `BaseForm` instead of from `System.Windows.Forms.Form`. The project has to be built before `BaseForm` is available to the visual designer.)


## Branches

The IAIP repository uses the [git-flow branching model](http://nvie.com/posts/a-successful-git-branching-model/). There are two permanent branches in the repository:

* `master` contains production-ready code. Releases are built from `master` and tagged with the version number.
* `develop` is an integration branch for new feature development. 

There `release` branch is transient:

* `release/...` branches are for preparing new production releases. They branch off from `develop` and merge into *both* `master` and `develop` when a production release is ready

Production releases (in the `master` branch) are tagged with the version number as `version/X.X.X`.
