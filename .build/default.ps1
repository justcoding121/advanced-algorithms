$PSake.use_exit_on_error = $true

$Here = "$(Split-Path -parent $MyInvocation.MyCommand.Definition)"

$SolutionRoot = (Split-Path -parent $Here)

$ProjectName = "Advanced.Algorithms"

$SolutionFile = "$SolutionRoot\$ProjectName.sln"

## This comes from the build server iteration
if(!$BuildNumber) { $BuildNumber = $env:APPVEYOR_BUILD_NUMBER }
if(!$BuildNumber) { $BuildNumber = "0"}

## The build configuration, i.e. Debug/Release
if(!$Configuration) { $Configuration = $env:Configuration }
if(!$Configuration) { $Configuration = "Release" }

if(!$Version) { $Version = $env:APPVEYOR_BUILD_VERSION }
if(!$Version) { $Version = "0.0.$BuildNumber" }

if(!$Branch) { $Branch = $env:APPVEYOR_REPO_BRANCH }
if(!$Branch) { $Branch = "local" }

if($Branch -eq "beta" ) { $Version = "$Version-beta" }

Import-Module "$Here\Common" -DisableNameChecking

$NuGet = Join-Path $SolutionRoot ".nuget\nuget.exe"

$MSBuild = "${env:ProgramFiles(x86)}\Microsoft Visual Studio\2017\Community\MSBuild\15.0\Bin\msbuild.exe"
$MSBuild -replace ' ', '` '


FormatTaskName (("-"*25) + "[{0}]" + ("-"*25))

Task default -depends  Document

Task Document {
	docfx docfx.json
	$TEMP_REPO_DIR=(Split-Path -parent $SolutionRoot) + "\temp-repo-clone"

	git clone https://github.com/justcoding121/advanced-algorithms.git --branch master $TEMP_REPO_DIR
	cd "$TEMP_REPO_DIR\docs"
	git rm -r *
	
	cp -r "$SolutionRoot\docs\* ."
	
	git config --global credential.helper store
	Add-Content "$env:USERPROFILE\.git-credentials" "https://$($env:github_access_token):x-oauth-basic@github.com`n"
	git config --global user.email $env:github_email
	git config --global user.name "justcoding121"
	git add . -A
	git commit -m "Update generated documentation"
	git push origin master
	
	cd $Here
}


Task Install-MSBuild {
    if(!(Test-Path $MSBuild)) 
    { 
        cinst microsoft-build-tools -y
    }
}

Task Install-BuildTools -depends Install-MSBuild