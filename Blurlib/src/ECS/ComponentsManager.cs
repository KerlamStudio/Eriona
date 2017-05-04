using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blurlib.ECS
{
    public class ComponentsManager
    {
        public Entity Entity;

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
            RefreshComponentList();

            // -TODO-: Sort components by update order
            foreach (Component component in _components)
            {
                if (component.Active)
                    component.Update();
            }
        }

        private void RefreshComponentList()
        {
            if (_componentsToRemove.Count > 0)
            {
                foreach (Component component in _componentsToRemove)
                {
                    _components.Remove(component);
                    component.OnRemove();
                }
                _componentsToRemove.Clear();
            }

            if (_componentsToAdd.Count > 0)
            {
                foreach (Component component in _componentsToAdd)
                {
                    _components.Add(component);
                    component.OnAdded(Entity);
                }
                _componentsToAdd.Clear();
            }
        }

        public T Get<T>() where T : Component
        {
            foreach (Component component in _components)
                if (component is T)
                    return component as T;
            return null;
        }

        public Component Get(string tag)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Component> GetAll(string tag)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll<T>() where T : Component
        {
            foreach (Component component in _components)
                if (component is T)
                    yield return component as T;
        }

        public bool Add<T>(T component) where T : Component
        {
            if (!_components.Contains(component) && !_componentsToAdd.Contains(component))
            {
                _componentsToAdd.Add(component);
                component.Initialize();
                return true;
            }
            return false;
        }

        public bool Add(IEnumerable<Component> components)
        {
            // If a component is not added we return false
            bool getOneFalse = true;

            foreach (Component component in components)
            {
                if (!Add(component))
                    getOneFalse = false;
            }
            return getOneFalse;
        }

        public bool Add(params Component[] components)
        {
            return Add(components);
        }

        public bool Remove<T>(T component) where T : Component
        {
            if (_components.Contains(component) && !_componentsToRemove.Contains(component))
            {
                _componentsToRemove.Add(component);
                return true;
            }
            return false;
        }

        public bool Remove<T>() where T : Component
        {
            foreach (Component component in _components)
                if (component is T)
                {
                    Remove(component);
                    return true;
                }
            return false;
        }

        public bool Remove(IEnumerable<Component> components)
        {
            // If a component is not added we return false
            bool getOneFalse = true;

            foreach (Component component in components)
                if (!Remove(component))
                    getOneFalse = false;
            return getOneFalse;
        }

        public bool Remove(params Component[] components)
        {
            return Remove(components);
        }

        public bool Remove(string tag)
        {
            throw new NotImplementedException();
        }

        public bool RemoveAll<T>() where T : Component
        {
            return Remove(GetAll<T>() as IEnumerable<Component>);
        }

        public bool Contain<T>(T component) where T : Component
        {
            return false;
        }
    }
}
