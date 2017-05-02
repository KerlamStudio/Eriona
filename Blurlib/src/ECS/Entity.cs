using Blurlib.Util;
using Blurlib.World;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blurlib.ECS
{
    public abstract class Entity
    {
        private string _id;
        public string Id
        {
            get { return _id; }
        }

        private GameCore _gameCore;

        private Scene _scene;

        public Transform WorldTransform;

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
            get { return _visible; }
            set
            {
                throw new NotImplementedException();
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

        public ComponentsManager Components;

        public Entity(string id=null, Transform worldTransform=null, bool active=false, bool visible=false, bool collidable=false, params Component[] components)
        {
            _active = active;
            Visible = visible;
            Collidable = collidable;

            if (id.IsNull())
                _id = Extension.GenerateUniqueId("Entity");
            else
                _id = id;

            if (worldTransform.IsNull())
                WorldTransform = new Transform(0, 0, 0, 0);
            else
                WorldTransform = worldTransform;

            if (components.Length > 0)
                Components.Add(components);
        }

        public virtual void Initialize()
        {
        }

        public virtual void OnAdded(Scene scene)
        {
            _scene = scene;
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
            Components.Update();
        }        
    }
}
