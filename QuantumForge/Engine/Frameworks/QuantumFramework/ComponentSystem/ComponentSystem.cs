using QuantumForge;
using QuantumForge.Engine;
using System.Collections.Generic;

public static class ComponentSystem
{
    private static List<IComponent> components = new List<IComponent>();

    public static void AddComponent(IComponent component)
    {
        components.Add(component);
    }

    public static void Update()
    {
        foreach (var component in components)
        {
            component.Update();
        }
    }

    public static void Draw()
    {
        foreach (var component in components)
        {
            if (component is IRenderComponent renderComponent)
            {
                renderComponent.Draw();
            }
        }
    }
}
