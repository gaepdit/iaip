@echo off
cls

ECHO : IAIP deployment script
ECHO : ----------------------
ECHO : 
:ask
ECHO : Deploy IAIP (Y/N)?

SET /P "ANSWER=: "
IF /I "%ANSWER%" EQU "Y" GOTO :deploy
IF /I "%ANSWER%" EQU "N" GOTO :eof

GOTO :ask

:deploy

ECHO : 
ECHO : Deploying...
ECHO : 

IF NOT EXIST S:\ISMP\DMU\Execs\appIAIP GOTO :NoSDrive

ECHO : Copying files to "S:\ISMP\DMU\Execs\appIAIP"...
ECHO :

COPY /y "..\*.*" "S:\ISMP\DMU\Execs\appIAIP"
ECHO :

GOTO :installer

:NoSDrive

ECHO : "S:\ISMP\DMU\Execs\appIAIP" is not available...
ECHO :

GOTO :installer

:installer

IF NOT EXIST "S:\ISMP\DMU\APB IAIP\Installer" GOTO :NoInstaller

ECHO : Copying installer to "S:\ISMP\DMU\APB IAIP\Installer"...
ECHO :

COPY /y "..\IAIP-Installer\*.zip" "S:\ISMP\DMU\APB IAIP\Installer"
ECHO :

GOTO :ftp

:NoInstaller

ECHO : "S:\ISMP\DMU\APB IAIP\Installer" is not available...
ECHO :

GOTO :ftp

:ftp

IF NOT EXIST "..\IAIP-Installer" GOTO :NoFTP
IF NOT EXIST "..\IAIP-Updater" GOTO :NoFTP


ECHO : Uploading files to FTP site...
ECHO : 
ftp -v -i -s:ftp-script.txt
ECHO : 

GOTO :done

:NoFTP

ECHO : Files to upload to FTP site don't exist...
ECHO :

:done

ECHO : Script has finished.
ECHO :
PAUSE

GOTO :eof