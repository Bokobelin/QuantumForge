using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections.ObjectModel;

[Serializable]
public class Scene : INotifyPropertyChanged
{
    private Entity _selectedEntity;
    public Entity SelectedEntity
    {
        get { return _selectedEntity; }
        set
        {
            if (_selectedEntity != value)
            {
                _selectedEntity = value;
                OnPropertyChanged(nameof(SelectedEntity));
            }
        }
    }

    public string Name { get; set; }

    // Use BindingList<Entity> for binding and ObservableCollection<Entity> for serialization
    [field: NonSerialized]
    public ObservableCollection<Entity> GameObjectsObservable { get; set; } = new ObservableCollection<Entity>();

    private List<Entity> _gameObjects;

    // This property is used for serialization
    public List<Entity> GameObjects
    {
        get { return _gameObjects; }
        set
        {
            if (_gameObjects != value)
            {
                _gameObjects = value;
                OnPropertyChanged(nameof(GameObjects));
            }
        }
    }

    private bool _isSelected;

    public bool IsSelected
    {
        get { return _isSelected; }
        set
        {
            if (_isSelected != value)
            {
                _isSelected = value;
                OnPropertyChanged(nameof(IsSelected));
            }
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public Scene(string name)
    {
        Name = name;
        _gameObjects = new List<Entity>();
        GameObjectsObservable = new ObservableCollection<Entity>(_gameObjects);
    }

    public Scene()
    {
        Name = "Default Scene";
        _gameObjects = new List<Entity>();
        GameObjectsObservable = new ObservableCollection<Entity>(_gameObjects);
    }

    public void AddEntity(Entity entity)
    {
        _gameObjects.Add(entity);
        GameObjectsObservable.Add(entity);
    }

    public void RemoveEntity(Entity entity)
    {
        _gameObjects.Remove(entity);
        GameObjectsObservable.Remove(entity);
    }
}
