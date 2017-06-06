using Blurlib.Physics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blurlib.Util;

namespace Blurlib.ECS.Components
{
    public class SolidCollider : Collider
    {
        public SolidCollider(Transform transform, string layer = null) : base(transform, true, true, true, layer)
        {

        }
        
        public override void OnCollide(Collider other, float collisionTime, Vector2 normal)
        {
            base.OnCollide(other);

            if (other.Static)
            {
                return;
            }

            Rectangle intersection = Rectangle.Intersect(WorldTransform.Bounds, other.WorldTransform.Bounds);

            ColliderPhysics colliderPhy;
            if (other is ColliderPhysics)
                colliderPhy = other as ColliderPhysics;
            else
                colliderPhy = null;


            if (intersection.Width < intersection.Height)
            {
                if (WorldTransform.Center.X > other.LastWorldTransform.Center.X)
                {
                    other.Entity.WorldPosition.X -= intersection.Width;
                }
                else
                {
                    other.Entity.WorldPosition.X += intersection.Width;
                }

                if (colliderPhy.IsNotNull())
                    colliderPhy.Velocity.X = 0;

            }
            else if (intersection.Width > intersection.Height)
            {
                if (WorldTransform.Center.Y > other.LastWorldTransform.Center.Y)
                {
                    other.Entity.WorldPosition.Y -= intersection.Height;
                }
                else
                {
                    other.Entity.WorldPosition.X += intersection.Width;
                }
                if (colliderPhy.IsNotNull())
                    colliderPhy.Velocity.Y = 0;
            }
            else
            {
                if (WorldTransform.Center.X > other.LastWorldTransform.Center.X)
                {
                    other.Entity.WorldPosition.X -= intersection.Width;
                }
                else
                {
                    other.Entity.WorldPosition.X += intersection.Width;
                }

                if (WorldTransform.Center.Y > other.LastWorldTransform.Center.Y)
                {
                    other.Entity.WorldPosition.Y -= intersection.Height;
                }
                else
                {
                    other.Entity.WorldPosition.Y += intersection.Height;
                }

                if (colliderPhy.IsNotNull())
                {
                    colliderPhy.Velocity.X = 0;
                    colliderPhy.Velocity.Y = 0;
                }
            }

        }
    }
}
