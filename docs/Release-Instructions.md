# IAIP Release Instructions

## Setup

Before publishing the first time, you will need to [create a Test Certificate](https://msdn.microsoft.com/en-us/library/che5h906%28v=vs.120%29.aspx) for signing the ClickOnce manifest. The test certificate expires after one year, and a new one must be generated.

**Warning**: This will change the value of the `<ManifestCertificateThumbprint>` element in the "IAIP.vbproj" file. Be aware that commiting this change to the repository may cause conflicts if multiple developers are creating certificates. I recommend that only the primary owner of the IAIP application commit this change and share the generated certificate with other publishers if necessary.

You will also need to copy the appropriate config files from the "app-config" repo. You will need "app.UAT.config" to publish to UAT and "app.Release.config" to publish to Production.

## Publishing procedure

1. Update the version number by changing `AssemblyVersion` in "AssemblyInfo.vb" and `ApplicationVersion` in the "IAIP.vbproj" file so that they match and commit this change. (The vbproj file also has a value for `MinimumRequiredVersion` that should be updated as needed.)

1. Release a **UAT** version:

    1. Rebase the "deploy/UAT" branch onto the release branch ("main") to incorporate the "UAT Project Mods" changeset. This modifies the vbproj file with changes to the Assembly Name, Publish URL, etc. that allow it to be published as an independent application.
    2. Open the solution in Visual Studio and change the active solution configuration to "UAT".
    3. Publish the application: Open the project properties page, select the Publish tab and select "Publish Now".

1. Release a **Production** version:

    1. Check out the release branch ("main")
    2. Open the solution in Visual Studio and change the active solution configuration to "Release".
    3. Publish the application: Open the project properties page, select the Publish tab and select "Publish Now".

1. Update the changelog on the IAIP website.
