% Using the Environmental Information Exchange Network to submit EIS data to EPA

## Introduction

The [Exchange Network](https://www.exchangenetwork.net) is a data and services platform for sharing environmental information data between EPA, States, Territories, and Tribes. The [Emissions Inventory System (EIS)](https://www.exchangenetwork.net/data-exchange/emissions-inventory-system/) is EPA’s information system for storing all current and historical emissions inventory data. 

The [Exchange Network Services Center](https://enservices.epa.gov/) (ENSC) is a web-based tool designed to allow Exchange Network users to easily use Exchange Network Services.

Regulated facilities in Georgia report EIS data using the [Georgia Environmental Connections Online (GECO)](https://geco.gaepd.org/) system. Air Protection Branch staff then use the [Integrated Air Information Platform (IAIP)](/) to stage existing data. Finally, staged EIS data is compiled and submitted to EPA using either the [Virtual Exchange Service (VES)](https://vnap.cloudapp.net/vnap/) — a hosted data exchange platform — or the ENSC. 

This tutorial is intended to introduce APB staff to the staging and submittal procedures. The tutorial focuses on using ENSC instead of VES.

## Access Control

New users must request access to both the *EIS Gateway,* which is the web interface for viewing EPA's EIS data, and the *ENSC.*

* To access the EIS Gateway, follow the instructions in *[How Do I Request Access to the EIS Gateway?](https://www.epa.gov/sites/production/files/2016-04/documents/access.pdf)*
    * The first step is to [register for EPA Web Application Access](https://waa.epa.gov) (select "Self Register" link).
    * The registration page asks for your EPA contact, which for Region 4 is:

        > Ahmed Amanulah  
        > 404-562-9209  
        > amanulah.ahmed@epa.gov

* To gain access to ENSC, [contact EPD-IT](mailto:epd_it@dnr.ga.gov?subject=ENSC Access Request).

## Staging Data (IAIP)

* In the IAIP, open the EIS & GECO Tools and select the "EIS Tools" tab. 
* Choose the year you want to work with, and below that select the "Stage to EPA" tab. Select "Display facilities that have submitted EIS data." 
* In the grid on the right, you can select one or more facilities whose data you want to submit to EPA. After selecting the desired facilities, select the buttons to stage Facility and/or Point Source data. 
* Alternatively, you can choose to stage data for all facilities for the selected year.

When you stage data using the IAIP EIS Tools, previous data of the same type is unstaged. For example, if you stage Facility data, any Facility data previously staged is no longer available to submit. 

![The IAIP EIS Tool](img/IAIP-EIS-Tool.png)

## Submitting data (ENSC)

* [Log into the ENSC](https://enservices.epa.gov/) and select "My Services Center". If you have used the ENSC before, this page will display services you have previously used. To access new services, select "Use a New Service".

![The ENSC Services Center](img/ENSC-Services-Center.png)

* In the "Filter By" tool, select "Node," then "GeorgiaProd" and click Filter.

![ENSC: Find Services](img/ENSC-Find-Services.png)

* There are four EIS services available. "GetEISFacInventory" and "GetEISPointEmission" allow you to view staged data, while "SubmitEISFacInventory" and "SubmitEISPointEmission" will submit data to EPA.
* Whether you are using a Get or Submit service, you *must* provide the emissions inventory year in the "EMISYear" textbox. You may optionally also provide a facility site ID (AIRS number) if you wish to only view/submit data for one facility. Regardless of which service you choose, the data for the year/facility you enter *must be already staged from the IAIP* or no data will be submitted.

![ENSC: Run a Service](img/ENSC-Run-Service.png)

* If you select one of the "Get Info" services, you can choose to view the data as a web page or a downloadable XML file.
* When you use one of the "Execute" (Submit) services, the submittal process is triggered, and you will shortly receive an email with the Transaction ID. The email will contain a link to the Exchange Network Web Client where you can find more information on the transaction, including a downloadable XML file of the data submitted, and an XML file documenting the XML validation results. Log into the Web Client with the same username and password as the ENSC.

![Exchange Network Web Client](img/Exchange-Network-Web-Client.png)

## Reviewing data/results (EIS Gateway)

* [Log into the EIS Gateway](https://eis.epa.gov/eis-system-web/) and select "Feedback Reports" in the Reports menu.
* The search results will show you the status and type of each transaction.
* Select "Download Report" for the desired transaction to download an Excel file with detailed statistics, errors, and warnings.

![EIS Gateway Transaction History](img/EIS-Gateway-Transaction-History.png)
