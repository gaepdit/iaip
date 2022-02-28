# Integrated Air Information Platform

The Integrated Air Information Platform ("IAIP" or "the Platform") was built beginning in 2004 to collect and organize the data required to operate an efficient air quality program for the State of Georgia Environmental Protection Division. It is currently used by several programs in the Air Protection Branch and the District Offices.

Users must [install the application](https://iaip.gaepd.org/) locally and connect to the state network in order to use it.

There are several associated repositories that interact with this project:

* Data is stored in the [airbranch database](https://github.com/gaepdit/airbranch-db).
* The IAIP uses the [DB Helper](https://github.com/gaepdit/db-helper) NuGet package to handle all database interaction.
* [GECO](https://github.com/gaepdit/geco) is a web application used by regulated facilities to view and submit data.
* The general public can search for air quality permits using the [Permit Search engine](https://github.com/gaepdit/permit-search).
* Various IAIP reports are generated and displayed by a separate [reporting web application](https://github.com/gaepdit/airbranch-reports).
* Some data from the Platform are transmitted to EPA's [ICIS-Air data exchange]( https://github.com/gaepdit/icis-air-data-exchange).
* Some data are received from the [GEOS data exchange](https://github.com/gaepdit/geos-fis-fims-data-exchange).
* The [IAIP website](https://github.com/gaepdit/iaip/iaip-website) hosts the installation files and some documentation.

## Development Setup

The IAIP is a VB.NET Winforms Application targeting .NET Framework version 4.8. Open the solution in Visual Studio, restore Nuget packages, and build.

You must build the solution before you can open any Windows Forms in design view in Visual Studio. (Each form inherits from a custom `BaseForm` which is only available after building the project.)

### Branching

* The `main` branch should always be deployable. When starting new work, create a new branch off the latest commit on `main`. Push your changes when ready and create a pull request.
* There are also two utility branches, `deploy/DEV` and `deploy/UAT`, that are only used for deploying QA and UAT versions of the IAIP. 

### Release instructions

See the [Release Instructions](docs/Release-Instructions.md) file in the "docs" folder.
