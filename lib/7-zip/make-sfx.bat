@echo off
cls
7za.exe a IAIP.7z JohnGaltProject.exe
copy /b "7zsd_All.sfx" + config.txt + IAIP.7z IAIP.exe
pause