using System.Collections.ObjectModel;
using System.ComponentModel;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Windows.Input;

[Serializable]
public class Project : INotifyPropertyChanged, ISerializable
{
    public static string Extension { get; } = ".qf";
    public string Name;
    public string Path;
    public string FullPath => $"{Path}{Name}{Extension}";

    [NonSerialized] // Mark the field as non-serializable
    private List<Scene> _scenes;

    [field: NonSerialized] // Mark the event as non-serializable
    public event PropertyChangedEventHandler PropertyChanged;

    public List<Scene> Scenes
    {
        get { return _scenes; }
        set
        {
            if (_scenes != value)
            {
                _scenes = value;
                OnPropertyChanged(nameof(Scenes));
            }
        }
    }

    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        // Serialize essential fields here
        info.AddValue("Scenes", _scenes);
        // Add other fields as needed
    }

    private Scene _selectedScene;

    public Scene SelectedScene
    {
        get { return _selectedScene; }
        set
        {
            if (_selectedScene != value)
            {
                _selectedScene = value;
                OnPropertyChanged(nameof(SelectedScene)); // Notify UI about the change
            }
        }
    }


    public Project()
    {
        _scenes = new List<Scene>();
        //Scenes.Add(new Scene());
        RemoveSceneCommand = new RelayCommand(RemoveScene, CanRemoveScene);
        _removeEntityCommand = new RelayCommand(RemoveEntity);
    }

    public void AddScene(Scene scene)
    {
        List<Scene> newScenes = new List<Scene>(Scenes);
        newScenes.Add(scene);
        Scenes = newScenes;
        SelectedScene = scene;
    }

    public ICommand RemoveSceneCommand { get; }
    private RelayCommand _removeEntityCommand;

    public RelayCommand RemoveEntityCommand
    {
        get
        {
            return _removeEntityCommand ?? (_removeEntityCommand = new RelayCommand(RemoveEntity));
        }
    }


    private void RemoveScene(object parameter)
    {
        if (parameter is Scene sceneToRemove)
        {
            RemoveScene(sceneToRemove);
        }
    }

    private void RemoveEntity(object parameter)
    {
        // Implement logic to remove the selected entity from the scene
        if (SelectedScene != null && SelectedScene.SelectedEntity != null)
        {
            SelectedScene.RemoveEntity(SelectedScene.SelectedEntity);
        }
    }

    private bool CanRemoveScene(object parameter)
    {
        // Add any conditions for when the RemoveSceneCommand can be executed
        return true;
    }

    public void RemoveScene(Scene scene)
    {
        List<Scene> newScenes = new List<Scene>(Scenes);
        newScenes.Remove(scene);
        Scenes = newScenes;
    }

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
