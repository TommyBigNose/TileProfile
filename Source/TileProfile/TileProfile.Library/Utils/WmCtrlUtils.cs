using System.Diagnostics;
using TileProfile.Library.Models;

namespace TileProfile.Library.Utils;

public static class WmCtrlUtils
{
    // public static string GetCurrentActiveWindowTitle()
    // {
    //     string currentActiveWindowTitle = string.Empty;
    //     var startInfo = new ProcessStartInfo
    //     {
    //         FileName = "wmctrl",
    //         Arguments = $"-l | grep \"$(wmctrl -l | awk -v id=$(xprop -root _NET_ACTIVE_WINDOW | awk -F ' ' '{{print $5}}') '$1==id')\"",
    //         RedirectStandardOutput = true,
    //         RedirectStandardError = true,
    //         UseShellExecute = false,
    //         CreateNoWindow = true
    //     };
    //     using (var xpropProcess = Process.Start(startInfo))
    //     {
    //         string output = xpropProcess.StandardOutput.ReadToEnd();
    //         string error = xpropProcess.StandardError.ReadToEnd();
    //         if (!string.IsNullOrWhiteSpace(output))
    //         {
    //             Console.WriteLine(output);
    //             currentActiveWindowTitle = output.Trim();
    //         }
    //     }
    //
    //     return currentActiveWindowTitle;
    // }
    
    public static void ApplyWindowBox(string windowTitle, WindowBox box)
    {
        try
        {
            var startInfo = new ProcessStartInfo
            {
                FileName = "wmctrl",
                Arguments = $"-r \"{windowTitle}\" -e 0,{box.X},{box.Y},{box.Width},{box.Height}",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };
            using (var xpropProcess = Process.Start(startInfo))
            {
                string output = xpropProcess.StandardOutput.ReadToEnd();
                string error = xpropProcess.StandardError.ReadToEnd();
                if (!string.IsNullOrWhiteSpace(output))
                {
                    Console.WriteLine(output);
                }
                if (!string.IsNullOrWhiteSpace(error))
                {
                    Console.WriteLine($"Error applying window box: {error}");
                }
            }
        }
        catch (Exception ex)
        {
            // Handle exceptions (e.g., access denied or process without a window)
            Console.WriteLine($"ApplyWindowBox: Could not access process: {ex.Message}");
        }
    }
    
    public static List<string> GetActiveWindows()
    {
        List<string> processNames = new ();
        try
        {
            var startInfoHostName = new ProcessStartInfo
            {
                FileName = "hostname",
                Arguments = $"",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };
            
            string hostname = string.Empty;
            
            using (var xpropProcess = Process.Start(startInfoHostName))
            {
                string output = xpropProcess.StandardOutput.ReadToEnd();
                string error = xpropProcess.StandardError.ReadToEnd();
                if (!string.IsNullOrWhiteSpace(output))
                {
                    hostname = output.Trim();
                    // Console.WriteLine($"Hostname: {hostname}");
                }
            }
            
            var startInfo = new ProcessStartInfo
            {
                FileName = "wmctrl",
                Arguments = $"-l",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };
            using (var xpropProcess = Process.Start(startInfo))
            {
                string output = xpropProcess.StandardOutput.ReadToEnd();
                string error = xpropProcess.StandardError.ReadToEnd();
                if (!string.IsNullOrWhiteSpace(output))
                {
                    var newLineList = output.Split('\n');
                    foreach (var line in newLineList)
                    {
                        if (!string.IsNullOrWhiteSpace(line))
                        {
                            // Get index of line after hostname until end of the line
                            int startIndex = line.IndexOf(hostname) + hostname.Length;
                            int endIndex = line.Length - startIndex;
                            string process = line.Substring(startIndex, endIndex);
                            processNames.Add(process);
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Handle exceptions (e.g., access denied or process without a window)
            Console.WriteLine($"GetActiveWindows: Could not access process: {ex.Message}");
        }
        return processNames;
    }
}