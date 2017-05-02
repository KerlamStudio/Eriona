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
        private GameCore _gameCore;

        public class ZIndexComparer : IComparer<IDraw>
        {
            public int Compare(IDraw a, IDraw b)
            {
                return a.ZIndex.CompareTo(b.ZIndex);
            }
        }

        private SortedSet<IDraw> _drawList = new SortedSet<IDraw>(new ZIndexComparer());

        public RenderManager(GameCore gameCore)
        {
            _gameCore = gameCore;
        }

        public bool AddEntity(Entity entity)
        {
            if (!entity.Visible)
                return false;


            return true;
        }

        public void Render(SpriteBatch spriteBatch, bool usePProcess=false)
        {
            
        }
    }
}
