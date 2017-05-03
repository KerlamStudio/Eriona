using Blurlib.ECS;
using Blurlib.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blurlib.Render
{
    public class RenderManager
    {
        public class ZIndexComparer : IComparer<IDraw>
        {
            public int Compare(IDraw a, IDraw b)
            {
                return a.ZIndex.CompareTo(b.ZIndex);
            }
        }

        public Color ClearColor;

        private SortedSet<IDraw> _drawList;

        public RenderManager()
        {
            _drawList = new SortedSet<IDraw>(new ZIndexComparer());
            ClearColor = Color.ForestGreen;
        }

        public void Initialize()
        {

        }

        public void AddComponent<T>(T component) where T : IDraw
        {
            _drawList.Add(component);
        }

        public void AddEntity(Entity entity)
        {
            throw new NotImplementedException();
        }

        public void Render(SpriteBatch spriteBatch, bool usePProcess=false)
        {
            spriteBatch.Begin();

            foreach (IDraw drawable in _drawList)
            {
                if (drawable.Visible)
                {
                    spriteBatch.Draw(
                        drawable.Texture, 
                        drawable.TexurePosition + drawable.TextureLocalTranslate, 
                        drawable.TextureClip, 
                        drawable.TextureColorFilter);
                }
            }

            spriteBatch.End();
        }
    }
}
