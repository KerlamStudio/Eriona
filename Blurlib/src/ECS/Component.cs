using Blurlib.Util;
using Blurlib.World;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

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
        
        public Vector2 LocalPosition;

        public Vector2 WorldPosition
        {
            get
            {
                return Entity.WorldPosition + LocalPosition;
            }
        }

        // public Transform Transform;
        
        public List<string> Tags;

        public Component(bool active=false, bool visible=false, bool collidable=false)
        {
            _active = active;
            _visible = visible;
            //Collidable = collidable;
            LocalPosition = new Vector2();

            Tags = new List<string>();
        }

        public virtual void Initialize()
        {
        }

        public virtual void OnAdded(Entity entity)
        {
            Entity = entity;
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
