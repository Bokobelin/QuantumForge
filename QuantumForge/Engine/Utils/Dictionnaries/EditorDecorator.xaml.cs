using System.Windows;
using System.Windows.Controls;

namespace QuantumForge.Engine.Utils.Dictionnaries
{
    public partial class EditorDecorator : ResourceDictionary
    {
        private void OnCloseButton_Click(object sender, RoutedEventArgs e)
        {
            Logger.LogInfo("Closing editor...");
            var window = (Window)((FrameworkElement)sender).TemplatedParent;
            window.Close();
        }

        private void OnRestoreButton_Click(object sender, RoutedEventArgs e)
        {
            var window = (Window)((FrameworkElement)sender).TemplatedParent;
            if(window.WindowState == WindowState.Maximized)
            {
                window.WindowState = WindowState.Normal;
            }
            else
            {
                window.WindowState = WindowState.Maximized;
            }
        }

        private void OnMinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            var window = (Window)((FrameworkElement)sender).TemplatedParent;
            window.WindowState = WindowState.Minimized;
        }
    }
}
