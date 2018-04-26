### 
### Common Profile functions for all users
###

$ErrorActionPreference = 'Stop'
Set-StrictMode -Version Latest

$ScriptPath = Split-Path -Parent $MyInvocation.MyCommand.Definition

$SolutionRoot = Split-Path -Parent $ScriptPath

$ToolsPath = Join-Path -Path $SolutionRoot -ChildPath "lib"

Export-ModuleMember -Variable @('ScriptPath', 'SolutionRoot', 'ToolsPath')

function Install-Chocolatey()
{
	if(-not $env:ChocolateyInstall -or -not (Test-Path "$env:ChocolateyInstall"))
	{
		Write-Output "Chocolatey Not Found, Installing..."
		iex ((new-object net.webclient).DownloadString('http://chocolatey.org/install.ps1')) 
	}
}

function Install-Psake()
{
	if(!(Test-Path $env:ChocolateyInstall\lib\Psake\tools\Psake*)) 
	{ 
		choco install psake -y
	}
	
}

function Install-Git()
{
	if(!((Test-Path ${env:ProgramFiles(x86)}\Git*) -Or (Test-Path ${env:ProgramFiles}\Git*))) 
	{ 
		choco install git.install	
	}
	$env:Path += ";${env:ProgramFiles(x86)}\Git"
	$env:Path += ";${env:ProgramFiles}\Git"
}

function Install-DocFx()
{
	if(!(Test-Path $env:ChocolateyInstall\lib\docfx\tools*)) 
	{ 
		choco install docfx	
	}
	$env:Path += ";$env:ChocolateyInstall\lib\docfx\tools"
}

Export-ModuleMember -Function *-*