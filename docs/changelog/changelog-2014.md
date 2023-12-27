% IAIP: What's New -- 2014

*Previously:* [2013](changelog-2013.html)

## Version 3.6.6 <span>(2014-12-04)</span>

- FIX: *SBEAP* -- Unable to access customer summaries (#279); thx John Y.
- FIX: Error message when entering an invalid AIRS # in the Navigation Screen Quick Access
- FIX: Longitude search broken in Query Generator (#191); thx Elisabeth
- FIX: *SSCP* -- Improve UI in the SSCP Managers Tools
- FIX: *SSCP* -- Improve data quality when copying source assignments to a new year in the SSCP Managers Tools (#271)

## Version 3.6.5 <span>(2014-11-13)</span>

+ NEW: Use updated SIC and NAICS tables (#277)

---

- FIX: Error when editing header data for facilities with no history (#276)
- FIX: Error when adding program subparts to facilities (#278); thx Eric
- FIX: *Fees* -- Incorrect check for valid AIRS number when updating some data (#275)
- FIX: *Fees* -- Incorrect payment amount when listing sources that have not paid (#275)

## Version 3.6.3 <span>(2014-10-28)</span>

+ NEW: *SSPP* -- Updated the Title V renewal reminder letter template (#267)

---

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

Note: Permits will be automatically added or marked as inactive as described above, but there is not yet a way to manually edit the list of permits. This is planned for a future release. In the meantime, please contact the DMU if you find any errors so we can fix them. Thanks!

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

---

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

---

- FIX: Allow AIRS number with dash in Navigation screen quick access (#249)
- FIX: *Smoke School* -- InvalidOperationException when opening Smoke School tool if no classes are current (#248)
- FIX: *Fees* -- Don't allow editing of GECO contact
- FIX: Various additional bug fixes and code cleanup

## Version 3.4.4 <span>(2014-06-26)</span>

+ NEW: Minor UI refinements and code cleanup

---

- FIX: *PASP* -- Fee audit log Update Audit button throws error (#224); thx Debbie

## Version 3.4.3 <span>(2014-06-12)</span>

+ NEW: *PASP* -- Updates to the Initial Mailout workflow in Fee Management Tools (#246)

## Version 3.4.1 <span>(2014-06-11)</span>

+ NEW: Some minor UI refinements and code cleanup

---

- FIX: *PASP* -- Several bugs in Fee Mailout System (#245 & #246)

## Version 3.4 <span>(2014-05-22)</span>

+ NEW: Several UI improvements to Navigation Screen (#238, #216, #167)

---

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

---

- FIX: Eliminated many of the database timeout errors people experience when the IAIP has been open for a while. Eliminating timeout errors is a work in progress. This release focused on the Navigation Screen, which accounts for over a third of all such errors logged. (#162)
- FIX: *SSCP* – FCE defaults to wrong calendar year (#82)
- FIX: Facility Lookup tool does not populate the Facility Summary (#170)

## Version 2.8.3 <span>(2014-01-08)</span>

<em>Happy new year!</em> Have a few bug-fixes to help celebrate.

- FIX: Forms sometimes open minimized, making them hard to find on Windows 7 (#159)
- FIX: *SSPP* – Application Log crashes if Navigation Screen button is double-clicked (#140)
- FIX: Some forms crash after being resized (finish fixing #157)
