namespace GetCspLoginBonus;

using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

internal static class Program
{
    private static readonly string PermanentPath = AppDomain.CurrentDomain.BaseDirectory;
    
    [STAThread]
    private static void Main()
    {
        SetupFiles();
            
        // Base directory for resolving paths
        string ps1Path = Path.Combine(PermanentPath, "CreateShortcut.ps1");
        RunPowerShellScript(ps1Path);
        DeleteFile(ps1Path);
        DeleteFile(Process.GetCurrentProcess().MainModule?.FileName);
    }

    private static void DeleteFile(string? filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
        {
            return;
        }

        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
    }

    private static void SetupFiles()
    {
        // Extract resources
        string shortcutCreatorPath = Path.Combine(PermanentPath, "CreateShortcut.ps1");
        string pageOpenerPath = Path.Combine(PermanentPath, "OpenCspLoginBonusPage.ps1");
        string pageOpenerWrapperPath = Path.Combine(PermanentPath, "OpenCspLoginBonusPage.bat");
        string iconPath = Path.Combine(PermanentPath, "icon.ico");
        ExtractResource("GetCspLoginBonus.Scripts.CreateShortcut.ps1", shortcutCreatorPath);
        ExtractResource("GetCspLoginBonus.Scripts.OpenCspLoginBonusPage.ps1", pageOpenerPath);
        ExtractResource("GetCspLoginBonus.Scripts.OpenCspLoginBonusPage.bat", pageOpenerWrapperPath);
        ExtractResource("GetCspLoginBonus.Icon.icon.ico", iconPath);
        HideFile(shortcutCreatorPath);
        HideFile(pageOpenerPath);
        HideFile(pageOpenerWrapperPath);
        HideFile(iconPath);
        Console.WriteLine("Successfully extracted all resources.");
    }

    private static void HideFile(string iconPath)
    {
        File.SetAttributes(iconPath, FileAttributes.Hidden);
    }

    private static void ExtractResource(string resourceName, string outputPath)
    {
        var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName);
        using var fileStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write);
        stream?.CopyTo(fileStream);
    }

    private static void RunPowerShellScript(string scriptPath)
    {
        var processInfo = new ProcessStartInfo
        {
            FileName = "powershell.exe",
            Arguments = $"-ExecutionPolicy Bypass -File \"{scriptPath}\"",
            UseShellExecute = false,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            CreateNoWindow = true
        };

        using var process = Process.Start(processInfo);
        if (process != null)
        {
            string output = process.StandardOutput.ReadToEnd();
            string error = process.StandardError.ReadToEnd();
            process.WaitForExit();
        }
    }
}