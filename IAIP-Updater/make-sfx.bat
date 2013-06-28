@echo off
cls
echo The commands in this batch file should be included (with full paths)
echo in the VisualStudio project's post-build event command line and executed 
echo after a successful build. This batch file is only here as backup and is 
echo safe to run.
echo
echo Creating zip file...
REM VisualStudio command: "$(TargetDir)IAIP-Updater\7za.exe" a "$(TargetDir)IAIP-Updater\IAIP.7z" "$(TargetDir)JohnGaltProject.exe"
7za.exe a IAIP.7z ../JohnGaltProject.exe
echo Creating self-extracting updater file from zip file...
REM VisualStudio command: copy /b "$(TargetDir)IAIP-Updater\7zsd_All.sfx" + "$(TargetDir)IAIP-Updater\config.txt" + "$(TargetDir)IAIP-Updater\IAIP.7z" "$(TargetDir)IAIP-Updater\IAIP_update.exe"
copy /b "7zsd_All.sfx" + config.txt + IAIP.7z IAIP_update.exe
echo Creating alternate updater for users of old versions...
REM VisualStudio command: copy /b "$(TargetDir)IAIP-Updater\7zsd_All.sfx" + "$(TargetDir)IAIP-Updater\config.txt" + "$(TargetDir)IAIP-Updater\IAIP.7z" + "$(TargetDir)oci.dll" + "$(TargetDir)Oracle.DataAccess.dll" + "$(TargetDir)orannzsbb11.dll" + "$(TargetDir)oraociicus11.dll" + "$(TargetDir)OraOps11w.dll" "$(TargetDir)IAIP-Updater\IAIP_update.exe"
copy /b "7zsd_All.sfx" + config.txt + IAIP.7z + ../oci.dll + ../Oracle.DataAccess.dll + ../orannzsbb11.dll + ../oraociicus11.dll + ../OraOps11w.dll IAIP_update.exe
echo Copying updater for users of old IAIP versions...
REM VisualStudio command: copy "$(TargetDir)IAIP-Updater\IAIP_update.exe" "$(TargetDir)IAIP-Updater\IAIP.exe"
copy IAIP_update.exe IAIP.exe
echo Deleting unnecessary files...
REM VisualStudio command: del "$(TargetDir)IAIP-Updater\IAIP.7z"
del IAIP.7z
REM VisualStudio command: del "$(TargetDir)IAIP-Updater\7za.exe"
del 7za.exe
REM VisualStudio command: del "$(TargetDir)IAIP-Updater\7zsd_All.sfx"
del 7zsd_All.sfx
REM VisualStudio command: del "$(TargetDir)IAIP-Updater\config.txt"
del config.txt
REM VisualStudio command: del "$(TargetDir)IAIP-Updater\make-sfx.bat"
del make-sfx.bat
pause