:: Mute console
@echo off
:: Sets scriptPath to the absolute path of the PowerShell script
:: %~dp0 is the folder this batch file is located
set scriptPath=%~dp0OpenCspLoginBonusPage.ps1
:: powershell: Invoke PowerShell from the batch file
:: -ExecutionPolicy: temporarily override its execution policy
:: Bypass: allow unsigned scripts' execution
:: -File "": Specify the path of the PowerShell script to run
powershell -ExecutionPolicy Bypass -File "%scriptPath%"