using System;
using System.Collections.Generic;
using System.Linq;
using QuantumForge;
using Microsoft.Xna.Framework;
using System.ComponentModel;
using System.Xml.Serialization;
using System.Runtime.Serialization;

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

    //public List<MonoBehaviour> ComponentsSerialized { get; set; } = new List<MonoBehaviour>();

    //[XmlIgnore]
    public List<MonoBehaviour> Components { get; set; } = new List<MonoBehaviour>();

    public void AddComponent(MonoBehaviour component)
    {
        Components.Add(component);
        //ComponentsSerialized.Add(component);
    }

    /*
    public void AddComponentAwake(MonoBehaviour component)
    {
        Logger.LogInfo(component.Name);
        var componentsActual = Components;
        componentsActual.Add(component);
        Components = componentsActual;
        foreach (var item in Components)
        {
            Logger.LogInfo(item.Name); 
        }
    }
    */

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
        /*
        foreach (var serializedComponent in ComponentsSerialized)
        {
            var newComponent = (MonoBehaviour)Activator.CreateInstance(serializedComponent.GetType());
            newComponent.Owner = this;
            AddComponentAwake(newComponent);
        }
        */
    }


    public Entity(string param)
    {
        //AddComponent(new Transform(Vector2.Zero));
        Name = "Empty entity!";
        if (param == "NewEntity")
        {
            var newTransform = new Transform(this);
            AddComponent(newTransform);
            //ComponentsSerialized.Add(newTransform);
        }
        /*
        foreach (var serializedComponent in ComponentsSerialized)
        {
            var newComponent = (MonoBehaviour)Activator.CreateInstance(serializedComponent.GetType());
            newComponent.Owner = this;
            AddComponentAwake(newComponent);
        }
        */
    }


    public void RemoveComponent(MonoBehaviour component)
    {
        Components.Remove(component);
        //ComponentsSerialized.Remove(component);
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

    /*
    public void OnDeserialization(object sender)
    {
        // Perform additional initialization after deserialization
        foreach (var component in ComponentsSerialized)
        {
            component.Owner = this;
            Components.Add(component);
        }
    }
    */
}
