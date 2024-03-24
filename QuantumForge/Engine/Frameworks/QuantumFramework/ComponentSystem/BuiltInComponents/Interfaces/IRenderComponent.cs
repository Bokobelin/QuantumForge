using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace QuantumForge
{
    public interface IRenderComponent : IComponent
    {
        Texture2D Texture { get; set; }
        Vector2 Position { get; set; }

        void Draw();
    }
}
