using Blurlib.ECS.Components;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Blurlib.Physics
{
    public class Cell
    {
        public Vector2 GridPosition;
        public Vector2 WordlPosition;
        public HashSet<Collider> Colliders;
        public int Count { get { return Colliders.Count; } }
    }
}
