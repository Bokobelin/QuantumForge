using Microsoft.VisualBasic.Logging;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace QuantumForge
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private ObservableCollection<TextBlock> logs = new ObservableCollection<TextBlock>();

        public ObservableCollection<TextBlock> Logs { get; set; } = new ObservableCollection<TextBlock>();


        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            // Add more logs dynamically if needed
            AddLog("[INFO] Logs initialized !", Brushes.Blue);

            if (Program.CONFIGURATION == "Debug")
            {
                AddLog("[WARN] This is a BETA version  of QuantumForge. This version is not stable.", Brushes.Yellow);
            }

            Dispatcher.Invoke(() =>
            {
                loggerListBox.ItemsSource = Logs; // Set the binding to Logs directly
                // Debug.WriteLine(loggerListBox.Items.Count);
                // Debug.WriteLine(logs.Count);
            });

            Title = $"QuantumForge {Program.VERSION} [{Program.CONFIGURATION}]";
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void AddLog(string logMessage, Brush color)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                TextBlock logTextBlock = new TextBlock();
                logTextBlock.Text = logMessage;
                logTextBlock.Foreground = color;
                Logs.Add(logTextBlock); // Use Logs directly
                                        // No need to manually trigger OnPropertyChanged(nameof(Logs))
            });
        }

        public void ClearLogs()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                Logs.Clear();
            });
        }

        private void LaunchGame(object sender, RoutedEventArgs e)
        {
            Logger.LogInfo($"Game launched at {DateTime.Now.TimeOfDay}");
            Program.GameLoop();
        }

        private void ClearLogsButton_Click(object sender, RoutedEventArgs e)
        {
            ClearLogs();
        }
    }
}
