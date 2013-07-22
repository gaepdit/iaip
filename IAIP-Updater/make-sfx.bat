@echo off
cls
echo The commands in this batch file should be included (with full paths)
echo in the VisualStudio project's post-build event command line and executed 
echo after a successful build. This batch file is only here as backup and is 
echo safe to run if necessary. The text of the post-build event command line 
echo is copied in "./IAIP-Updater/Post-build Events.txt"
echo
echo Creating updater zip file...
7za.exe a IAIP.update.7z ../JohnGaltProject.exe -mx=7
echo
echo Creating alternate updater for users of old versions (prior to v2.6.3.0)
echo to include several Oracle files for connecting to the database...
7za.exe a IAIP.7z ../JohnGaltProject.exe ../oci.dll ../Oracle.DataAccess.dll ../orannzsbb11.dll ../oraociicus11.dll ../OraOps11w.dll  ../tnsnames.ora -mx=7
echo
echo Creating self-extracting updater file from zip file...
copy /b "7zsd_All.sfx" + config.txt + IAIP.update.7z IAIP.update.exe
echo
echo Creating self-extracting updater for old versions...
copy /b "7zsd_All.sfx" + config.txt + IAIP.7z IAIP_Update.exe
copy /b "7zsd_All.sfx" + config.txt + IAIP.7z IAIP.exe
echo
echo Deleting unnecessary interim files...
del IAIP.update.7z
del IAIP.7z
del 7za.exe
del 7zsd_All.sfx
del config.txt
echo
echo (This batch file not deleted.)
pause