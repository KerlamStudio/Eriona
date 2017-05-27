using Blurlib.Render;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Blurlib.Util;

namespace Blurlib.ECS.Components
{
    public class Sprite : Component, IDraw
    {
        public Sprite(Texture2D texture, bool visible = true, int zIndex = 0, Rectangle? textureClip = null, Vector2? textureOffset = null, Color? textureColorFilter = null) : base(false, visible)
        {
            Texture = texture;
            ZIndex = zIndex;
            TextureClip = textureClip;
            TextureOffset = textureOffset ?? Vector2.Zero;
            TextureColorFilter = textureColorFilter.IsNull() ? Color.White : textureColorFilter.Value;
        }

        public int ZIndex { get; set; }
        
        public Texture2D Texture { get; set; }

        public Rectangle? TextureClip { get; set; }

        public Vector2 TexturePosition => WorldPosition;

        public Vector2 TextureOffset { get; set; }

        public Color TextureColorFilter { get; set; }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture,
                            TexturePosition + TextureOffset,
                            TextureClip,
                            TextureColorFilter);
        }

        public override int GetHashCode()
        {
            int hash = base.GetHashCode();

            hash = hash * 23 + ZIndex.GetHashCode();
            hash = hash * 23 + Texture?.GetHashCode() ?? 0;
            hash = hash * 23 + TextureOffset.GetHashCode();

            return hash;
        }
    }
}
