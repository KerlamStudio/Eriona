using Blurlib.Render;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Blurlib.Util;

namespace Blurlib.ECS.Components
{
    public class Sprite : Component, IDraw
    {
        public Sprite(Texture2D texture, bool visible = true, int zIndex = 0, Rectangle? textureClip = null, Vector2? textureLocalTranslate = null, Color? textureColorFilter = null) : base(false, visible, false)
        {
            Texture = texture;
            ZIndex = zIndex;
            TextureClip = textureClip;
            TextureLocalTranslate = textureLocalTranslate.IsNull() ? Vector2.Zero : textureLocalTranslate.Value;
            TextureColorFilter = textureColorFilter.IsNull() ? Color.White : textureColorFilter.Value;
        }

        public int ZIndex { get; set; }
        
        public Texture2D Texture { get; set; }

        public Rectangle? TextureClip { get; set; }

        public Vector2 TexturePosition => WorldPosition;

        public Vector2 TextureLocalTranslate { get; set; }

        public Color TextureColorFilter { get; set; }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture,
                            TexturePosition + TextureLocalTranslate,
                            TextureClip,
                            TextureColorFilter);
        }
    }
}
