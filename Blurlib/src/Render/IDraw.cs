using Microsoft.Xna.Framework.Graphics;

namespace Blurlib.Render
{
    public interface IDraw
    {
        int ZIndex { get; }
        bool Visible { get; set; }
        void Draw(SpriteBatch spriteBatch);
    }
}
