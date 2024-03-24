using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using QuantumForge.Engine;
using SharpDX.Direct2D1;
using System.Linq;

namespace QuantumForge
{
    public class SpriteRenderer : MonoBehaviour
    {

        public Texture2D Texture { get; set; }

        public Color color { get; set; } = Color.White;

        public override void Draw()
        {
            if (Texture != null)
            {
                Transform transform;
                if (Owner != null)
                {
                    var transforms = Owner.GetComponents<Transform>();
                    transform = transforms.First();
                }
                else
                {
                    transform = new Transform();
                }
                Constants.spriteBatch.Draw(Texture, transform.position, color);
            }
        }

        public SpriteRenderer(Texture2D texture)
        {
            Texture = texture;
        }
    }
}
