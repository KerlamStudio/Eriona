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

        }

        public T Get<T>() where T : Component
        {
            return default(T);
        }

        public T[] GetList<T>() where T : Component
        {
            return default(T[]);
        }

        public void Add<T>(T component) where T : Component
        {

        }

        public void Add(IEnumerable<Component> components)
        {

        }

        public void Add(params Component[] components)
        {

        }

        public bool Remove<T>() where T : Component
        {
            return false;
        }

        public bool Remove<T>(T component) where T : Component
        {
            return false;
        }

        public bool Remove(string id)
        {
            return false;
        }

        public bool Contains<T>(T component) where T : Component
        {
            return false;
        }
    }
}
