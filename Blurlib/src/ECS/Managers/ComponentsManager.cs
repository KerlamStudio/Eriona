﻿using System;
using System.Collections.Generic;
using System.Collections;
using Blurlib.Render;
using Blurlib.ECS.Components;

namespace Blurlib.ECS.Managers
{
    public class ComponentsManager : IEnumerable<Component>, IEnumerable
    {
        public Entity Entity { get; internal set; }

        private HashSet<Component> _components;
        private HashSet<Component> _componentsToAdd;
        private HashSet<Component> _componentsToRemove;

        public int Count
        {
            get { return _components.Count; }
        }

        public ComponentsManager(Entity entity)
        {
            Entity = entity;

            _components = new HashSet<Component>();
            _componentsToAdd = new HashSet<Component>();
            _componentsToRemove = new HashSet<Component>();

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

        public void RefreshLists()
        {
            if (_componentsToAdd.Count > 0)
            {
                foreach (Component component in _componentsToAdd)
                {
                    _components.Add(component);
                    component.OnAdded(Entity);
                    if (component is IDraw)
                    {
                        GameCore.Instance.RenderManager.Add(component as IDraw);
                    }
                }
                _componentsToAdd.Clear();
            }

            if (_componentsToRemove.Count > 0)
            {
                foreach (Component component in _componentsToRemove)
                {
                    _components.Remove(component);
                    component.OnRemove();
                    if (component is IDraw)
                    {
                        GameCore.Instance.RenderManager.Remove(component as IDraw);
                    }
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

        public void ForEach(Action<Component> action)
        {
            foreach (Component component in _components)
            {
                action(component);
            }
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
