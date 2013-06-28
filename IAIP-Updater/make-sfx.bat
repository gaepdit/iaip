@echo off
cls
echo The commands in this batch file should be included (with full paths)
echo in the VisualStudio project's post-build event command line and executed 
echo after a successful build. This batch file is only here as backup and is 
echo safe to run.
echo
echo Creating updater zip file...
REM VisualStudio command: "$(TargetDir)IAIP-Updater\7za.exe" a "$(TargetDir)IAIP-Updater\IAIP_Update.7z" "$(TargetDir)JohnGaltProject.exe" -mx=7
7za.exe a IAIP_Update.7z ../JohnGaltProject.exe -mx=7
echo
echo Creating alternate updater for users of old versions (prior to v2.6.2.11)...
REM VisualStudio command: "$(TargetDir)IAIP-Updater\7za.exe" a "$(TargetDir)IAIP-Updater\IAIP.7z" "$(TargetDir)JohnGaltProject.exe" "$(TargetDir)oci.dll" "$(TargetDir)Oracle.DataAccess.dll" "$(TargetDir)orannzsbb11.dll" "$(TargetDir)oraociicus11.dll" "$(TargetDir)OraOps11w.dll" -mx=7
7za.exe a IAIP.7z ../JohnGaltProject.exe ../oci.dll ../Oracle.DataAccess.dll ../orannzsbb11.dll ../oraociicus11.dll ../OraOps11w.dll  -mx=7
echo
echo Creating self-extracting updater file from zip file...
REM VisualStudio command: copy /b "$(TargetDir)IAIP-Updater\7zsd_All.sfx" + "$(TargetDir)IAIP-Updater\config.txt" + "$(TargetDir)IAIP-Updater\IAIP_Update.7z" "$(TargetDir)IAIP-Updater\IAIP_Update.exe"
copy /b "7zsd_All.sfx" + config.txt + IAIP_Update.7z IAIP_Update.exe
echo
echo Creating alternate self-extracting updater...
REM VisualStudio command: copy /b "$(TargetDir)IAIP-Updater\7zsd_All.sfx" + "$(TargetDir)IAIP-Updater\config.txt" + "$(TargetDir)IAIP-Updater\IAIP.7z" "$(TargetDir)IAIP-Updater\IAIP.exe"
copy /b "7zsd_All.sfx" + config.txt + IAIP.7z IAIP.exe
echo
echo Deleting unnecessary files...
REM VisualStudio command: del "$(TargetDir)IAIP-Updater\IAIP_Update.7z"
del IAIP_Update.7z
REM VisualStudio command: del "$(TargetDir)IAIP-Updater\IAIP.7z"
del IAIP.7z
REM VisualStudio command: del "$(TargetDir)IAIP-Updater\7za.exe"
del 7za.exe
REM VisualStudio command: del "$(TargetDir)IAIP-Updater\7zsd_All.sfx"
del 7zsd_All.sfx
REM VisualStudio command: del "$(TargetDir)IAIP-Updater\config.txt"
del config.txt
REM VisualStudio command: del "$(TargetDir)IAIP-Updater\make-sfx.bat"
echo
echo (Batch file not deleted.)
pause