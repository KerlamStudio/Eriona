using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blurlib.Render
{
    public interface IDraw
    {
        int ZIndex { get; }
        bool Show { get; }
        Texture2D Texture { get; }
        Rectangle TextureClip { get; }
        Vector2 TexurePosition { get; }
        Vector2 TextureLocalTranslate { get; }
        Color TextureColorFilter { get; }
    }
}
