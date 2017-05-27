using Blurlib.ECS.Managers;
using Blurlib.Util;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

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

        private ComponentsManager _components;

        public Vector2 WorldPosition;

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
                return _visible;
            }
            set
            {
                _visible = value;
            }
        }
        
        public bool Collidable;

        public Entity(string id, Vector2? worldPosition, bool active = false, bool visible = false, bool collidable = false, params Component[] components)
        {
            _active = active;
            _visible = visible;
            Collidable = collidable;

            _components = new ComponentsManager(this);

            Tags = new List<string>();
           
            _id = id ?? Extension.GenerateUniqueId("Entity");
            
           WorldPosition = worldPosition ?? Vector2.Zero;

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
            _components.RefreshLists();
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

        public override int GetHashCode()
        {
            int hash = 17;

            hash = hash * 23 + Id.GetHashCode();

            if (Scene.IsNotNull())
                hash = hash * 23 + Scene.GetHashCode();

            hash = hash * 23 + _components.GetHashCode();
            hash = hash * 23 + WorldPosition.GetHashCode();
            hash = hash * 23 + Tags.GetHashCode();

            return hash;
        }
    }
}
