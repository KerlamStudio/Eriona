using Blurlib.ECS.Managers;
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

        public List<string> Tags;

        public Scene Scene { get; private set; }

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

        public bool Visible;

        private bool _collidable;
        public bool Collidable
        {
            get { return _collidable; }
            set
            {
                throw new NotImplementedException();
            }
        }

        private ComponentsManager _components;

        public Entity(string id = null, Transform worldTransform = null, bool active = false, bool visible = false, bool collidable = false, params Component[] components)
        {
            _active = active;
            Visible = visible;
            //Collidable = collidable;

            _components = new ComponentsManager(this);

            Tags = new List<string>();

            if (id.IsNull())
                _id = Extension.GenerateUniqueId("Entity");
            else
                _id = id;

            if (worldTransform.IsNull())
                WorldTransform = new Transform(0, 0, 0, 0);
            else
                WorldTransform = worldTransform;

            if (components.Length > 0)
                _components.Add(components);
        }

        #region Components Management

        public T Get<T>() where T : Component
        {
            return _components.Get<T>();
        }

        public IEnumerable<Component> GetAll(string tag)
        {
            return _components.GetAll(tag);
        }

        public IEnumerable<Component> GetAll(IEnumerable<string> tags)
        {
            return _components.GetAll(tags);
        }
        
        public IEnumerable<T> GetAll<T>() where T : Component
        {
            return _components.GetAll<T>();
        }

        public void Add(Component component)
        {
            _components.Add(component);
        }

        public void Add(IEnumerable<Component> components)
        {
            _components.Add(components);
        }

        public void Add(params Component[] components)
        {
            _components.Add(components);
        }

        public void Remove<T>(T component) where T : Component
        {
            _components.Remove(component);
        }

        public void Remove(IEnumerable<Component> components)
        {
            _components.Remove(components);
        }

        public void Remove(params Component[] components)
        {
            _components.Remove(components);
        }

        public void Remove<T>() where T : Component
        {
            _components.Remove<T>();
        }

        public void Remove(string tag)
        {
            _components.Remove(tag);
        }

        public void RemoveAll<T>() where T : Component
        {
            _components.RemoveAll<T>();
        }

        public bool Contains(Component component)
        {
            return _components.Contains(component);
        }

        public bool Contains<T>() where T : Component
        {
            return _components.Contains<T>();
        }

        #endregion // Coponent Management

        #region Cycle

        public virtual void Initialize()
        {
        }

        public virtual void OnAdded(Scene scene)
        {
            Scene = scene;
        }

        public virtual void Awake()
        {
        }

        public virtual void OnRemove()
        {
            foreach (Component component in _components)
                component.OnRemove();
        }

        public virtual void OnEnable()
        {
        }

        public virtual void OnDisable()
        {
        }
        
        public virtual void Update()
        {
            _components.Update();
        }

        #endregion // Cycle
    }
}
