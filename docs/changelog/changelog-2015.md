% IAIP: What's New -- 2015

*Previously:* [2014](changelog-2014.html) | [2013](changelog-2013.html)

## Version 4.2.9 <span>(2015-12-16)</span>

- FIX: *SSPP* -- Fixed more bugs introduced in version 4.2.5 (#402); thx Manny (again!)

## Version 4.2.8 <span>(2015-12-11)</span>

- FIX: *SSCP* -- Unable to open FCE from Compliance Log (#403); thx Pierre
- FIX: *SSPP* -- Unable to start permit application for new facility (#404); thx Mary W (again!)

## Version 4.2.7 <span>(2015-12-11)</span>

- FIX: *SSPP* -- Fixed more bugs introduced in version 4.2.5 (#402); thx Mary W (again) & Dave M!

## Version 4.2.6 <span>(2015-12-10)</span>

- FIX: *SSPP* -- Fixed bugs introduced in version 4.2.5 (#402); thx Mary W, Manny, & Hamid!

## Version 4.2.5 <span>(2015-12-08)</span>

+ Code cleanup and bug fixes, mostly in Application Tracking Log
+ Improved database connection stability
+ Reduced file download size

## Version 4.2.4 <span>(2015-11-18)</span>

- FIX: *Fees* -- Added ability to fix facility status for facilities with zero balance (#393); thx Sakina
- FIX: *Fees* -- One of the fee statistics queries returned data for the wrong year (#392); thx Debbie
- Minor bug fixes and performance improvements

## Version 4.2.2 <span>(2015-10-08)</span>

- FIX: Modifications to new FCE printout (#377 & #378); thx Sean

## Version 4.2.1 <span>(2015-10-06)</span>

- FIX: Unable to create FCEs after updating to version 4.2 (#376); thx John C!

## Version 4.2.0 <span>(2015-10-06)</span>

**"It's Harder Than It Looks" edition**

+ NEW: Redesigned facility summary printouts
+ NEW: *SSCP* -- Redesigned FCE printouts

## Version 4.1.5 <span>(2015-10-05)</span>

+ NEW: *EIS* -- New columns needed in EIS Stats view (#374); thx Jing

## Version 4.1.4 <span>(2015-10-05)</span>

+ NEW: Location map on Facility Summary

---

- FIX: *SSCP* -- Automatically select date range for new report (#356); thx Sherry!
- FIX: *SSCP* -- Tool to copy annual facility assignments is broken (#373); thx Sean!

## Version 4.1.3 <span>(2015-09-09)</span>

- FIX: *SSPP* -- Unable to upload permit documents (#370); thx Lynn
- FIX: *SSPP* -- Title V renewal letter has outdated URLs (#371); thx again Lynn!

## Version 4.1.2 <span>(2015-09-04)</span>

- FIX: *EIS* -- Exclude facilities not yet in QA process when viewing QA results (#367); thx Jing
- Other bugfixes and code cleanup

## Version 4.1.1 <span>(2015-08-24)</span>

**💯 edition** -- This is the 100th "official" build of the IAIP (since version 3.0)! 🎉

- FIX: Update URL for Draft Permits (#363); thx Lynn

## Version 4.1.0 <span>(2015-08-21)</span>

**"High Priority" edition**

+ NEW: *SSCP* -- Shiny new HPV/FRV codes available for enforcement actions (#265); thx Sean, EPA
+ NEW: *EDT* -- Open relevant IAIP data directly from error detail form (#332)

---

- FIX: *SSCP* -- Only allow Day Zero for HPVs
- FIX: *EDT* -- Fix tab order on EDT error detail form

## Version 4.0.3 <span>(2015-08-17)</span>

+ NEW: Better process for exporting to Excel implemented in remainder of IAIP (#357); thx Debbie
+ NEW: *SSCP* -- Improved enforcement event linking tool

---

- FIX: Better checking for installed Crystal Reports version
- FIX: Display latest info after saving changes in Header Data editor (#358); thx Brian

## Version 4.0.2 <span>(2015-07-24)</span>

+ NEW: *SSPP* -- Updated permit acknowledgment letter and email templates (#353); thx Eric
+ NEW: Restore ability to select (and copy) individual cells in Facility Summary Contacts tab (#354); thx Jing

---

- FIX: Some Crystal Reports documents were asking for a database password for no good reason (#355); thx Ginger

## Version 4.0.1 <span>(2015-07-16)</span>

- FIX: Bug in user permissions tool created bad data (#352); thx Chang

## Version 4.0 <span>(2015-07-14)</span>

**"Post-modern" edition**

+ Updated IAIP to use .NET Framework 4.5.2
+ Updated Crystal Reports to version 13.0.14

## Version 3.8.3 <span>(2015-06-23)</span>

- FIX: Crash when editing header data (#348); thx Sean
- FIX: Reload facility summary data if edited using Edit Header Data form

## Version 3.8.2 <span>(2015-06-18)</span>

**"Higgs-Bugson" edition**

- FIX: *ISMP* -- Re-enable ability to clear all data from stack test, which was mysteriously disappeared in Version 3.6.10 (#342); thx Anna, Dave S., Bob, and Josh!
- FIX: *SSCP & ISMP* -- Fix for bug #181, in which about three times a year, two compliance work items might be created for one stack test, resulting in a work item that could never be closed. (Classic three-two-one bug.)

## Version 3.8.1 <span>(2015-06-04)</span>

- FIX: Facility Summary crash on fees tab if no fees paid (#346); thx Sherry

## Version 3.8.0 <span>(2015-06-03)</span>

**"Not-enough-contrast" edition**

+ NEW: Overhauled the Facility Summary:
    + Improved data presentation
    + Streamlined the user interface
    + Cleaned up code to make it faster and more efficient
    + Added ability to open multiple facilities simultaneously
+ NEW: *SSCP* -- Display air program in compliance manager tools pollutant bubble-up form (#124)
+ NEW: *SSCP* -- Added "Follow Up" to list of available inspection reasons for GEOS compatibility (#285)
+ NEW: *EIS* -- Added Date Submitted column to EIS Statistics (#324)
+ NEW: *EIS* -- EIS tools can modify both facility location and mailing address (fix #241)
+ NEW: Better tracking of IAIP errors and crashes to help us fix these in the future
+ Also... lots of fixes and code cleanup throughout

## Version 3.7.3 <span>(2015-05-21)</span>

+ NEW: *SSPP* -- App Log can now search for permit applications with confidential info (#296); yw Eric

---

- FIX: *ISMP* -- Clearly identify test reports that have been deleted (#342)
- FIX: Error when closing a facility (#344); thx Sean
- FIX: *EDT* -- EDT Tools now available to SSPP PM2 (#343)
- FIX: *SSPP* -- Better formatting of list in Title V Renewal letter tool

## Version 3.7.2 <span>(2015-05-06)</span>

- FIX: *EDT* -- Crash when resolving EDT errors (#333)
- FIX: Error whene adding MACT or NSPS subparts (#334); thx Sean
- FIX: *SSPP* -- Title V renewal reminder letter needs page break between facilities (#335); thx Dave M.

## Version 3.7.1 <span>(2015-05-04)</span>

**"May the Fourth" edition**

+ NEW: *EDT* -- UI and data improvements for EDT Errors (#332, etc.)

---

- FIX: Crasher in Facility Creator
- FIX: *Smoke School* -- First class in Smoke School scores tab not displayed (#331)
- Various other bug fixes and UI improvements

## Version 3.7.0 <span>(2015-04-20)</span>

+ NEW: Module to review EDT errors returned by EPA's new data transfer system (#307)
+ NEW: *EDT* -- UI to trigger forcing an update of facility data to EPA (#314)
+ NEW: *SSPP* -- Updated Title V reminder letter with GEOS language (#327); thx Eric

---

- FIX: *ISMP* -- Smoke School module can't handle people with apostrophes in their name (#325); thx Jeff
- FIX: Better data handling when deleting a facility (#310)
- Various other bug fixes and UI improvements

## Version 3.6.10 <span>(2015-03-23)</span>

**"March Madness" edition**

- FIX: Having trouble with your brackets? So were we! [Square brackets](https://en.wikipedia.org/wiki/Bracket#Square_brackets) in facility names were causing errors in the permit search website. For now, brackets will be replaced with parentheses. (#320); thx Noel
- FIX: *SSCP & ISMP* -- Some data was incorrectly updated when a stack test was deleted (#260)

## Version 3.6.9 <span>(2015-02-26)</span>

+ FIX: Some Nav screen buttons not working (#308); thx Jing

## Version 3.6.8 <span>(2015-02-23)</span>

+ NEW: *SSCP* -- Display GEOS inspection ID on inspection form (#300)
+ NEW: *SSPP* -- Add confidential information flag to permit (#296)
+ NEW: *SSCP* -- Change day zero default to 90 days (#301)

---

- FIX: Crystal Reports plugin no longer available online (#304); thx SAP

## Version 3.6.7 <span>(2015-01-30)</span>

+ NEW: SSPP Program Manager can shut down a facility (#288); thx Eric
+ NEW: *SSCP* -- Ensure consistent formatting for CO numbers (#294)

---

- FIX: *ISMP* -- Improve UI in the Test Report form
- FIX: *SSCP* -- Not all items displayed in Compliance Log (#295); thx John Harrison
