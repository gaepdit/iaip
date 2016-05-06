# Integrated Air Information Platform (IAIP)

## SQL Server Fork

This repository is a fork of the IAIP for the purpose of migrating the database back-end from Oracle to SQL Server. All of the original information below still applies, with the following additions to the branching model:

* `ss-develop`: All database migration work will be done on the `ss-develop` branch.
* `ss-feature/...`: Feature/work-in-progress branches off of `ss-develop`.
* `ss-release/...`: Branches for preparation of UAT releases.
* `ss-master`: UAT release branches off of `ss-develop`. Published releases should be tagged with `ss-version/...`. (See versioning section below.)

Work done on production IAIP will routinely be pulled into this fork by merging from `iaip/develop` into `ss-develop`.

### Versioning

Version numbering of UAT releases from the SQL Server fork is completely independent of production IAIP versioning. UAT releases will be numbered "0.1", "0.2", etc. 

At the completion of the project, all changes will be merged back into the production IAIP repository and the released as IAIP 5.0.

---

## Application

The IAIP is a Windows Forms Application and currently targets the .NET Framework version 4.5.2. 


## Prerequisites for building

+ [Visual Studio](https://www.visualstudio.com/) 2015 or later

+ [Microsoft .NET Framework 4.5.2 Developer Pack](http://www.microsoft.com/en-us/download/details.aspx?id=42637)

+ [SAP Crystal Reports, developer version for Microsoft Visual Studio](http://scn.sap.com/docs/DOC-7824), Support Pack 15 or later. Be sure to download the Install Executable, not an MSI.

+ Some NuGet packages are required. They should be restored automatically. If not, open the NuGet Package Manager and click Restore to install them:
    - Oracle ODP.NET, Managed Driver
    - Telerik Analytics Monitor


## Setup

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

Production releases (in the `master` branch) are tagged with the version number as `version/X.X.X`.