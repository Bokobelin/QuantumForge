using QuantumForge;
using SharpDX.Direct2D1.Effects;
using System;
using System.Diagnostics;
using System.Threading;
using System.Windows;

public static class Program
{
    public static StylesGetter app;
    public static MainWindow window = new MainWindow();
    public static LoadingScreen loadScreen;
    public static Thread gameThread;

    public static string VERSION = "0.1.0";
    public static string CONFIGURATION = $"{GetConfiguration()}";

    private static string GetConfiguration()
    {
#if DEBUG
        return "Debug";
#elif RELEASE
        return "Release";
#else
        return "Pirate version";
#endif
    }


    [STAThread]
    static void Main()
    {
        // Create the WPF application and main window
        app = new();
        window = new MainWindow();
        loadScreen = new LoadingScreen();

        // Start the game loop in a separate thread
        /*
        gameThread = new Thread(new ThreadStart(GameLoop));
        gameThread.Start();
        */

        // Run the WPF application and show the main window
        try
        {
            Application.Current.MainWindow = window;
            Application.Current.Run();
        }
        catch(Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
    }

    public static void GameLoop()
    {
        using var game = new QuantumForge.Main();
        game.Run();
    }
}
