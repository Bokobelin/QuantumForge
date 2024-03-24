using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QuantumEditor
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<TextBlock> logs = new List<TextBlock>();
        public MainWindow()
        {
            InitializeComponent();

            // Add more logs dynamically if needed
            AddLog("[INFO] Logs initialized !", Brushes.Blue);

            loggerListBox.ItemsSource = logs;
            Debug.WriteLine(loggerListBox.Items.Count);
            Debug.WriteLine(logs.Count);
        }

        private void AddLog(string logMessage, Brush color)
        {
            TextBlock logTextBlock = new TextBlock();
            logTextBlock.Text = logMessage;
            logTextBlock.Foreground = color;
            logs.Add(logTextBlock);
        }
    }
}
