Param(
	[Parameter(Mandatory=$true,Position=1)]
	[string] $WBaseDLLPath,
	[Parameter(Mandatory=$true,Position=2)]
	[string] $UnityPlugins
)
$WBaseDLLName = [System.IO.Path]::GetFileName($WBaseDLL)
Copy-Item $WBaseDLLPath ($UnityPlugins + "/" + $WBaseDLLName)