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
        
        public bool Visible { get; set; }

        private bool _collidable;
        public bool Collidable
        {
            get { return _collidable; }
            set
            {
                throw new NotImplementedException();
            }
        }

        public Entity Entity { get; set; }
        
        // =TODO=: WorldTransform / EntityTransform
        // public Transform Transform;
        
        public string[] Tags;

        public Component(bool active=false, bool visible=false, bool collidable=false)
        {
            _active = active;
            Visible = visible;
            //Collidable = collidable;
            
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
    }
}
