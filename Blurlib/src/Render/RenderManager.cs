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

        private List<IDraw> _toAdd;
        private List<IDraw> _toRemove;

        public RenderManager()
        {
            _drawList = new SortedSet<IDraw>(new ZIndexComparer());
            _toRemove = new List<IDraw>();

            ClearColor = Color.ForestGreen;
        }
        
        // -TODO-: Finish method

        public bool Add(IDraw drawable)
        {
            if (!_drawList.Contains(drawable) && drawable.Visible)
            {
                _toAdd.Add(drawable);
                return true;
            }

            return false;
        }

        public bool Remove(IDraw drawable)
        {
            if (_drawList.Contains(drawable))
            {
                _toRemove.Add(drawable);
                return true;
            }
            return false;
        }

        public bool Remove(IEnumerable<IDraw> drawables)
        {
            // If a component is not added we return false
            bool getOneFalse = true;

            foreach (IDraw drawable in drawables)
                if (!Remove(drawable))
                    getOneFalse = false;
            return getOneFalse;
        }

        public bool AddComponent(Component component)
        {
            if (component is IDraw)
            {
                return Add(component as IDraw);
            }
            return false;
        }

        public bool RemoveComponent(Component component)
        {
            if (component is IDraw)
            {
                return Remove(component as IDraw);
            }
            return false;
        }

        

        public bool AddEntity(Entity entity)
        {
            throw new NotImplementedException();
        }



        public void Initialize()
        {

        }

        public void Update()
        {
            if (_toAdd.Count > 0)
            {
                _drawList.Add(_toAdd);
                _toAdd.Clear();
            }

            if (_toRemove.Count > 0)
            {
                foreach (IDraw drawable in _toRemove)
                {
                    drawable.Visible = false;
                    _drawList.Remove(drawable);
                }
                _toRemove.Clear();
            }
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
                else
                {
                    _toRemove.Add(drawable);
                }
            }

            spriteBatch.End();
        }
    }
}
