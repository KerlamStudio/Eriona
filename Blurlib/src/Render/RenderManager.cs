using Blurlib.ECS;
using Blurlib.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Blurlib.Render
{
    public class RenderManager
    {
        public class ZIndexComparer : IComparer<IDraw>
        {
            public int Compare(IDraw a, IDraw b)
            {
                int result = a.ZIndex.CompareTo(b.ZIndex);

                if (result == 0)
                    return 1;   // Handle equality as beeing greater
                else
                    return result;
            }

            public static ZIndexComparer Comparer = new ZIndexComparer();
        }

        public Color ClearColor;

        private SortedSet<IDraw> _drawList;

        private HashSet<IDraw> _toAdd;
        private HashSet<IDraw> _toRemove;

        public RenderManager()
        {
            _drawList = new SortedSet<IDraw>(new ZIndexComparer());
            _toAdd = new HashSet<IDraw>();
            _toRemove = new HashSet<IDraw>();

            ClearColor = Color.ForestGreen;
        }
        
        // -TODO-: Finish method

        public void Add(IDraw drawable)
        {
            if (!_drawList.Contains(drawable) && !_toAdd.Contains(drawable))
            {
                _toAdd.Add(drawable);
            }            
        }

        public void Remove(IDraw drawable)
        {
            if (_drawList.Contains(drawable) && !_toRemove.Contains(drawable))
            {
                _toRemove.Add(drawable);
            }
        }

        public void Remove(IEnumerable<IDraw> drawables)
        {
            foreach (IDraw drawable in drawables)
            {
                Remove(drawable);
            }
        }

        public void AddComponent(Component component)
        {
            if (component is IDraw)
            {
                Add(component as IDraw);
            }
        }

        public void RemoveComponent(Component component)
        {
            if (component is IDraw)
            {
                Remove(component as IDraw);
            }
        }

        public bool Contain(IDraw drawable)
        {
            throw new NotImplementedException();
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
                _drawList.Remove(_toRemove);
                _toRemove.Clear();
            }
        }

        public void Render(SpriteBatch spriteBatch, bool usePProcess=false)
        {
            GameCore.Instance.GraphicsDevice.Clear(ClearColor);

            spriteBatch.Begin();

            foreach (IDraw drawable in _drawList)
            {
                if (drawable.Visible)
                {
                    try
                    {
                        drawable.Draw(spriteBatch);
                    }
                    catch (Exception e)
                    {
                        "Can't draw an entity".Printl();
                    }
                }
            }
#if DEBUG
            GameCore.Instance.Scene.World.MainLayer.DebugDraw(spriteBatch);
#endif

            spriteBatch.End();
        }
    }
}
