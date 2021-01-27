# IAIP Release Instructions

1. Update the version numbers in both the `AssemblyInfo.vb` and `IAIP.vbproj` files and commit the changes. (The vbproj file also has a value for minimum required version that should be updated as needed.)
2. Release a **UAT** version:
    1. Rebase the `deploy/UAT` branch onto the release branch to incorporate the "UAT Project Mods" changes. This modifies the vbproj file to change Assembly Name, Publish URL, etc.
    2. Open in Visual Studio and change the active solution configuration to "UAT".
    3. Publish the application. (Open the project properties page, select the Publish tab and select "Publish Now".)
3. Release a **production** version:
    1. Check out the release branch
    2. Open in Visual Studio and change the active solution configuration to "Release".
    3. Publish the application.
    4. Update the changelog on the IAIP website.
