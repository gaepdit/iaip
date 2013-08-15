@echo off
cls
echo 2013-08-15: This batch file is out of date, and all commands have been 
echo disabled. If it is still needed, use "Post-build Events.txt" as 
echo a guide for rewriting it.
echo 
echo The commands in this batch file should be included (with full paths)
echo in the VisualStudio project's post-build event command line and executed 
echo after a successful build. This batch file is only here as backup and is 
echo safe to run if necessary. The text of the post-build event command line 
echo is copied in "./IAIP-Updater/Post-build Events.txt"
echo
echo Note: This batch file only processes the IAIP-Updater folder. 
echo The Post-build event command line also processes the IAIP-Installer 
echo folder.
echo
echo Creating updater zip file...
echo ::: 7za.exe a IAIP.update.7z ../JohnGaltProject.exe -mx=7
echo
echo Creating alternate updater for users of old versions (prior to v2.6.3.0)
echo to include several Oracle files for connecting to the database...
echo ::: 7za.exe a IAIP.7z ../JohnGaltProject.exe ../oci.dll ../Oracle.DataAccess.dll ../orannzsbb11.dll ../oraociicus11.dll ../OraOps11w.dll  ../tnsnames.ora -mx=7
echo
echo Creating self-extracting updater file from zip file...
echo ::: copy /b "7zsd_All.sfx" + config.txt + IAIP.update.7z IAIP.update.exe
echo
echo Creating self-extracting updater for old versions...
echo ::: copy /b "7zsd_All.sfx" + config.txt + IAIP.7z IAIP_Update.exe
echo ::: copy /b "7zsd_All.sfx" + config.txt + IAIP.7z IAIP.exe
echo
echo Deleting unnecessary interim files...
echo ::: del IAIP.update.7z
echo ::: del IAIP.7z
echo ::: del 7za.exe
echo ::: del 7zsd_All.sfx
echo ::: del config.txt
echo
echo (This batch file not deleted.)
pause