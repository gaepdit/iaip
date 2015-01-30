IAIP Build & Release Instructions
=================================

Step Zero of releasing a new IAIP version is, of course, to test all changes thoroughly.

There are two main branches in the repository: *default* and *stable*. The *stable* branch is for releases; the *default* branch is for development of future releases. When preparing for a new version, all new features and bug fixes should be merged into the *default* branch and tested there.

Once you're sure a new version is ready for general release, follow these instructions.

1. Close the project in Visual Studio.

1. Merge all *default* branch into the *stable* branch.

    1. Ensure there are no uncommitted changes in the *default* branch.
	
		```hg status```

	1. Update to the *stable* branch, merge with *default*, and commit.
	
		```
		hg update stable
		hg merge default
		hg commit -m Merge with default
		```

1. Reopen the project in Visual Studio and build it in Debug mode to check for errors.

1. Change to Release mode. (Build → Configuration Manager… → Active solution configuration (dropdown) → Release)

1. Change the version numbers. The version is entered in three places and all three must match. 
	
	In general, the Major version number is rarely changed. In the past, it has been updated only when installation requirements are significantly altered, such as changing the target .NET framework or moving to a ClickOnce installation.
	
	The Minor version number is used to indicate majore new features have been added. The Build version is used for bugfixes.
    
	(Note: The Revision number is handled automatically and does not need to match. Only worry about Major, Minor and Build numbers.)
	
	1. Open the project properties page (Project → IAIP Properties…). Select the Application tab and open Assembly Information. Change the Assembly and File Versions to the new version number. (Note: These version numbers are only used by the analytics package to track version usage. Leave the Revision at zero.)
	1. Select the Publish tab and change the Publish Version. (Note: This is the version number used by the automatic update system. Leave the Revision as-is; it is automatically incremented on each publish.)
	1. *Optional:* Change the minimum required version in the Updates dialog.
	1. Update the changelog to include the correct version number and publication date.

1. Commit the changes to the version number and changelog.

    ```
	hg commit -m Bump version numbers and update changelog
	```

1. Publish the IAIP: Either select "Publish Now" in the IAIP properties page, Publish tab *or* select Build → Publish IAIP.

1. Publish the IAIP Documentation: Select the IAIP Documentation project in the Solution Explorer and select Build → Publish IAIP Documentation.

1. Test that the IAIP will update to the new version and that it works as expected.

1. Tag the new version in Mercurial.

1. Merge the changes back into the *default* branch.

	```
	hg update default
	hg merge stable
	hg commit -m Merge with stable
	```
