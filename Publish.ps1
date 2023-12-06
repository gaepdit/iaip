# Config

$configuration = "UAT"
$projectDir = "IAIP"
$publishDir = Join-Path -Path "_publish" -ChildPath $configuration

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
Write-Output "=== MSBuild: $((Get-Command $msBuildPath).Path)"

# Remove previous application files.
Write-Output "`n"
Write-Output "=== Removing previous files"
Push-Location $projectDir
if (Test-Path $publishDir) {
    Remove-Item -Path $publishDir -Recurse
}
Pop-Location

# Build & publish.
Write-Output "`n"
Write-Output "=== Restoring"
& $msBuildPath -target:restore -property:Configuration=$configuration -v:m
Write-Output "`n"
Write-Output "=== Building"
& $msBuildPath -target:rebuild -property:Configuration=$configuration -v:m
Write-Output "`n"
Write-Output "=== Publishing"
& $msBuildPath -target:publish -p:Configuration=$configuration -p:PublishDir=$publishDir -v:m

# Measure publish size.
Push-Location $projectDir
$publishSize = (Get-ChildItem -Path "$publishDir/Application Files" -Recurse | Measure-Object -Property Length -Sum).Sum / 1Mb
Write-Output "`n"
Write-Output ("=== Published size: {0:N2} MB" -f $publishSize)
Pop-Location
