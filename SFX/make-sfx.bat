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
REM VisualStudio command: copy /b "$(TargetDir)SFX\7zsd_All.sfx" + "$(TargetDir)SFX\config.txt" + "$(TargetDir)SFX\IAIP.7z" "$(TargetDir)SFX\IAIP.exe"
copy /b "7zsd_All.sfx" + config.txt + IAIP.7z IAIP.exe
echo Deleting zip file...
REM VisualStudio command: del "$(TargetDir)SFX\IAIP.7z"
del IAIP.7z
pause