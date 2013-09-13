IAIP Notes for Developers
======================


Source Code
----------

The IAIP currently targets the .NET Framework version 3.5. It is built in Visual Studio 2008 (though I have a goal of switching to VS 2010 by the end of the year!).

The repository is on [BitBucket](https://bitbucket.org/bgregory/iaip-2008).


Prerequisites
------------

Before you can build the IAIP, you must have several additional tools available in a directory named `_Tools` parallel to the root IAIP directory like so:

```t
	⊟ Project root directory
	┣⊞ IAIP solution
	┗⊟ _Tools
	  ┣⊞ 7za
	  ┣⊞ 7zsd
	  ┣⊞ Eqatec
	  ┗⊞ Oracle
```

_(It doesn't matter what you name the root directory or the IAIP solution directory.)_

The `_Tools` repository is also on [BitBucket](https://bitbucket.org/dougwaldron/tools-for-vs-projects).

Currently used tools are:

* [7za](http://sourceforge.net/projects/sevenzip/files/7-Zip/9.20/) (7-zip command line, version 9.20)
* [7zSD extra](http://7zsfx.info/en/download.html) (Modified SFX module, 1.5 Release)
* Oracle (ODAC xcopy and instantclient)
* [Eqatec](http://www.telerik.com/analytics/download/) (Application monitor, version 3.2.1)
