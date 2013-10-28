IAIP Notes for Developers
=========================


Source Code
-----------

The IAIP currently targets the .NET Framework version 3.5. It is built in Visual Studio 2008 (though I have a goal of switching to VS 2010 by the end of the year!).

The repository is on [BitBucket](https://bitbucket.org/bgregory/iaip-2008).


Prerequisites
-------------

Before you can build the IAIP, you must have several additional tools available in a directory named `_Tools` parallel to the IAIP solution directory like so:

```t
	⊟ Repositories root directory
    ┃
	┣─⊞ IAIP solution
    ┃
	┣─⊟ _Build
	┃ ┗─⊞ IAIP
    ┃
	┗─⊟ _Tools
	   ┣─⊞ 7za
	   ┣─⊞ 7zsd
	   ┣─⊞ Eqatec
	   ┗─⊞ Oracle
```

*(It doesn't matter what you name the root directory or the IAIP solution directory.)*

The `_Tools` repository is also on [BitBucket](https://bitbucket.org/dougwaldron/tools-for-vs-projects).

There are some items in the `_Tools` directory that are not used by the IAIP. Currently required tools are:

+ [7za](http://sourceforge.net/projects/sevenzip/files/7-Zip/9.20/) (7-zip command line, version 9.20)
+ [7zSD extra](http://7zsfx.info/en/download.html) (Modified SFX module, 1.5 Release)
+ Oracle (ODAC xcopy and instantclient)
+ [Eqatec](http://www.telerik.com/analytics/download/) (Application monitor, version 3.2.1)

To build the readme and changelog docs, you must have [Pandoc](http://johnmacfarlane.net/pandoc/) installed. This script is run as part of the post-build events for Release builds. **Important: You must change the path in the post-build events to point to your installation of Pandoc.**

Build
-----

The Release build outputs to a directory named `_Build` parallel to the IAIP directory (see diagram above). The Debug build outputs to the `bin` folder in the IAIP solution directory.

Tips
----

+ Before you can edit any forms, you have to build the project. (Each form inherits from `BaseForm` instead of from `System.Windows.Forms.Form`. The project has to be built before `BaseForm` is available to the visual designer.)
