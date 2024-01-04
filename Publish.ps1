# Config

$configuration = "Debug"
$projectDir = "IAIP"
$publishDir = Join-Path -Path ".publish" -ChildPath $configuration

# Display configuration before proceeding.
Write-Output "`n"
Write-Output "=== Current configuration: $configuration ==="
Write-Output "`n"
Pause

# Find MSBuild.
$msBuildPath = & "${env:ProgramFiles(x86)}\Microsoft Visual Studio\Installer\vswhere.exe" `
-latest -requires Microsoft.Component.MSBuild -find MSBuild\**\Bin\MSBuild.exe `
-prerelease | select-object -first 1
Write-Output "`n"
Write-Output "=== MSBuild location: $((Get-Command $msBuildPath).Path)"

# Remove previous application files.
Write-Output "`n"
Write-Output "=== Removing previous files"
Push-Location $projectDir
if (Test-Path $publishDir) {
    Remove-Item -Path $publishDir -Recurse
}
Write-Output "!== ... done removing previous files"

# Build & create publish files.
Write-Output "`n"
Write-Output "=== Restoring"
& $msBuildPath -t:restore -p:Configuration=$configuration -p:Platform=x86 -v:m
Write-Output "!== ... done restoring"

Write-Output "`n"
Write-Output "=== Building"
& $msBuildPath -t:rebuild -p:Configuration=$configuration -p:Platform=x86 -v:m
Write-Output "!== ... done building"

Write-Output "`n"
Write-Output "=== Creating publish files"
& $msBuildPath -t:publish -p:Configuration=$configuration -p:Platform=x86 -p:PublishDir=$publishDir -v:m
Write-Output "!== ... done creating publish files"

# Measure publish size.
$publishSize = (Get-ChildItem -Path "$publishDir/Application Files" -Recurse | Measure-Object -Property Length -Sum).Sum / 1Mb
Write-Output "`n"
Write-Output ("=== Published size: {0:N2} MB ===" -f $publishSize)
Pop-Location
