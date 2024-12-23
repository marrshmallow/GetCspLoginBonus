# Get the directory of the current script
$currentDir = Split-Path -Path $MyInvocation.MyCommand.Definition -Parent

# Define the path to the batch file
$batFilePath = Join-Path -Path $currentDir -ChildPath "OpenCspLoginBonusPage.bat"

# Define the path to the icon
$iconPath = Join-Path -Path $currentDir -ChildPath "icon.ico"

# Define where the shortcut should be created
$shortcutPath = Join-Path -Path $currentDir -ChildPath "LaunchCspLoginBonusPage.lnk"

# Create a WScript.Shell COM object
$WshShell = New-Object -ComObject WScript.Shell

# Create the shortcut
$shortcut = $WshShell.CreateShortcut($shortcutPath)

# Set the target to cmd.exe
$shortcut.TargetPath = "C:\Windows\System32\cmd.exe"

# Add arguments to run the batch file
$shortcut.Arguments = "/c `"$batFilePath`""

# Set the working directory to the batch file's directory
$shortcut.WorkingDirectory = $currentDir

# Set the custom icon
$shortcut.IconLocation = $iconPath

# Save the shortcut
$shortcut.Save()

Write-Host "Shortcut created at: $shortcutPath"
