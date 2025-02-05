# IAIP Signing Certificate Instructions

The ClickOnce signing certificate expires after one year, and a new one must be generated.

## Generate a new signing certificate

1. Open the IAIP project properties.

2. Select "Signing" in the menu on the left.

3. Check "Sign the ClickOnce manifests".

4. Select "Create Test Certificate...".

5. Leave the password fields blank and select "OK".

6. Select File → Save All (<kbd>Ctrl</kbd>+<kbd>Shift</kbd>+<kbd>S</kbd>).

This will create a new certificate named "IAIP_TemporaryKey.pfx" and add several entries in the "IAIP.vbproj" file. *Don't commit these changes.* Once the entire procedure is finished, these changes will be discarded.

## Add the new certificate values to the GitHub repository

(Adapted from a Microsoft [GitHub Actions sample](https://github.com/microsoft/github-actions-for-desktop-apps?tab=readme-ov-file#signing) repository with fixes based on [this PowerShell change](https://github.com/PowerShell/PowerShell/issues/14537#issuecomment-754014628).)

1. Open PowerShell to the "IAIP" folder and Base64-encode the ".pfx" file by running the following Powershell commands:

    ```powershell
    [System.Convert]::ToBase64String([System.IO.File]::ReadAllBytes("IAIP_Key.pfx")) | Out-File "BASE64_ENCODED_PFX.txt"

    (Get-PfxCertificate -Filepath 'IAIP_Key.pfx').Thumbprint | Out-File "PFX_THUMBPRINT.txt"
    ```

    These two commands generate text files to be used in the following steps.

2. In GitHub, go to [Actions secrets and variables](https://github.com/gaepdit/iaip/settings/secrets/actions) in the IAIP repository.

    1. Add or update the value of the `BASE64_ENCODED_PFX` secret with the text of the `BASE64_ENCODED_PFX.txt` file.
    2. Add or update the value of the `PFX_THUMBPRINT` secret with the text of the `PFX_THUMBPRINT.txt` file.

5. Discard changes from the IAIP git repository. This should delete the files generated above and remove the additions to the "IAIP.vbproj" file.
