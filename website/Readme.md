# iaip.gaepd.org

This is the website for hosting the [IAIP](https://bitbucket.org/gaepdit/iaip/) installation files plus some documentation.

## Build Instructions

The HTML files are generated from the Markdown (*.md) files using the `build.bat` or `build.sh` script. This requires that [Pandoc](https://pandoc.org/) be installed on your computer.

## Deployment Instructions

Everything in the `www` folder should be copied onto the server using any method you prefer. There is a `publish.bat` or `publish.sh` script to automate this process, but they require some initial setup to use. (Specifically, you must install and configure git-ftp.)

### git-ftp

Git-ftp is a Git plugin that uploads files to an FTP server using your Git history to determine which files have changed since the last upload.

#### Install git-ftp on Windows

Run Git Bash as Administrator and run the following commands to [install git-ftp](https://github.com/git-ftp/git-ftp/blob/master/INSTALL.md#windows) and set your FTP username:

```bash
curl https://raw.githubusercontent.com/git-ftp/git-ftp/master/git-ftp > /bin/git-ftp

chmod 755 /bin/git-ftp
```

#### Install git-ftp on MacOS

```bash
brew install git-ftp
```

#### Configure git-ftp

```bash
git config git-ftp.user <your-FTP-username>
```

### Notes

The publish script only succeeds if your repository is clean (i.e., there are no uncommitted changes). It first calls the build script, which will ensure all changes to the Markdown files are correctly reflected in the HTML files before publishing.
