param(
	[string] $version 
)
$scriptpath = $MyInvocation.MyCommand.Path
$build_dir = Split-Path -Path $scriptpath
$squirrel = "$build_dir\..\packages\squirrel.windows.1.4.4\tools\Squirrel.exe"
$release_dir = "$build_dir\..\Releases"


Write-Host $build_dir

# Cleanup packaging directories
Remove-Item "$build_dir\FinalPackage" -Recurse -ErrorAction Ignore
Remove-Item "$build_dir\packaging\lib\net45" -Recurse -ErrorAction Ignore
Remove-Item "$build_dir\packaging\lib" -ErrorAction Ignore
Remove-Item "$build_dir\packaging" -ErrorAction Ignore

$ReleaseFilePackagingArea = "$build_dir\packaging\lib\net45"
$SourceReleaseFilesDir = "$build_dir\..\Frankentime.WPF\bin\Release"

# Setup the Packaging area for the squirrel nuget package
New-Item $ReleaseFilePackagingArea -Force -ItemType Directory
Copy-Item "$SourceReleaseFilesDir\*.*" $ReleaseFilePackagingArea -Exclude @('*.pdb','*.vshost.*')

# Create squirrel required nuget package
& "$build_dir\nuget.exe" pack "$build_dir\FrankenTime.nuspec" -NoPackageAnalysis -BasePath "$build_dir\packaging" -NonInteractive -Version $version -OutputDirectory $build_dir


.$squirrel -releasify "$build_dir\FrankenTime.$version.nupkg" -releaseDir $release_dir --no-msi
#-setupIcon "$build_dir\sparkleshare-app.ico" -loadingGif "$src_dir\Pixmaps\install-spinner.gif" | Out-Null


$destinationDir = "I:\Dropbox\Workshare\Frankentime"
Copy-Item "$release_dir\*.*" $destinationDir 

##Build a nuget package for entire squirrel Release dir
#New-Item $build_dir\FinalPackage -Force -ItemType Directory
#& "$build_dir\nuget.exe" pack "$build_dir\FrankenTimeSquirrel.nuspec" -NoPackageAnalysis -BasePath "$build_dir\..\Releases" -NonInteractive -nodefaultexcludes -Version $version -OutputDirectory "$build_dir\FinalPackage"
#
#
##Push up to octopus Server  API-SHNNLA8ZQMNKKLJFBWYQOS8AY
#& "$build_dir\nuget.exe" push   "$build_dir\FinalPackage\FrankenTimeSquirrel.$version.nupkg" -ApiKey API-SHNNLA8ZQMNKKLJFBWYQOS8AY -Source http://52.183.38.142:10930/nuget/packages
#

