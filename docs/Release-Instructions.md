# IAIP Release Instructions

1. Use git-flow to create a new release branch with the desired version number.
2. Update the version numbers in both the `AssemblyInfo.vb` and `IAIP.vbproj` files and commit the changes. (The vbproj file also has a value for minimum required version that should be updated as needed.)
3. To release a **UAT** version:
    1. Create a separate UAT branch from the new release branch.
    2. Incorporate the "UAT Project Mods" changes. This modifies the vbproj file to change Assembly Name, Publish URL, etc. The easiest way to do this is to cherrypick or rebase the old UAT branch onto the current release branch.
    3. Open in Visual Studio and change the active solution configuration to "UAT".
    4. Publish the application. (Open the project properties page, select the Publish tab and select "Publish Now".)
4. To release a **production** version:
    1. Use git-flow to finish the release. (Since this automatically uses the release branch created above, any changes made on the UAT branch will not be included in the production version.)
    2. Open in Visual Studio and change the active solution configuration to "Release".
    3. Publish the application.
    4. Update the changelog on the IAIP website.
