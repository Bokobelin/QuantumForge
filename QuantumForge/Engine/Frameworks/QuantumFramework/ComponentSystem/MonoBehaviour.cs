using System;
using System.Collections.Generic;
using System.Reflection;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;

namespace QuantumForge
{
    [Serializable]
    [XmlInclude(typeof(Transform))]
    public class MonoBehaviour
    {
        public static List<MonoBehaviour> instances = new List<MonoBehaviour>();


        // Dictionary to store the fields
        [XmlIgnore]
        public Dictionary<string, object> Fields { get; set; } = new Dictionary<string, object>();

        // Method to add or update a field
        public void SetField(string fieldName, object value)
        {
            if (Fields.ContainsKey(fieldName))
            {
                Fields[fieldName] = value;
            }
            else
            {
                Fields.Add(fieldName, value);
            }
        }

        // Method to retrieve a field
        public object GetField(string fieldName)
        {
            if (Fields.ContainsKey(fieldName))
            {
                return Fields[fieldName];
            }
            else
            {
                throw new ArgumentException($"Field '{fieldName}' does not exist.");
            }
        }

        // Method to populate fields dictionary with public fields
        public virtual void PopulateFields()
        {
            Type type = this.GetType();
            FieldInfo[] publicFields = type.GetFields(BindingFlags.Public | BindingFlags.Instance);

            foreach (FieldInfo field in publicFields)
            {
                Fields[field.Name] = field.GetValue(this);
            }
        }

        public virtual string Name { get; set; }

        // Other members and methods remain unchanged

        [XmlIgnore]
        public virtual Entity Owner { get; set; }

        public MonoBehaviour()
        {
            instances.Add(this);
            PopulateFields();
        }

        public MonoBehaviour(Entity owner)
        {
            instances.Add(this);
            Owner = owner;
            PopulateFields();
        }

        public virtual void Start()
        {
        }

        public virtual void Awake()
        {
        }

        public virtual void Update()
        {
        }

        public virtual void Draw()
        {
        }

        public static void UpdateAll()
        {
            List<MonoBehaviour> instancesCopy = new List<MonoBehaviour>(instances);
            foreach (var instance in instancesCopy)
            {
                instance.Update();
            }
        }

        public static void DrawAll()
        {
            List<MonoBehaviour> instancesCopy = new List<MonoBehaviour>(instances);
            foreach (var instance in instancesCopy)
            {
                instance.Draw();
            }
        }


        public static void AwakeAll()
        {
            List<MonoBehaviour> instancesCopy = new List<MonoBehaviour>(instances);
            foreach (var instance in instancesCopy)
            {
                instance.Awake();
            }
        }

        public static void StartAll()
        {
            List<MonoBehaviour> instancesCopy = new List<MonoBehaviour>(instances);
            foreach (var instance in instancesCopy)
            {
                instance.Start();
            }
        }
    }
}
