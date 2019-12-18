# iaip.gaepd.org

This is the website for hosting the [IAIP](https://bitbucket.org/gaepdit/iaip/) 
installation files plus some documentation.

## Build Instructions

The HTML files are generated from the Markdown (*.md) files using the `build.bat`
script. This requires that [Pandoc](https://pandoc.org/) be installed on your
computer.

## Deployment Instructions

Everything in the `www` folder should be copied onto the server using any method
you prefer. There is a `publish.bat` script to automate this process, but it
requires some initial setup to use:

### Install git-ftp

Git-ftp is a Git plugin that uploads files to an FTP server using your Git
history to determine which files have changed since the last upload.

Run Git Bash as Administrator and run the following commands to
[install git-ftp](https://github.com/git-ftp/git-ftp/blob/master/INSTALL.md#windows)
and set your FTP username:

```bash
curl https://raw.githubusercontent.com/git-ftp/git-ftp/master/git-ftp > /bin/git-ftp

chmod 755 /bin/git-ftp

git config git-ftp.user <your-FTP-username>
```

### Update curl

Curl is a command line tool for getting or sending files to remote servers. It
is already installed on Windows, and a second copy gets installed with Git, but
unfortunately, these versions weren't built with suppport for SFTP.

Download the latest binary of [curl for Windows](https://curl.haxx.se/windows/),
save the contents of the bin folder somewhere, and add this folder to your PATH
environment variable (make sure it appears before "%SystemRoot%\system32"). 

Then disable the copy of curl.exe in the `C:\Program Files\Git\mingw64\bin`
folder (I just renamed mine to `x-curl.exe`). You can use the `fix-curl.bat`
script for this purpose. *(NOTE: You will have to repeat this
step any time Git is updated.)*

### Notes

The publish script only succeeds if your repository is clean (there are no
uncommitted changes). It first calls the build script, which will ensure all
changes to the Markdown files are correctly reflected in the HTML files.
