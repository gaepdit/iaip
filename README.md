IAIP Notes for Developers
=========================

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

To get the latest release version, run `hg update stable`. To get the latest development version, run `hg update default`.

Other branches may be created for work on bugs or new features that are experimental or will take a long time to complete so that these don't intefere with regular releases. Feature branch names should be prefixed with `feat/` followed by a short descriptive name, e.g., `feat/new-permit-search`. Bug fix branches should be prefixed `bug/` followed by the issue number and an optional brief description, e.g., `bug/299-permit-search-error`.

Tips
----

+ Before you can open any Winforms in design view, you have to build the project. (Each form inherits from `BaseForm` instead of from `System.Windows.Forms.Form`. The project has to be built before `BaseForm` is available to the visual designer.)

+ You can set up automatic Bitbucket links within TortoiseHg Workbench by adding this to your hgrc file:

```ini
[tortoisehg]
issue.regex = #(\d+)\b
issue.link = https://bitbucket.org/bgregory/iaip/issue/{1}
changeset.link = https://bitbucket.org/bgregory/iaip/commits/{node|short}
```
