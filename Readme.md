# iaip.gaepd.org

Website for hosting the [IAIP](https://bitbucket.org/gaepdit/iaip/) installation
files and some documentation.

## Instructions

The HTML files are generated from the Markdown (*.md) files using the `build.bat`
script. This requires that [Pandoc](https://pandoc.org/) be installed on your
computer.

Everything in the `www` folder should be copied onto the server using any method
you prefer. There is a `publish.bat` script to automate this process, but it
requires some initial setup:

Run Git Bash as Administrator and run the following commands to
[install git-ftp](https://github.com/git-ftp/git-ftp/blob/master/INSTALL.md#windows)
and set your FTP username:

```bash
curl https://raw.githubusercontent.com/git-ftp/git-ftp/master/git-ftp > /bin/git-ftp

chmod 755 /bin/git-ftp

git config git-ftp.user <your-FTP-username>
```

The publish script only succeeds if your repository is clean (there are no
uncommitted changes).
