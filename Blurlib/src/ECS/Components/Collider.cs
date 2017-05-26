using Blurlib.Util;
using Blurlib.World;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Blurlib.ECS.Components
{
    public class Collider : Component
    {
        public bool Sleeping;

        private Transform _hitbox;
        public Transform Hitbox
        {
            get { return _hitbox; }
            set
            {
                _hitbox = value;
            }
        }

        public Transform WorldTransform
        {
            get { return new Transform(Position, _hitbox.Size); }
        }

        public Vector2 Position
        {
            get { return WorldPosition + _hitbox.Position; }
        }
                
        public Transform LastHitbox
        {
            get;
            private set;
        }

        public Vector2 LastPosition
        {
            get;
            private set;
        }

        public bool Changed
        {
            get { return LastPosition != Position || LastHitbox != Hitbox; }
        }

        public Collider(Transform transform, bool sleeping=false) : base(true, false, true)
        {
            _hitbox = transform;
            LastHitbox = transform;
            LastPosition = WorldPosition + _hitbox.Position;
            Sleeping = sleeping;
        }

        public virtual bool CollideWith(Collider other)
        {
            /*
            if (face != null)
            {
                Vector2 F = WorldPosition - LastPosition;
                if (F.X > 0)
                    face = Direction.Left;
                else if (F.X < 0)
                    face = Direction.Right;
                else if (F.Y > 0)
                    face = Direction.Down;
                else if (F.Y < 0)
                    face = Direction.Up;
            }
            */
            return other.Hitbox.IntersectBox(WorldPosition.X + Hitbox.X, WorldPosition.Y + Hitbox.Y, Hitbox.Width, Hitbox.Height);
        }

        public override void Update()
        {
            base.Update();

            LastHitbox = Hitbox.Copy();

            LastPosition = WorldPosition + _hitbox.Position;
        }

        public virtual void OnCollide(Collider other, Direction from)
        {

        }

        public override int GetHashCode()
        {
            return base.GetHashCode() * 23 + Hitbox.GetHashCode();
        }
    }
}
