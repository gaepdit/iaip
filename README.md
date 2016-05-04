# Integrated Air Information Platform (IAIP)

## Application

The IAIP is a Windows Forms Application and currently targets the .NET Framework version 4.5.2. 


## Prerequisites for building

+ [Visual Studio](https://www.visualstudio.com/) 2010 or later

+ [Microsoft .NET Framework 4.5.2 Developer Pack](http://www.microsoft.com/en-us/download/details.aspx?id=42637)

+ [SAP Crystal Reports, developer version for Microsoft Visual Studio](http://scn.sap.com/docs/DOC-7824), Support Pack 15 or later. Be sure to download the Install Executable, not an MSI.

+ Some NuGet packages are required. They should be restored automatically. If not, open the NuGet Package Manager and click Restore to install them:

  - Oracle ODP.NET, Managed Driver
  - Telerik Analytics Monitor


## Setup

* Make sure you have a good `.gitignore` file set up or you will have lots of unnecessary files invading your repository. Use the example in the [Common Libraries repo](https://bitbucket.org/gaepdit/common-libraries/src/master/Git%20settings/.gitignore?fileviewer=file-view-default)

* Before publishing, you will need to [create a Test Certificate](https://msdn.microsoft.com/en-us/library/che5h906%28v=vs.120%29.aspx) for signing the ClickOnce manifest.

* Before you can open any Forms in design view in Visual Studio, you must build the project. (Each form inherits from `BaseForm` instead of from `System.Windows.Forms.Form`. The project has to be built before `BaseForm` is available to the visual designer.)


## Branches

The IAIP repository uses the [git-flow branching model](http://nvie.com/posts/a-successful-git-branching-model/). There are two permanent branches in the repository:

* `master` contains production-ready code. Releases are built from `master` and tagged with the version number.
* `develop` is an integration branch for new feature development. 

There are several transient branch families as well:

* `feature/...` branches are for work on new features. They branch off from `develop` and merge back into `develop` when ready.
* `hotfix/...` branches are for bug fixes on production releases. They branch off from `master` and merge into *both* `master` and `develop` when finished.
* `release/...` branches are for preparing new production releases. They branch off from `develop` and merge into *both* `master` and `develop` when a production release is ready

Production releases (in the `master` branch) are tagged with the version number as `version/vX.X.X`.
