using Blurlib.Util;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Blurlib.ECS
{
    public abstract class Component
    {
        public string Id { get; private set; }

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
        
        public Entity Entity { get; private set; }

        public Scene Scene { get { return Entity.IsNotNull() ? Entity.Scene : null; } }
        
        public Vector2 LocalPosition;

        public Vector2 WorldPosition
        {
            get
            {
                if (Entity.IsNotNull())
                    return Entity.WorldPosition + LocalPosition;
                else
                    return new Vector2(0, 0);
            }
        }

        // public Transform Transform;
        
        public List<string> Tags;

        public Component(bool active=false, bool visible=false)
        {
            _active = active;
            _visible = visible;
            LocalPosition = new Vector2();
            Id = Util.Extension.GenerateUniqueId("Component");
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

        public override int GetHashCode()
        {
            int hash = 17;

            if (Entity.IsNotNull())
                hash = hash * 23 + Entity.GetHashCode();
            hash = hash * 23 + Id.GetHashCode();
            hash = hash * 23 + Tags.GetHashCode();

            return hash;
        }
    }
}
