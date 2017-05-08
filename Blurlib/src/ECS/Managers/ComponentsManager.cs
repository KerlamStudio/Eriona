using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Blurlib.ECS.Managers
{
    public class ComponentsManager : IEnumerable<Component>, IEnumerable
    {
        public Entity Entity { get; internal set; }

        private List<Component> _components;
        private List<Component> _componentsToAdd;
        private List<Component> _componentsToRemove;

        public int Count
        {
            get { return _components.Count; }
        }

        public ComponentsManager(Entity entity)
        {
            Entity = entity;

            _components = new List<Component>();
            _componentsToAdd = new List<Component>();
            _componentsToRemove = new List<Component>();

        }

        public void Update()
        {
            RefreshLists();

            // -TODO-: Sort components by update order
            foreach (Component component in _components)
            {
                if (component.Active)
                    component.Update();
            }
        }

        private void RefreshLists()
        {
            if (_componentsToAdd.Count > 0)
            {
                foreach (Component component in _componentsToAdd)
                {
                    _components.Add(component);
                    component.OnAdded(Entity);
                }
                _componentsToAdd.Clear();
            }

            if (_componentsToRemove.Count > 0)
            {
                foreach (Component component in _componentsToRemove)
                {
                    _components.Remove(component);
                    component.OnRemove();
                }
                _componentsToRemove.Clear();
            }
        }

        public T Get<T>() where T : Component
        {
            foreach (Component component in _components)
                if (component is T)
                    return component as T;
            return null;
        }

        public IEnumerable<Component> GetAll(string tag)
        {
            foreach (Component component in _components)
            {
                if (component.Tags.Contains(tag))
                {
                    yield return component;
                }
            }
        }

        public IEnumerable<Component> GetAll(IEnumerable<string> tags)
        {
            bool add;
            foreach (Component component in _components)
            {
                add = false;
                foreach (String tag in tags)
                {
                    if (component.Tags.Contains(tag))
                    {
                        add = true;
                        break;
                    }
                }
                if (add)
                {
                    yield return component;
                }
            }
        }

        public IEnumerable<T> GetAll<T>() where T : Component
        {
            foreach (Component component in _components)
                if (component is T)
                    yield return component as T;
        }

        public void Add(Component component)
        {
            if (!_components.Contains(component) && !_componentsToAdd.Contains(component))
            {
                _componentsToAdd.Add(component);
                component.Initialize();
            }
        }

        public void Add(IEnumerable<Component> components)
        {
            foreach (Component component in components)
            {
                Add(component);
            }
        }

        public void Add(params Component[] components)
        {
            foreach (Component component in components)
            {
                Add(component);
            }
        }

        public void Remove(Component component)
        {
            if (_components.Contains(component) && !_componentsToRemove.Contains(component))
            {
                _componentsToRemove.Add(component);
            }
        }

        public void Remove<T>() where T : Component
        {
            foreach (Component component in _components)
            {
                if (component is T)
                {
                    Remove(component);
                }
            }
        }

        public void Remove(IEnumerable<Component> components)
        {
            foreach (Component component in components)
            {
                Remove(component);
            }
        }

        public void Remove(params Component[] components)
        {
            foreach (Component component in components)
            {
                Remove(component);
            }
        }

        public void Remove(string tag)
        {
            Remove(GetAll(tag));
        }

        public void RemoveAll<T>() where T : Component
        {
            Remove(GetAll<T>());
        }

        public bool Contains(Component component)
        {
            return _components.Contains(component);
        }

        public bool Contains<T>() where T : Component
        {
            foreach (Component component in _components)
            {
                if (component is T)
                {
                    return true;
                }
            }
            return false;
        }

        public IEnumerator<Component> GetEnumerator()
        {
            return ((IEnumerable<Component>)_components).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<Component>)_components).GetEnumerator();
        }
    }
}
