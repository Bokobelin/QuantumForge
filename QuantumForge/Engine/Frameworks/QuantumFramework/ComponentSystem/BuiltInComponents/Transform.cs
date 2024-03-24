using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Xml.Serialization;

namespace QuantumForge
{
    [Serializable]
    [XmlInclude(typeof(Transform))]
    public class Transform : MonoBehaviour
    {

        public Vector2 position;
        public int rotation;

        [XmlIgnore] // Exclude Owner from XML serialization
        public override Entity Owner { get => base.Owner; set => base.Owner = value; }

        [XmlIgnore]
        public new Dictionary<string, object> Fields { get; set; } = new Dictionary<string, object>();

        public Transform(Entity owner, Vector2 _position)
        {
            Owner = owner;
            position = _position;
            Name = "Transform";
            PopulateFields();
        }

        public Transform(Entity owner, Vector2 _position, int _rotation)
        {
            Owner = owner;
            position = _position;
            rotation = _rotation;
            Name = "Transform";
            PopulateFields();
        }

        public Transform(Entity owner)
        {
            Owner = owner;
            position = Vector2.Zero;
            Name = "Transform";
            PopulateFields();
        }

        public Transform()
        {
            position = Vector2.Zero;
            Name = "Transform";
            PopulateFields();
        }

        public override void PopulateFields()
        {
            base.PopulateFields();
            Type type = this.GetType();
            FieldInfo[] publicFields = type.GetFields(BindingFlags.Public | BindingFlags.Instance);

            foreach (FieldInfo field in publicFields)
            {
                Fields[field.Name] = field.GetValue(this);
            }
        }
    }
}
