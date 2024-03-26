using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Threading;

namespace QuantumForge.Engine.Editor.EditorViews
{
    public partial class EntityComponentView : UserControl
    {
#nullable enable
        public Entity? SelectedEntity;
#nullable disable
        public Entity OldSelectedEntity;
        public List<MonoBehaviour> SelectedEntityComponents { get; set; } = new List<MonoBehaviour>();
        public List<MonoBehaviour> OldSelectedEntityComponents { get; set; } = new List<MonoBehaviour>();

        private DispatcherTimer updateTimer;

        public EntityComponentView()
        {
            DataContext = this;
            //SelectedEntity = (Hierarchy._dataContext.SelectedScene.SelectedEntity);
            InitializeComponent();
            DataContext = this;

            // Initialize the timer
            updateTimer = new DispatcherTimer();
            updateTimer.Interval = TimeSpan.FromMilliseconds(1000f); // Set the interval as needed
            updateTimer.Tick += UpdateTimer_Tick;

            // Start the timer
            updateTimer.Start();
        }

        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            // Call the Update method here
            Update();
        }

        public void Update()
        {
            if (Hierarchy._dataContext != null && Hierarchy._dataContext.SelectedScene != null)
            {
                SelectedEntity = Hierarchy._dataContext.SelectedScene.SelectedEntity;
                if (SelectedEntity != null)
                {
                    SelectedEntityComponents = SelectedEntity.Components;
                    if (SelectedEntityComponents != OldSelectedEntityComponents)
                    {
                        UpdateListBox();
                    }
                }
            }
        }

        private void UpdateListBox()
        {
            var selected = listBox.SelectedItem;
            listBox.Items.Clear();
            if (SelectedEntity != null)
            {
                foreach (var component in SelectedEntity.Components)
                {
                    var fields = new ObservableCollection<PropertyViewModel>();

                    foreach (var field in component.GetType().GetFields())
                    {
                        if (field.FieldType == typeof(int) && field.IsPublic)
                        {
                            fields.Add(new PropertyViewModel { Key = field.Name, Value = field.GetValue(component) });
                        }
                    }

                    listBox.Items.Add(new MonoBehaviour(){ Name = component.Name, Fields = fields });

                    // Hook up the TextBox.TextChanged event for each TextBox in the ItemsControl
                    foreach (PropertyViewModel property in fields)
                    {
                        var panel = new StackPanel();
                        panel.Orientation = Orientation.Horizontal;

                        var textBlock = new TextBlock();
                        textBlock.Text = property.Key + ":";
                        panel.Children.Add(textBlock);

                        var textBox = new TextBox();
                        textBox.TextChanged += (sender, e) =>
                        {
                            //Logger.LogInfo("TextBox text changed !");
                            if (int.TryParse(textBox.Text, out int value))
                            {
                                // Get the PropertyViewModel object bound to this TextBox
                                PropertyViewModel prop = textBox.DataContext as PropertyViewModel;
                                if (prop != null)
                                {
                                    // Update the value of the corresponding field using reflection
                                    FieldInfo fieldInfo = component.GetType().GetField(prop.Key);
                                    if (fieldInfo != null)
                                    {
                                        fieldInfo.SetValue(component, value);
                                    }
                                }
                            }
                        };

                        Binding binding = new Binding("Value");
                        binding.Mode = BindingMode.TwoWay;
                        binding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;

                        textBox.SetBinding(TextBox.TextProperty, binding);


                        // Set the DataContext of the TextBox to the PropertyViewModel
                        textBox.DataContext = property;

                        panel.Children.Add(textBox);

                        //var itemControl = listBox.SelectedItem.

                        // Add the StackPanel to the ItemsControl
                        //itemControl.Items.Add(panel);
                    }
                }
            }
            if (listBox.Items.Contains(selected))
            {
                listBox.SelectedItem = selected;
            }
        }

        private void RemoveComponentButton_Click(object sender, RoutedEventArgs e)
        {
            if (listBox.SelectedItem != null && listBox.SelectedItem is { } selectedItem)
            {
                // Remove the selected component
                // Your logic to remove the component goes here
                if (listBox.SelectedItem != null)
                {
                    if (listBox.SelectedItem is MonoBehaviour componentName)
                    {
                        // Find the component by name and remove it
                        //var componentToRemove = SelectedEntityComponents.FirstOrDefault(c => c.Name == componentName);
                        if (componentName != null)
                        {
                            SelectedEntity?.RemoveComponent(componentName);
                            Update(); // Update the UI after removing the component
                        }
                        else
                        {
                            Logger.LogWarn("Component not found!");
                        }
                    }
                    else
                    {
                        Logger.LogWarn("Entities ListBox selected item is invalid !");
                    }
                }
                else
                {
                    Logger.LogWarn("No item selected!");
                }
                // After removing, refresh the list box
                Update();
            }
            else
            {
                Logger.LogWarn("No item selected!");
            }
        }

        private void AddComponentButton_Click(object sender, RoutedEventArgs e)
        {
            SelectedEntity?.AddComponent(new Transform());
        }
    }
}
