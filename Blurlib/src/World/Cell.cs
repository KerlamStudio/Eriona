using Blurlib.ECS.Components;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Blurlib.World
{
    public class Cell
    {
        public Vector2 GridPosition;
        public Vector2 WordlPosition;
        public List<Collider> Colliders;
        public int Count { get { return Colliders.Count; } }
    }
}
