using System;
using System.Diagnostics;
using System.Windows.Media;

namespace QuantumForge
{
    public static class Logger
    {
        private static MainWindow mainWindow;

        public static void Initialize(MainWindow window)
        {
            mainWindow = window;
        }

        public static void LogInfo(string message)
        {
            //if (mainWindow != null)
            //{
                Debug.WriteLine(message);
                Program.window.AddLog("[INFO] " + message, Brushes.Blue);
            //}
        }

        [STAThread]
        public static void LogWarn(string message)
        {
            //if (mainWindow != null)
            //{
                Debug.WriteLine(message);
                Program.window.AddLog("[WARN] " + message, Brushes.Yellow);
            //}
            //else
            //{
            //    Debug.WriteLine("ERROR : mainWindow is null !");
            //}
        }

        public static void LogError(string message)
        {
            //if (mainWindow != null)
            //{
                Debug.WriteLine(message);
                Program.window.AddLog("[ERROR] " + message, Brushes.Red);
            //}
        }

        public static void ClearLogs()
        {
            Program.window.ClearLogs();
        }
    }
}
