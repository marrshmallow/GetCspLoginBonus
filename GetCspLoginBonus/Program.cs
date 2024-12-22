using System.Diagnostics;

// Base directory for resolving paths
string baseDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../..");
string batchFilePath = Path.Combine(baseDirectory, "Scripts", "CreateShortcut.bat");

// Debug: Print resolved paths
Console.WriteLine($"Base Directory: {baseDirectory}");
Console.WriteLine($"Batch File Path: {batchFilePath}");

// Check if the batch file exists
if (!File.Exists(batchFilePath))
{
    Console.WriteLine("No batch file found.");
    return;
}

try
{
    var processInfo = new ProcessStartInfo
    {
        FileName = batchFilePath,
        WorkingDirectory = Path.Combine(baseDirectory, "Scripts"),
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
        Console.WriteLine($"Batch file output: {output}");

        if (!string.IsNullOrWhiteSpace(error))
        {
            Console.WriteLine($"Error: {error}");
        }
    }
    else
    {
        Console.WriteLine("Process is null.");
    }
}
catch (Exception e)
{
    Console.WriteLine($"Error Starting Process: {e.Message}");
}