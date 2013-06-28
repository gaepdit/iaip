@echo off
cls
echo The commands in this batch file should be included (with full paths)
echo in the VisualStudio project's post-build event command line and executed 
echo after a successful build. This batch file is only here as backup and is 
echo safe to run.
echo
echo Creating zip file...
REM VisualStudio command: "$(TargetDir)SFX\7za.exe" a "$(TargetDir)SFX\IAIP.7z" "$(TargetDir)JohnGaltProject.exe"
7za.exe a IAIP.7z ../JohnGaltProject.exe
echo Creating self-extracting file from zip file...
REM VisualStudio command: copy /b "$(TargetDir)SFX\7zsd_All.sfx" + "$(TargetDir)SFX\config.txt" + "$(TargetDir)SFX\IAIP.7z" "$(TargetDir)SFX\IAIP_update.exe"
copy /b "7zsd_All.sfx" + config.txt + IAIP.7z IAIP_update.exe
echo Copying updater for users of old IAIP versions...
REM VisualStudio command: copy "$(TargetDir)SFX\IAIP_update.exe" "$(TargetDir)SFX\IAIP.exe"
copy IAIP_update.exe IAIP.exe
echo Deleting unnecessary files...
REM VisualStudio command: del "$(TargetDir)SFX\IAIP.7z"
del IAIP.7z
REM VisualStudio command: del "$(TargetDir)SFX\7za.exe"
del 7za.exe
REM VisualStudio command: del "$(TargetDir)SFX\7zsd_All.sfx"
del 7zsd_All.sfx
REM VisualStudio command: del "$(TargetDir)SFX\config.txt"
del config.txt
REM VisualStudio command: del "$(TargetDir)SFX\make-sfx.bat"
del make-sfx.bat
pause