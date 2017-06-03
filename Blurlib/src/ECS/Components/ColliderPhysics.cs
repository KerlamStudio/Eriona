using Blurlib.Physics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blurlib.ECS.Components
{
    public class ColliderPhysics : Collider
    {
        public Vector2 Velocity;

        public ForceList Forces { get; internal set; }

        public Vector2 Acceleration { get { return Forces.Acceleration; } }

        public float Friction;

        public float Mass;

        public ColliderPhysics(Transform transform, float mass, ForceList forces = null, float friction = 0.1f, bool collidable=true) : base(transform, collidable)
        {
            Mass = mass;

            Forces = forces ?? new ForceList(Mass);

            Friction = friction;

            Velocity = Vector2.Zero;
        }
    }
}
