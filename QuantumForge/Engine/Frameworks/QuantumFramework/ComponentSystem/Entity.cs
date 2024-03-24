using System;
using System.Collections.Generic;
using System.Linq;
using QuantumForge;
using Microsoft.Xna.Framework;
using System.ComponentModel;

[Serializable]
public class Entity
{
    private string _name = "New Entity";
    public string Name
    {
        get { return _name; }
        set
        {
            if (_name != value)
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public List<MonoBehaviour> Components { get; set; } = new List<MonoBehaviour>();

    public void AddComponent(MonoBehaviour component)
    {
        Components.Add(component);
    }

    public void Update()
    {
        foreach (var component in Components)
        {
            component.Update();
        }
    }

    public void Draw()
    {
        foreach (var component in Components)
        {
            component.Draw();
        }
    }

    public Entity()
    {
        //AddComponent(new Transform(Vector2.Zero));
        Name = "Empty entity!";
    }

    public Entity(string param)
    {
        //AddComponent(new Transform(Vector2.Zero));
        Name = "Empty entity!";
        if(param == "NewEntity")
        {
            AddComponent(new Transform(this));
        }
    }

    public void RemoveComponent(MonoBehaviour component)
    {
        Components.Remove(component);
    }

    public IEnumerable<T> GetComponents<T>() where T : MonoBehaviour
    {
        return Components.OfType<T>();
    }

    public void Rename(string name)
    {
        Name = name;
        OnPropertyChanged(nameof(Name));
    }
}
