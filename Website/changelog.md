% IAIP Change Log

## Version 3.7.3 <span>(2015-05-21)</span>

+ NEW: *SSPP* -- App Log can now search for permit applications with confidential info (#296); yw Eric

- FIX: *ISMP* -- Clearly identify test reports that have been deleted (#342)
- FIX: Error when closing a facility (#344); thx Sean
- FIX: *EDT* -- EDT Tools now available to SSPP PM2 (#343)
- FIX: *SSPP* -- Better formatting of list in Title V Renewal letter tool

## Version 3.7.2 <span>(2015-05-06)</span>

- FIX: *EDT* -- Crash when resolving EDT errors (#333)
- FIX: Error whene adding MACT or NSPS subparts (#334); thx Sean
- FIX: *SSPP* -- Title V renewal reminder letter needs page break between facilities (#335); thx Dave M.

## Version 3.7.1 <span>(2015-05-04)</span>

**May the Fourth edition**

+ NEW: *EDT* -- UI and data improvements for EDT Errors (#332, etc.)

- FIX: Crasher in Facility Creator
- FIX: *Smoke School* -- First class in Smoke School scores tab not displayed (#331)
- Various other bug fixes and UI improvements

## Version 3.7.0 <span>(2015-04-20)</span>

+ NEW: Module to review EDT errors returned by EPA's new data transfer system (#307)
+ NEW: *EDT* -- UI to trigger forcing an update of facility data to EPA (#314)
+ NEW: *SSPP* -- Updated Title V reminder letter with GEOS language (#327); thx Eric

- FIX: *ISMP* -- Smoke School module can't handle people with apostrophes in their name (#325); thx Jeff
- FIX: Better data handling when deleting a facility (#310)
- Various other bug fixes and UI improvements

## Version 3.6.10 <span>(2015-03-23)</span>

**March Madness edition** 

- FIX: Having trouble with your brackets? So were we! [Square brackets](https://en.wikipedia.org/wiki/Bracket#Square_brackets) in facility names were causing errors in the permit search website. For now, brackets will be replaced with parentheses. (#320); thx Noel 
- FIX: *SSCP & ISMP* -- Some data was incorrectly updated when a stack test was deleted (#260)

## Version 3.6.9 <span>(2015-02-26)</span>

+ FIX: Some Nav screen buttons not working (#308); thx Jing

## Version 3.6.8 <span>(2015-02-23)</span>

+ NEW: *SSCP* -- Display GEOS inspection ID on inspection form (#300)
+ NEW: *SSPP* -- Add confidential information flag to permit (#296)
+ NEW: *SSCP* -- Change day zero default to 90 days (#301)

- FIX: Crystal Reports plugin no longer available online (#304); thx SAP

## Version 3.6.7 <span>(2015-01-30)</span>

+ NEW: SSPP Program Manager can shut down a facility (#288); thx Eric
+ NEW: *SSCP* -- Ensure consistent formatting for CO numbers (#294)

- FIX: *ISMP* -- Improve UI in the Test Report form
- FIX: *SSCP* -- Not all items displayed in Compliance Log (#295); thx John Harrison

## Version 3.6.6 <span>(2014-12-04)</span>

- FIX: *SBEAP* -- Unable to access customer summaries (#279); thx John Y.
- FIX: Error message when entering an invalid AIRS # in the Navigation Screen Quick Access
- FIX: Longitude search broken in Query Generator (#191); thx Elisabeth
- FIX: *SSCP* -- Improve UI in the SSCP Managers Tools
- FIX: *SSCP* -- Improve data quality when copying source assignments to a new year in the SSCP Managers Tools (#271)

## Version 3.6.5 <span>(2014-11-13)</span>

+ NEW: Use updated SIC and NAICS tables (#277)

- FIX: Error when editing header data for facilities with no history (#276)
- FIX: Error when adding program subparts to facilities (#278); thx Eric
- FIX: *Fees* -- Incorrect check for valid AIRS number when updating some data (#275)
- FIX: *Fees* -- Incorrect payment amount when listing sources that have not paid (#275)

## Version 3.6.3 <span>(2014-10-28)</span>

+ NEW: *SSPP* -- Updated the Title V renewal reminder letter template (#267)

- FIX: Made it easier to identify and fix problems related to a missing Crystal Reports runtime (#266)

## Version 3.6.2 <span>(2014-10-24)</span>

- FIX: *ISMP* -- Allow district manager to be selected for compliance assignment in stack test form. But don't allow free-form text entry; it won't get saved! (#273); thx Josh

## Version 3.6.1 <span>(2014-10-20)</span>

- FIX: *SSPP* -- Errors when saving a new permit (#270); thx Manny

## Version 3.6 <span>(2014-10-17)</span>

+ NEW: List of all active and inactive permits in Facility Summary
+ NEW: *SSPP* -- Procedure for revoking old permits when issuing new permit (#196)
+ NEW: *SSCP* -- All active permits get marked as inactive when changing facility operating status to Closed (#242)
+ NEW: Improved Facility Header Data editor 

Note: Permits will be automatically added or marked as inactive as described above, but there is not yet a way to manually edit the list of permits. This is planned for a future release. In the meantime, please [contact the DMU](http://dmu.georgiaair.org/support) if you find any errors so we can fix them. Thanks!

## Version 3.5.7 <span>(2014-08-29)</span>

- FIX: *SSCP* -- Title V Renewals search broken since version 3.5.5 (#263); thx Sean

## Update 2014-08-25

Moved website to new URL

## Version 3.5.6 <span>(2014-08-25)</span>

- FIX: *SSCP* -- Check for HPV type before saving enforcement, if marked as HPV (#63); thx Karen
- FIX: *SBEAP* -- UI fixes on some screens (#230); thx Teresa
- FIX: Disable Save button in Edit Header Data screen if user doesn't have permission to save data (#256)
- FIX: Some other UI fixes

## Version 3.5.5 <span>(2014-08-05)</span>

+ NEW: *Smoke School* -- Improved Smoke School layout
+ NEW: Updated DMU contact phone numbers

- FIX: *SSCP* -- Upcoming Title V Renewals search shows incorrect SSCP contact (#243); thx Sean
- FIX: *Smoke School* -- Not all upcoming Smoke School events displayed (#251); thx Jeff

## Version 3.5.4 <span>(2014-08-04)</span>

- FIX: *Fees* -- Fix tab orderin Fee Audit Log (#253)
- FIX: Unable to install update (#254); thx Karen and Jimmy

## Version 3.5.3 <span>(2014-07-30)</span>

- FIX: *Fees* -- Unable to save admin data on Fee Audit Log (#250); thx Debbie

## Version 3.5.1 <span>(2014-07-25)</span>

- FIX: *Fees* -- Fee Management Tools button to open Fee Audit Log does not populate fee year

## Version 3.5.0 <span>(2014-07-25)</span>

+ NEW: *Fees* -- Change how Fee Mailout Contact is edited. Edit contacts directly from Fees Audit Log (#247)

- FIX: Allow AIRS number with dash in Navigation screen quick access (#249)
- FIX: *Smoke School* -- InvalidOperationException when opening Smoke School tool if no classes are current (#248)
- FIX: *Fees* -- Don't allow editing of GECO contact
- FIX: Various additional bug fixes and code cleanup

## Version 3.4.4 <span>(2014-06-26)</span>

+ NEW: Minor UI refinements and code cleanup

- FIX: *PASP* -- Fee audit log Update Audit button throws error (#224); thx Debbie

## Version 3.4.3 <span>(2014-06-12)</span>

+ NEW: *PASP* -- Updates to the Initial Mailout workflow in Fee Management Tools (#246)

## Version 3.4.1 <span>(2014-06-11)</span>

+ NEW: Some minor UI refinements and code cleanup

- FIX: *PASP* -- Several bugs in Fee Mailout System (#245 & #246)

## Version 3.4 <span>(2014-05-22)</span>

+ NEW: Several UI improvements to Navigation Screen (#238, #216, #167)

- FIX: Sometimes Navigation Screen default list does not load (#215); thx Dave M.
- FIX: Huge performance gain on Navigation Screen upon login (#239)
- FIX: Some URLs have changed after the Great NADC Server Consolidation (#234)

## Version 3.3 <span>(2014-05-12)</span>

- NEW: *SBEAP* -- Small Business Environmental Assistance Program now incorporated into IAIP (#210)
- NEW: Navigation buttons are now grouped by program/category (#211)

- FIX: Minor UI improvements

## Version 3.2.1 <span>(2014-05-04)</span>

- FIX: Additional work to ensure database connectivity after the server move (#225) (#228)

## Version 3.2 <span>(2014-04-24)</span>

This release prepares the IAIP for the upcoming server move to the 
North Atlanta Data Center. The IAIP will be unavailable from 5 PM 
on Friday, May 2 until Monday morning, May 5. 

As a side-benefit to the preparations, database availability problems 
are now handled more gracefully.

## Version 3.1.1 <span>(2014-04-15)</span>

- FIX: *MASP* -- Can't create new event registration (#217); thx Thomas

## Version 3.1 <span>(2014-04-14)</span>

- FIX: More work to prevent database timeout errors (#199)
- FIX: *EIS* -- Remove ambiguous Enroll Mailout List button (#198)
- FIX: *SSCP* -- ACC memo incorrectly reported whether postmarked by deadline (#204); thx Michael
- FIX: *SSCP* -- Make room for long company names in ACC memo (#193); thx Sherry
- FIX: Mild UI improvements in various places
- FIX: Unable to install IAIP in some cases if MS Office was installed before .NET framework (#213); thx Tammy

## Version 3.0 <span>(2014-04-03)</span>

Welcome to the latest version of the IAIP! We hope the new installation platform makes future updates easier for all users.

Many thanks to our brave volunteers who tested the new installer and offered valuable feedback!

+ Travis Harris
+ David Lyle
+ Lynne Collier
+ Gany Seetharaman
+ Sean Taylor
+ DeAnna Oser

We couldn't have done it without you!

## Version 3.0 RC2 <span>(2014-03-21)</span>

Second Release Candidate for Version 3.0.

+ FIX: *SSCP* – Not all SSCP managers can access enforcement document uploader (#197); thx Sean

## Version 3.0 RC1 <span>(2014-03-20)</span>

First Release Candidate for Version 3.0.

+ NEW: New installer/updater
+ NEW: Correct logo will now display for all users

## Version 2.9.2 <span>(2014-03-03)</span>

+ FIX: *SSCP* – Can't link to discovery event from brand new enforcement (#190); thx Sherry
+ FIX: Don't display connection environment on Navigation Screen for normal connections

## Version 2.9.0 <span>(2014-02-26)</span>

+ FIX: *SSPP* – Unable to generate acknowledgement email (#185); thx Gany
+ FIX: *SSCP* – Can't submit enforcement to UC or EPA (#183); thx Sean, Mike & Karen

## Version 2.8.9 <span>(2014-02-11)</span>

+ FIX: *SSCP* – ACC memo does not correctly indicate if previously unreported deviations were reported (#179); thx Msengi

## Version 2.8.8 <span>(2014-01-31, limited release)</span>

+ FIX: *ISMP* – Unable to open multiple test reports at once (#176); thx Dave S.
+ FIX: Error thrown if incorrect password entered in Login Screen (#177)
+ FIX: Various minor bug fixes

## Version 2.8.7 <span>(2014-01-23)</span>

+ FIX: *SSPP* – Application Tracking Log does not load data when opened from Navigation Screen (#173); thx Bradley

## Version 2.8.5 <span>(2014-01-22, limited release)</span>

+ NEW: New location added to Smoke School (#172)

## Version 2.8.4 <span>(2014-01-21)</span>

+ NEW: Many changes to the main Navigation Screen to make it more responsive and useable
+ NEW: Changes to the Facility Lookup tool to make it more useable

- FIX: Eliminated many of the database timeout errors people experience when the IAIP has been open for a while. Eliminating timeout errors is a work in progress. This release focused on the Navigation Screen, which accounts for over a third of all such errors logged. (#162)
- FIX: *SSCP* – FCE defaults to wrong calendar year (#82)
- FIX: Facility Lookup tool does not populate the Facility Summary (#170)

## Version 2.8.3 <span>(2014-01-08)</span>

<em>Happy new year!</em> Have a few bug-fixes to help celebrate.

- FIX: Forms sometimes open minimized, making them hard to find on Windows 7 (#159)
- FIX: *SSPP* – Application Log crashes if Navigation Screen button is double-clicked (#140)
- FIX: Some forms crash after being resized (finish fixing #157)

## Version 2.8.2 <span>(2013-12-27)</span>

+ NEW: *SSCP* – ACC review memos are now automatically generated
+ NEW: Improved Smoke School roster

- FIX: *PASP* – Removed obsolete Fee Audit Tool (#134)
- FIX: *ISMP/SSCP* – Clarified layout on Test Report compliance tab (#153)
- FIX: Smoke School Roster not printing (#120)
- FIX: *SSCP* – Unable to link enforcement event or save enforcement (#158)
- FIX: *SSCP* – "Add/Edit Enforcement Process" button on Compliance Event form is broken (#156)
- FIX: *SSCP* – Compliance Log fields obscured at small screen sizes (#155)
- FIX: *ISMP* – Some forms crashing after being resized (#157, but may still affect some forms)

## Version 2.8.1 <span>(2013-12-13)</span>

- FIX: Updating IAIP no longer forgets user settings, like login name (sorry about that!) (#152)

## Version 2.8.0 <span>(2013-12-12)</span>

+ NEW: *SSCP* – New enforcement document storage tool. Also, new documents tab on enforcement screen.
+ NEW: *SSCP* – Now you can open multiple enforcement screens at once.
+ NEW: *SSCP* – Improved usability on Compliance Log (#144)
+ NEW: Improved usability on event registration tool

- FIX: Registration tool email generator not working (#148)
- FIX: Registration tool View Details button not working (#149)
- FIX: *SSCP* – Unable to delete enforcement from Compliance Log (#151)

## Version 2.7.0 <span>(2013-11-20)</span>

+ NEW: Added error tracking to analytics
+ NEW: "Reset all forms" added to Login screen

- FIX: Last digit of date fields cut off on Windows 7 (#121)
- FIX: *ISMP* – Tests assigned by program manager cause Facility Summary to break (#141)

## Version 2.6.10 <span>(2013-11-12)</span>

+ NEW: Wish the Navigation Screen was bigger? Do you always maximize the Application Log? Now when you move forms around on your screen, the Platform will remember their size and location, so they will be in the same place the next time you open them. Got things a little muddled up? Choose "Reset all forms" from the Help menu on the main Navigation Screen.

- FIX: Improved file saving in some scenarios

## Version 2.6.9 <span>(2013-11-05)</span>

- FIX: *SSMP* – Buttons to add or delete facility from CMS universe not working (#138)

## Version 2.6.8 <span>(2013-10-24)</span>

- FIX: *Smoke School* – Printed diplomas did not include all classes (#132)

## Version 2.6.7 <span>(2013-10-18)</span>

+ NEW: *SSCP* – Better Excel export on SSCP Managers Tools & Compliance Log
+ NEW: *SSPP* – Better Excel export on Application Log
+ NEW: Better indication when logging in to Testing Environment
+ NEW: *ISMP* – Alphabetize engineer lists in ISMP managers tools (#129)

- FIX: Error when saving contact information (#126)
- FIX: Error when saving facility location
- FIX: *SSCP* – Unable to delete notifications in Compliance Log (#133)

## Version 2.6.6 <span>(2013-09-27)</span>

+ NEW: *Navigation Screen* – Added ability to export grid to Excel (#58)
+ NEW: *Smoke School* – Export to Excel works better
+ NEW: *SSCP* – Added Excel export to SSCP Managers statistical reports tab (#59)
+ NEW: Added new About page and Changelog

- FIX: *SSPP* – New error message when uploading permit docs (#118)

## Version 2.6.5 <span>(2013-09-20)</span>

+ NEW: New icon & logo for Windows 7

- FIX: *SSPP* – Error message when uploading permit docs (#118)

## Version 2.6.4.6 <span>(2013-09-13)</span>

+ NEW: Login form loads imperceptibly faster now

- FIX: Some data fields were unreadable on Windows 7 (#119)

## Version 2.6.4.5 <span>(2013-09-13)</span>

+ NEW: *Query Generator* – Better formatting of results
+ NEW: *Query Generator* – HUGE performance improvement when exporting to Excel

- FIX: *Query Generator* produces more relevant results when searching for Compliance Inspector or Compliance Unit (#114)

## Version 2.6.4.4 <span>(2013-09-09)</span>

+ NEW: Added analytics to Crystal Reports to better understand usage of printed reports

- FIX: *SSPP* – Improved grammar in acknowledgment email

## Version 2.6.4.3 <span>(2013-08-30)</span>

+ NEW: Display unassigned compliance items in the Navigation Screen compliance program view (#80)

- FIX: *ISMP* – Stack tests for District Offices were cc'ed to Terri Crosby-Vega (#97)
- FIX: *ISMP* – Try to prevent compliance staff being unassigned inappropriately (#80)
- FIX: Bug in Navigation Screen monitoring test reports program view
- FIX: *SSPP* – Email body truncated if company name contains an ampersand (#113)

## Version 2.6.4.1 <span>(2013-08-16)</span>

- FIX: *Smoke School* – Tools were unaccessible (#110)

## Version 2.6.4 <span>(2013-08-15)</span>

+ NEW: *Fees Log* – Added separate Administrative Fee line to invoice (#98)
+ NEW: Added application analytics to gain insight on usage and improve future development
+ Various minor improvements under the hood

## Version 2.6.3.3 <span>(2013-08-07)</span>

+ NEW: *Smoke School* – New location added (#107)

## Version 2.6.3.2 <span>(2013-07-22)</span>

- FIX: *Fee Audit Log* – Previous invoice report still displayed when a new facility is loaded (#92)

## Version 2.6.3.1 <span>(2013-07-22)</span>

- FIX: *Application Log* – Some users unable to display applications (#76)

## Version 2.6.3 <span>(2013-07-19)</span>

- FIX: Installation/updates on 64-bit machines (#77)

## Version 2.6.2.8 <span>(2013-05-21)</span>

+ NEW: Improved layout of data grid on Navigation Screen (#20)
+ NEW: Allow all SSCP staff to edit Facility Subparts (#37)
+ NEW: Allow deletion of stipulated penalties (#34)
+ NEW: Improved FCE form (layout, buttons, menu)

- FIX: State Contact data not refreshed correctly when loading new facility (#50)
- FIX: Bug in counting of stipulated penalties (#53)
- FIX: FCE did not include work done on the same day as the FCE (#54)