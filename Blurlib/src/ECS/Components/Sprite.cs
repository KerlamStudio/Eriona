using Blurlib.Render;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Blurlib.ECS.Components
{
    public class Sprite : Component, IDraw
    {
        public Sprite(bool active = false, bool visible = false, bool collidable = false) : base(active, visible, collidable)
        {

        }

        public int ZIndex => 1;

        public Texture2D Ttexture;
        public Texture2D Texture => Ttexture;

        public Rectangle? TextureClip => null;

        public Vector2 TexurePosition => Vector2.Zero;

        public Vector2 TextureLocalTranslate => Vector2.Zero;

        public Color TextureColorFilter => Color.White;
    }
}
