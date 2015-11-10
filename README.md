IAIP Notes for Developers
=========================


Source Code
-----------

The IAIP currently targets the .NET Framework version 4.5.2. It is built in Visual Studio 2013 (but other versions may work).

The repository is on [Bitbucket](https://bitbucket.org/bgregory/iaip).


Prerequisites
-------------

+ Visual Studio 2013

+ [Microsoft .NET Framework 4.5.2 Developer Pack](http://www.microsoft.com/en-us/download/details.aspx?id=42637)

+ [SAP Crystal Reports, developer version for Microsoft Visual Studio](http://scn.sap.com/docs/DOC-7824). Be sure to download the Install Executable, not the MSI. I'm currently using v.13.0.15.

+ Some NuGet packages are required. They should be restored automatically. If not, open the NuGet Package Manager and click Restore to install them:
  - Oracle ODP.NET, Managed Driver
  - Telerik Analytics Monitor


Setup
-----

* Make sure you have a good hgignore file set up or you will have lots of unnecessary and temporary files invading your repository. Use the example in the [Common Libraries repo](https://bitbucket.org/dougwaldron/common-libraries/src/default/Mercurial%20settings/hgignore.ini?fileviewer=file-view-default)

* Before publishing, you will need to [create a Test Certificate](https://msdn.microsoft.com/en-us/library/che5h906%28v=vs.120%29.aspx) for signing the ClickOnce manifest. Recommended: Use a single space character as the password.


Branches
--------

There are two main branches in the repository: `default` and `stable`.

+ The `stable` branch is for releases
+ The `default` branch is for development of future releases

To get the latest release version, run `hg update stable`. To get the latest development version, run `hg update default`. 

Releases are tagged with their unique version number. Use `hg tags` to see a list of existing tags.

Other branches may be created for work on bugs or new features that are experimental or will take a long time to complete so that these don't intefere with routine or bugfix releases. Feature branch names should be prefixed with `feat/` followed by a short descriptive name, e.g., `feat/new-permit-search`. Bug fix branches should be prefixed `bug/` followed by the issue number and an optional brief description, e.g., `bug/299-permit-search-error`.


Tips
----

+ Before you can open any Forms in design view, you have to build the project. (Each form inherits from `BaseForm` instead of from `System.Windows.Forms.Form`. The project has to be built before `BaseForm` is available to the visual designer.)

+ You can set up automatic Bitbucket links within TortoiseHg Workbench by adding these lines to your .hg\hgrc file:

```ini
[tortoisehg]
issue.regex = #(\d+)\b
issue.link = https://bitbucket.org/bgregory/iaip/issue/{1}
changeset.link = https://bitbucket.org/bgregory/iaip/commits/{node|short}
```
