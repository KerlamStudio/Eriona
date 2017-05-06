using Blurlib.Util;
using Blurlib.World;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blurlib.ECS
{
    public abstract class Component
    {
        private bool _active;
        public bool Active
        {
            get { return _active; }

            set
            {
                if (value)
                {
                    _active = true;
                    OnEnable();
                }
                else
                {
                    _active = false;
                    OnDisable();
                }
            }
        }

        private bool _visible;
        public bool Visible
        {
            get
            {
                if (Entity.IsNotNull() && Entity.Visible)
                    return _visible;
                else
                    return false;
            }

            set
            {
                _visible = value;
                if (_visible)
                {
                    GameCore.Instance.RenderManager.AddComponent(this);
                }
                else if (!_visible)
                {
                    GameCore.Instance.RenderManager.RemoveComponent(this);
                }
            }
        }

        private bool _collidable;
        public bool Collidable
        {
            get { return _collidable; }
            set
            {
                throw new NotImplementedException();
            }
        }

        public Entity Entity { get; private set; }

        public Scene Scene { get { return Entity.IsNotNull() ? Entity.Scene : null; } }

        private Transform _transform;
        public Transform Transform
        {
            get { return new Transform(WorldPosition, _transform.Size); }
            set
            {
                _transform = value;
                LocalPosition = Entity.WorldTransform.Position - value.Position;
            }
        }
        public Vector2 LocalPosition;

        public Vector2 WorldPosition
        {
            get
            {
                return Entity.WorldTransform.Position + LocalPosition;
            }
        }

        // public Transform Transform;
        
        public List<string> Tags;

        public Component(bool active=false, bool visible=false, bool collidable=false)
        {
            _active = active;
            Visible = visible;
            //Collidable = collidable;
            _transform = new Transform();
            LocalPosition = new Vector2();

            _transform = Transform.Zero;

           

            Tags = new List<string>();
        }

        public virtual void Initialize()
        {
        }

        public virtual void OnAdded(Entity entity)
        {
            Entity = entity;
            Visible = _visible;
        }

        public virtual void OnRemove()
        {
        }

        public virtual void OnEnable()
        {
        }

        public virtual void OnDisable()
        {
        }

        public virtual void Update()
        {

        }

        public void RemoveSelf()
        {
            if (Entity.IsNotNull())
                Entity.Remove(this);
        }
    }
}
