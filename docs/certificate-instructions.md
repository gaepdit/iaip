# IAIP Signing Certificate Instructions

The ClickOnce signing certificate expires after one year, and a new one must be generated.

## Generate a new signing certificate

1. Open the IAIP project properties.

2. Select "Signing" in the menu on the left.

3. Check "Sign the ClickOnce manifests".

4. Select "Create Test Certificate...".

5. Leave the password fields blank and select "OK".

6. Select File → Save All (Ctrl+Shift+S).

This will create a new certificate named "IAIP_TemporaryKey.pfx" and add several entries in the "IAIP.vbproj" file. *Don't commit these changes.* Once the entire procedure is finished, these changes will be discarded.

## Add the new certificate values to the GitHub repository

(Adapted from a Microsoft [GitHub Actions sample](https://github.com/microsoft/github-actions-for-desktop-apps?tab=readme-ov-file#signing) repository with fixes based on [this PowerShell change](https://github.com/PowerShell/PowerShell/issues/14537#issuecomment-754014628).)

1. Open PowerShell to the "IAIP" folder and Base64-encode the ".pfx" file by running the following Powershell script:

    ```powershell
    $pfx_cert = Get-Content '.\IAIP_TemporaryKey.pfx' -AsByteStream
    [System.Convert]::ToBase64String($pfx_cert) | Out-File 'SigningCertificate_Encoded.txt'
    ```

2. In GitHub, go to [Actions secrets and variables](https://github.com/gaepdit/iaip/settings/secrets/actions) in the IAIP repository.

3. Update the value of the `BASE64_ENCODED_PFX` secret with the text of the "SigningCertificate_Encoded.txt" file.

4. Update the value of the `PFX_THUMBPRINT` secret with the value in the `<ManifestCertificateThumbprint>` element from the updated "IAIP.vbproj" file.

5. Discard changes from the IAIP repository. This should delete the "IAIP_TemporaryKey.pfx" and "SigningCertificate_Encoded.txt" files and remove the additions to the "IAIP.vbproj" file.
