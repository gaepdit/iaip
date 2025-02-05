# IAIP Release Instructions

## Publishing DEV or UAT versions

1. Merge the feature branch into the desired deployment branch, `deploy/DEV` or `deploy/UAT`. 
2. Update the application revision number within the deployment branch by changing `<ApplicationVersion>` in the "IAIP.vbproj" file. (Also update `<MinimumRequiredVersion>` as needed.)
3. Push the updated deployment branch to GitHub to trigger a new installer build.

## Publishing a Production version

1. Merge the feature branch into `main`.
2. Update the application version number by changing `<ApplicationVersion>` in the "IAIP.vbproj" file. (Also update `<MinimumRequiredVersion>` as needed.)
3. [Update the changelog](../iaip-website/README.md).
    1. Edit the `iaip-website\www\changelog\index.md` file.
    2. Run the changelog build script `iaip-website\build.bat` to generate the HTML pages.
4. Commit the above changes.
5. Merge the `main` branch into the production deployment branch `deploy/PROD`. 
6. Push the production deployment branch to GitHub to trigger a new installer build.
7. Tag the released commit on the `main` branch using a tag in the format `v7/x.x.x`, substituting in the new version number. 
8. Push the tag to GitHub to trigger a new Raygun deployment registration.
9. Publish a new [release post](https://github.com/gaepdit/iaip/releases).
