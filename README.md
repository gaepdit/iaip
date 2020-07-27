# Integrated Air Information Platform

The Integrated Air Information Platform ("IAIP" or "the Platform") was built beginning in 2004 to collect and organize all the data required to operate an efficient air quality program. It is currently used by several programs in the Air Protection Branch and the District Offices.

The public interacts with the same database through the GECO web application. 
Some data from the Platform are also fed to EPA's ICIS-Air and EIS databases.

## Application

The IAIP is a WinForms Application targeting the .NET Framework version 4.5.2. Users must [install the application](https://iaip.gaepd.org/) locally and connect to the [DNR VPN](https://sslx.gta.ga.gov/dnr/) in order to use it.

## Prerequisites for developing

+ [Visual Studio](https://www.visualstudio.com/) (any recent version)

+ [Visual Studio 2015](https://my.visualstudio.com/Downloads?pid=1881) (if modifying any Crystal Reports)

+ [Microsoft .NET Framework 4.5.2 Developer Pack](http://www.microsoft.com/en-us/download/details.aspx?id=42637)

+ [SAP Crystal Reports, developer version for Microsoft Visual Studio SP 20](http://downloads.businessobjects.com/akdlm/cr4vs2010/CRforVS_13_0_20.exe) and [Crystal Reports runtime engine SP 20](http://downloads.businessobjects.com/akdlm/cr4vs2010/CRforVS_redist_install_32bit_13_0_20.zip)

**Important Note:** Crystal Reports SP 21 supports Visual Studio 2017, but SP 21 of the CR runtime engine is *not compatible* with previous versions. Since the IAIP was originally deployed using an earlier version of the runtime, in order to avoid needing to update all users' machines, SP 21 cannot be used. Instead, *install SP 20* and use [Visual Studio 2015](https://my.visualstudio.com/Downloads?pid=1881) when editing Crystal Reports documents.

## Setup

* Restore all NuGet packages.

* You must build the solution before you can open any forms in design view in Visual Studio. (Each form inherits from a custom `BaseForm` which is only available after building the project.)

* Before publishing, you will need to [create a Test Certificate](https://msdn.microsoft.com/en-us/library/che5h906%28v=vs.120%29.aspx) for signing the ClickOnce manifest. 

    **Warning**: This will change the value of the `<ManifestCertificateThumbprint>` element in IAIP.vbproj. *Do not* commit this change to the repository. Since the value will differ for all developers, the result would be every developer constantly needing to create new test certificates.

## Branches

The IAIP repository uses the [git-flow branching model](http://nvie.com/posts/a-successful-git-branching-model/). There are two permanent branches in the repository:

* `main` contains production-ready code. Releases are built from `main` and tagged with the version number using the format `version5/5.X.X`.
* `develop` is an integration branch for new feature development.

## Release instructions

See the [Release Instructions](docs/Release-Instructions.md) file in the "docs" folder.

## Related projects

Several projects revolve around the "airbranch" database and work together:

* [GECO](https://bitbucket.org/gaepdit/geco/): a web application for use by the regulated community.
* [Airbranch DB](https://bitbucket.org/gaepdit/airbranch-db/): Tracks development of the `airbranch` database used by IAIP, GECO, etc.
* Data Exchanges: Database code used to exchange data with EPA and Enfotech.
    * [ICIS-Air Data Exchange](https://bitbucket.org/gaepdit/icis-air-data-exchange/)
    * [EIS Data Exchange](https://bitbucket.org/gaepdit/eis-data-exchange/)
    * [GEOS/FIS/FIMS Data Exchange](https://bitbucket.org/gaepdit/geos-fis-fims-data-exchange/)
* [permitsearch.gaepd.org](https://bitbucket.org/gaepdit/permitsearch.gaepd.org/): Website for searching for issued Air Quality Permits.
* [gatv.gaepd.org](https://bitbucket.org/gaepdit/gatv.gaepd.org/): Website providing read-only access to old Title V application data (does not interact with data from any other application).
* [IAIP Website](https://bitbucket.org/gaepdit/iaip-website/): A static website where the IAIP installation files are stored along with a changelog and some other documentation.
* [IAIP Framework](https://bitbucket.org/gaepdit/iaip-framework/): An installer that ensures required prerequisites are available prior to installing the IAIP.
* [DB Helper](https://bitbucket.org/gaepdit/db-helper/): A library to consolidate and simplify database interactions (included in IAIP as a NuGet package).
