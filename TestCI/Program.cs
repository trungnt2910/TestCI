using System.Diagnostics;
using System.Reflection;

namespace TestCI
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var _runtimeFolder = Path.Combine(Path.GetTempPath(), $"observationframework-{Guid.NewGuid()}");
            var edgeDataFolder = Path.Combine(_runtimeFolder, "edgedata");

            Directory.CreateDirectory(edgeDataFolder);

            File.WriteAllText(Path.Combine(edgeDataFolder, "FirstLaunchAfterInstallation"), "");
            File.WriteAllText(Path.Combine(edgeDataFolder, "Local State"), @"
            {
               ""fre"":{
                  ""has_first_visible_browser_session_completed"":true,
                  ""has_user_committed_selection_to_import_during_fre"":false,
                  ""has_user_completed_fre"":false,
                  ""has_user_seen_fre"":true,
                  ""last_seen_fre"":""106.0.1370.47"",
                  ""oem_bookmarks_set"":true
               }
            }
            ");

            var process = Process.Start(new ProcessStartInfo()
            {
                FileName = "cmd.exe",
                Arguments = $"/K \"msedge.exe --user-data-dir={edgeDataFolder} --auto-open-devtools-for-tabs --disable-extensions \"https://example.com\"\"",
                UseShellExecute = false,
            });

            if (process == null)
            {
                throw new Exception("Cannot launch Microsoft Edge.");
            }

            process.WaitForExit();
        }
    }
}