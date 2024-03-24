using QuantumForge.Engine.Editor.Serialization;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
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
using System.Xml.Serialization;
using System.Timers;
using System; // Add this namespace for Timer functionality

namespace QuantumForge.Engine.Editor.EditorViews
{
    /// <summary>
    /// Logique d'interaction pour Hierarchy.xaml
    /// </summary>
    public partial class Hierarchy : UserControl
    {
        public static Project _dataContext;
        private Timer timer; // Declare a Timer object

        public Hierarchy()
        {
            _dataContext = LoadProjectFromXml("Content/Projects/TestProject1.xml");  // Load your project from XML here or set it to a new instance

            // Check if the DataContext is set
            if (_dataContext != null)
            {
                DataContext = _dataContext;
            }
            else
            {
                // Handle the case where loading the project fails or create a new project
                DataContext = new Project();
            }

            InitializeComponent();

            // Initialize the timer
            timer = new Timer();
            timer.Interval = 500; // Set the interval to 100 milliseconds
            timer.Elapsed += Timer_Elapsed; // Set the event handler for the Elapsed event
            timer.AutoReset = true; // Set AutoReset to true for continuous execution
            timer.Start(); // Start the timer
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Program.app.Dispatcher.Invoke(() =>
            {
                // Save the selected entity
                var savedSelectedEntity = entityListBox.SelectedItem;

                // Clear and reset the ItemsSource to trigger a refresh
                entityListBox.ItemsSource = null;
                entityListBox.ItemsSource = ((DataContext as Project)?.SelectedScene?.GameObjects);

                // Restore the selected entity if it still exists in the list
                if (savedSelectedEntity != null && entityListBox.Items.Contains(savedSelectedEntity))
                {
                    entityListBox.SelectedItem = savedSelectedEntity;
                }
            });
        }


        private void AddScene(object sender, RoutedEventArgs e)
        {
            _dataContext = DataContext as Project;
            if (_dataContext is Project project)
            {
                var newScene = new Scene("New Scene");

                // Call the AddScene method
                project.AddScene(newScene);

                _dataContext.SelectedScene = (newScene as Scene);

                //Logger.LogInfo($"Created new scene : {newScene.Name}");
            }
            DataContext = _dataContext as Project;
        }

        private void OnAddGameObjectsClick(object sender, RoutedEventArgs e)
        {
            _dataContext = DataContext as Project;
            // Get the DataContext as a Project
            if (_dataContext is Project project)
            {
                // Ensure a scene is selected
                if (project.SelectedScene != null)
                {
                    project.SelectedScene.AddEntity(new Entity("NewEntity"));
                    // Add GameObjects to the selected Scene

                    if (sceneListBox.SelectedItem is Scene selectedScene)
                    {
                        // Set the SelectedScene property of the Project class
                        project.SelectedScene = selectedScene;
                    }
                    else
                    {
                        Logger.LogWarn("# ! #");
                    }
                    // You can add more GameObjects as needed
                }
                else
                {
                    // Notify the user that no scene is selected
                    MessageBox.Show("Please select a scene before adding GameObjects.");
                }
                DataContext = _dataContext as Project;
            }
            else
            {
                Logger.LogError("HIERARCHY DATACONTEXT NOT SET TO A PROJECT !");
            }
            DataContext = _dataContext as Project;
        }



        private void OnSceneSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            if (sender is ListBox listBox && listBox.SelectedItem is Scene selectedScene)
            {
                _dataContext = DataContext as Project;
                if (DataContext is Project project)
                {
                    // Set the SelectedScene property of the Project class
                    project.SelectedScene = selectedScene;
                    DataContext = _dataContext as Project;
                }
                else
                {
                    Logger.LogError($"{_dataContext.Name} is not a valid project !");
                }
            }
            else
            {
                Logger.LogError("OnSceneSelectionChanged method owner is not a ListBox or ot does not have a SelectedItem who is a Scene.");
            }
        }


        private void OnEntitySelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ListBox listBox && listBox.SelectedItem is Entity selectedEntity)
            {
                _dataContext = DataContext as Project;
                if (DataContext is Project project)
                {
                    // Set the SelectedScene property of the Project class
                    project.SelectedScene.SelectedEntity = selectedEntity;
                    DataContext = _dataContext as Project;
                }
            }
        }

        private void OnSceneRadioButtonClick(object sender, RoutedEventArgs e)
        {
            if (sender is RadioButton radioButton && radioButton.DataContext is Scene scene)
            {
                if (DataContext is Project project)
                {
                    project.SelectedScene = scene;
                }
            }
        }

        public void SaveProject(object sender, RoutedEventArgs e)
        {
            ProjectSerializer.SaveProjectToXml(DataContext as Project, "Content/Projects/TestProject1.xml");
        }

        public static Project LoadProjectFromXml(string filePath)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Project));

                using (FileStream stream = new FileStream(filePath, FileMode.Open))
                {
                    return (Project)serializer.Deserialize(stream);
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., file not found, invalid XML, etc.)
                Console.WriteLine($"Error loading project from XML: {ex.Message}");
                //Logger.LogError($"Error loading project from XML: {ex.Message}");
                return null;
            }
        }

    }
}
