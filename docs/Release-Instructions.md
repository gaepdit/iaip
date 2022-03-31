# IAIP Release Instructions

## Publishing procedure

1. Update the version number by changing `AssemblyVersion` in "AssemblyInfo.vb" and `ApplicationVersion` in the "IAIP.vbproj" file so that they match. (The vbproj file also has a value for `MinimumRequiredVersion` that may be updated as needed.)

2. Release a **UAT** version:

    1. Rebase the "deploy/UAT" branch onto the release branch ("main") to incorporate the "UAT Project Mods" changeset. This modifies the vbproj file with changes to the Assembly Name, Publish URL, etc. that allow it to be published as an independent application for testing purposes.
    2. Open the solution in Visual Studio and change the active solution configuration to "UAT".
    3. Publish the application: Open the project properties page, select the Publish tab and select "Publish Now".
    4. To push the updated "deploy/UAT" branch to GitHub, you will need to force push.

3. Release a **Production** version:

    1. Check out the release branch ("main").
    2. Open the solution in Visual Studio and change the active solution configuration to "Release".
    3. Publish the application: Open the project properties page, select the Publish tab and select "Publish Now".
    4. Tag the release using a tag in the format "version6/x.x.x", substituting in the new version number. Manually push the tag to GitHub. (Git tags don't get pushed automatically.)

4. Update and publish the website changelog in the "iaip-website" folder. Instructions for publishing the website are in the [website Readme file](../iaip-website/Readme.md).
