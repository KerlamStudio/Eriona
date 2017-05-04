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

        // -TODO-: Finish add component way
        public bool AddComponent(Component component)
        {
            if (component is IDraw)
            {
                if (!_drawList.Contains(component as IDraw))
                {
                    _drawList.Add(component as IDraw);
                    return true;
                }
            }
            return false;
        }

        public bool RemoveComponent(Component component)
        {
            if (component is IDraw)
            {
                if (_drawList.Contains(component as IDraw))
                {
                    _drawList.Remove(component as IDraw);
                    return true;
                }
            }
            return false;
        }

        public bool AddEntity(Entity entity)
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
                        drawable.TexturePosition + drawable.TextureLocalTranslate, 
                        drawable.TextureClip, 
                        drawable.TextureColorFilter);
                }
            }

            spriteBatch.End();
        }
    }
}
