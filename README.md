IAIP Notes for Developers
=========================

ClickOnce Fork

---

This is a special fork to prepare for moving to a [ClickOnce](http://msdn.microsoft.com/en-us/library/142dbbz4%28v=vs.90%29.aspx) installation process.

### Pre-deployment (complete):

+ ✓ Remove hard-coded file dependencies
+ ✓ Remove installer project
+ ✓ Change project name
+ ✓ Add code in new project to remove old program (shortcuts only)
+ ✓ Remove version checking code
+ ✓ Review CPU settings
+ ✓ Review ClickOnce settings (prerequisites, etc.)
+ ✓ Review update frequency
+ ✓ Implement on-demand updater
+ ✓ Rebuild newer Crystal Reports
+ ✓ Draft emails
+ ✓ Send heads-up email
+ ✓ Create installation website
+ ✓ Create dummy replacement for JohnGaltProject to inform users of new IAIP installation (with link to installation website)

### Beta program:

+ ✓ Update changelog (#11)
+ ✓ Publish instructions (#1)
+ ✓ Publish 3.0 Release Candidate
+ ✓ Send beta program email (#2)

- Check logs

### Deployment:

- Update changelog (#16)
- Republish instructions if modified
- Publish 3.0 Final
- Release JohnGaltProject deactivator (#3)
- Send deployment email (#2)

### Post-deployment:

- Update documentation wiki

Original IAIP Notes for Developers
=========================

Source Code
-----------

The IAIP currently targets the .NET Framework version 3.5. It is built in Visual Studio 2008.

The repository is on [BitBucket](https://bitbucket.org/bgregory/iaip-2008).


Prerequisites
-------------

Before you can build the IAIP, you must have several additional tools available in a directory named `_Common_Libraries` parallel to the IAIP solution directory like so:

```text
	⊟ Repositories root directory
    ┃
	┣─⊞ IAIP solution
    ┃
	┣─⊟ _Build
	┃ ┗─⊞ IAIP
    ┃
	┗─⊟ _Common_Libraries
	   ┣─⊞ 7za
	   ┣─⊞ 7zsd
	   ┣─⊞ Eqatec
	   ┗─⊞ Oracle
```

*(It doesn't matter what you name the root directory or the IAIP solution directory.)*

The `_Common_Libraries` repository is also on [BitBucket](https://bitbucket.org/dougwaldron/tools-for-vs-and-other-projects).

There are some items in the `_Common_Libraries` directory that are not used by the IAIP. Currently required tools are:

+ [7za](http://sourceforge.net/projects/sevenzip/files/7-Zip/9.20/) (7-zip command line, version 9.20)
+ [7zSD extra](http://7zsfx.info/en/download.html) (Modified SFX module, 1.5 Release)
+ Oracle (ODAC xcopy and instantclient)
+ [Telerik Analytics](http://www.telerik.com/analytics/download/) (Application monitor, version 3.2.61)

To build the readme and changelog docs, you must have [Pandoc](http://johnmacfarlane.net/pandoc/) installed. This script is run as part of the post-build events for Release builds. **Important: You must change the path in the post-build events to point to your installation of Pandoc.**

Build
-----

The Release build outputs to a directory named `_Build` parallel to the IAIP directory (see diagram above). The Debug build outputs to the `bin` folder in the IAIP solution directory.

### Branches

There are two main branches in the repository: `default` and `stable`

+ The `stable` branch is for releases
+ The `default` branch is for development of future releases

To get the latest release version, run `hg update stable` and then build.

Other branches may be created for work on new features that are experimental or will take a long time to complete so that these don't intefere with regular releases. These branch names should be prefixed with `feature-`.

Tips
----

+ Before you can edit any forms, you have to build the project. (Each form inherits from `BaseForm` instead of from `System.Windows.Forms.Form`. The project has to be built before `BaseForm` is available to the visual designer.)
