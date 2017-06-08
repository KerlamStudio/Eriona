using Blurlib.Physics;
using Blurlib.Util;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Blurlib.ECS.Components
{
    public class Collider : Component
    {
        private Transform _hitbox;
        public Transform Hitbox
        {
            get { return _hitbox; }
            set
            {
                if (!Static)
                    _hitbox = value;
            }
        }

        private Transform _worldPosition;
        public Transform WorldTransform
        {
            get
            {
                _worldPosition.Position = Position;
                _worldPosition.Size = _hitbox.Size;
                return _worldPosition;
            }
        }

        public Color DebugColor = Color.Blue;

    // -TODO- Optimize
        public Transform LastWorldTransform
        {
            get { return new Transform(LastPosition, LastHitbox.Size); }
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

        private bool _collidable;
        public bool Collidable
        {
            get
            {
                if (Entity.IsNotNull() && Entity.Collidable)
                    return _collidable;
                else
                    return false;
            }
            set
            {
                _collidable = value;
            }
        }

        public bool Changed
        {
            get
            {
                return LastPosition != Position || LastHitbox != Hitbox;
            }
        }

        public string WorldLayer { get; internal set; }

        private bool _static;
        public bool Static
        {
            get
            {
                return _static;
            }
            set
            {
                if (value)
                {
                    LastHitbox.Copy(Hitbox);
                    LastPosition = WorldPosition + _hitbox.Position;
                }
                _static = value;
            }
        }

        public bool Solid;

        public Collider(Transform transform, bool collidable = true, bool isStatic = false, bool solid = false, string layer = null) : base(true, false)
        {
            _hitbox = transform;
            _worldPosition = new Transform();
            LastHitbox = transform;
            LastPosition = WorldPosition + _hitbox.Position;
            _collidable = collidable;
            _static = isStatic;
            WorldLayer = layer;
            Solid = solid;
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

        public override void OnAdded(Entity entity)
        {
            base.OnAdded(entity);

            Scene?.World.Add(this, WorldLayer);
        }

        public override void Update()
        {
            base.Update();

            LastHitbox.Copy(Hitbox);

            LastPosition = WorldPosition + _hitbox.Position;
        }

        public virtual void OnCollide(Collider other, Vector3 normal = default(Vector3))
        {

        }

        public Grid GetWorldLayer()
        {
            return Scene.World.GetLayer(WorldLayer);
        }
    }
}
