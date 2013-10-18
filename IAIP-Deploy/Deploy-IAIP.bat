@echo off
cls

ECHO ^| 
ECHO ^| IAIP deployment script
ECHO ^| ----------------------
ECHO ^| 

:ask

ECHO ^| Deploy IAIP (Y/N)?

SET /P "ANSWER=| "
IF /I "%ANSWER%" EQU "Y" GOTO :deploy
IF /I "%ANSWER%" EQU "N" GOTO :eof

GOTO :ask

:deploy

ECHO : 
ECHO : Deploying...
ECHO : 

REM --- Copy files that CopyGATV.bat will look for

IF EXIST "S:\ISMP\DMU\Execs\appIAIP" (

  ECHO ^| Copying files to "S:\ISMP\DMU\Execs\appIAIP"...
  ECHO ^|

  COPY /y "..\*.*" "S:\ISMP\DMU\Execs\appIAIP"
  
  ECHO ^|

  IF NOT EXIST "S:\ISMP\DMU\Execs\appIAIP\docs" (
    MKDIR  "S:\ISMP\DMU\Execs\appIAIP\docs"
  )

  IF EXIST "S:\ISMP\DMU\Execs\appIAIP" (
    COPY /y "..\docs\*.*" "S:\ISMP\DMU\Execs\appIAIP\docs"
    ECHO ^|
  )

) ELSE (

  ECHO ^| "S:\ISMP\DMU\Execs\appIAIP" is not available...
  ECHO ^|

)

REM --- Copy installer to S Drive

IF EXIST "S:\ISMP\DMU\APB IAIP\Installer" (

  ECHO ^| Copying installer to "S:\ISMP\DMU\APB IAIP\Installer"...
  ECHO ^|

  COPY /y "..\IAIP-Installer\*.zip" "S:\ISMP\DMU\APB IAIP\Installer"
  
  ECHO ^|

) ELSE (

  ECHO ^| "S:\ISMP\DMU\APB IAIP\Installer" is not available...
  ECHO ^|

)

REM --- Upload files to FTP site

IF EXIST "..\IAIP-Installer" (

  IF EXIST "..\IAIP-Updater" (

    ECHO ^| Uploading files to FTP site...
    ECHO ^| 
    
    FTP -v -i -s:ftp-script.txt
	
    ECHO ^| 

  ) ELSE (

    ECHO ^| Files to upload to FTP site don't exist...
    ECHO ^|

  )

) ELSE (

  ECHO ^| Files to upload to FTP site don't exist...
  ECHO ^|

)

ECHO ^| Script has finished.
ECHO ^|
ECHO ^| Remaining tasks:
ECHO ^| 
ECHO ^| * Move the update files on the server from /AirPermit/IAIP/new
ECHO ^|   to /AirPermit/IAIP (and backup the existing files there)
ECHO ^| 
ECHO ^| * Change the published version number in the Platform AFS tools
ECHO ^|
PAUSE

:eof