IAIP Notes for Developers
=========================

ClickOnce deployment
---

These are/were the steps to follow for moving to a [ClickOnce](http://msdn.microsoft.com/en-us/library/142dbbz4%28v=vs.90%29.aspx) installation process. Issue numbers refer to forked repository at [IAIP ClickOnce](https://bitbucket.org/dougwaldron/iaip-clickonce). Once ClickOnce development is complete, this section will be removed.

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

### Beta program (complete):

+ ✓ Update changelog (#11)
+ ✓ Publish instructions (#1)
+ ✓ Publish 3.0 Release Candidate
+ ✓ Send beta program email (#2)
+ ✓ Check logs

### Deployment (complete):

+ ✓ Update changelog (#16)
+ ✓ Publish 3.0 instructions
+ ✓ Publish 3.0 Final
+ ✓ Send deployment email (#2)

### Post-deployment:

- Update documentation wiki
- Release JohnGaltProject deactivator (#3)


Source Code
-----------

The IAIP currently targets the .NET Framework version 3.5. It is built in Visual Studio 2008.

The repository is on [Bitbucket](https://bitbucket.org/bgregory/iaip-2008).


Prerequisites
-------------

Before you can build the IAIP, you must have some additional tools available in a directory named `common-libraries` parallel to the IAIP solution directory like so:

```text
	⊟ Repositories root directory
    ┃
	┣─⊞ IAIP solution directory
    ┃
	┗─⊟ common-libraries
	   ┣─⊞ Oracle
	   ┗─⊞ Telerik
```

*(It doesn't matter what you name the root directory or the IAIP solution directory.)*

The `common-libraries` repository is on [Bitbucket](https://bitbucket.org/dougwaldron/common-libraries), and the easiest way to aquire it is to use the Clone command on the Bitbucket page.

There are some items in the `common-libraries` directory that are not used by the IAIP. Currently required tools are:

+ Oracle (ODAC xcopy and instantclient)
+ [Telerik -- Analytics](https://platform.telerik.com/#downloads/analytics) (Application monitor)

To build the IAIP Documentation, you must have [Pandoc](http://johnmacfarlane.net/pandoc/installing.html) installed. A script is run as part of the pre-build events.


Branches
--------

There are two main branches in the repository: `default` and `stable`

+ The `stable` branch is for releases
+ The `default` branch is for development of future releases

To get the latest release version, run `hg update stable`.

Other branches may be created for work on new features that are experimental or will take a long time to complete so that these don't intefere with regular releases. These branch names should be prefixed with `feature-`.

Tips
----

+ Before you can open any Winforms in design view, you have to build the project. (Each form inherits from `BaseForm` instead of from `System.Windows.Forms.Form`. The project has to be built before `BaseForm` is available to the visual designer.)
