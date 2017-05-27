using System;
using System.Collections;
using System.Collections.Generic;

namespace Blurlib.ECS.Managers
{
    public class EntitiesManager : IEnumerable<Entity>, IEnumerable
    {
        public Scene Scene { get; internal set; }

        private HashSet<Entity> _entities;
        private HashSet<Entity> _entitiesToAdd;
        private HashSet<Entity> _entitiesToRemove;

        public EntitiesManager(Scene scene)
        {
            Scene = scene;

            _entities = new HashSet<Entity>();
            _entitiesToAdd = new HashSet<Entity>();
            _entitiesToRemove = new HashSet<Entity>();
        }

        public void Update()
        {
            foreach (Entity entity in _entities)
            {
                if (entity.Active)
                    entity.Update();
            }
        }

        public void RefreshLists()
        {
            if (_entitiesToAdd.Count > 0)
            {
                foreach (Entity entity in _entitiesToAdd)
                {
                    if (!_entities.Contains(entity))
                    {
                        _entities.Add(entity);
                        entity.OnAdded(Scene);
                    }
                }
            }

            if (_entitiesToRemove.Count > 0)
            {
                foreach (Entity entity in _entitiesToRemove)
                {
                    if (_entities.Contains(entity))
                    {
                        _entities.Remove(entity);
                        entity.OnRemove();
                    }
                }
                _entitiesToRemove.Clear();
            }

            if (_entitiesToAdd.Count > 0)
            {
                foreach (Entity entity in _entitiesToAdd)
                {
                    entity.Awake();
                }
                _entitiesToAdd.Clear();
            }
        }

        public T Get<T>() where T : Entity
        {
            foreach (Entity entity in _entities)
                if (entity is T)
                    return entity as T;
            return null;
        }

        public Entity Get(string id)
        {
            foreach (Entity entity in _entities)
            {
                if (entity.Id == id)
                {
                    return entity;
                }
            }
            return null;
        }

        public T Get<T>(string id) where T : Entity
        {
            foreach (Entity entity in _entities)
            {
                if (entity.Id == id)
                {
                    return entity as T;
                }
            }
            return null;
        }

        public IEnumerable<Entity> GetAll(string tag)
        {
            foreach (Entity entity in _entities)
            {
                if (entity.Tags.Contains(tag))
                {
                    yield return entity;
                }
            }
        }

        public IEnumerable<Entity> GetAll(IEnumerable<string> tags)
        {
            bool add;
            foreach (Entity entity in _entities)
            {
                add = false;
                foreach (String tag in tags)
                {
                    if (entity.Tags.Contains(tag))
                    {
                        add = true;
                        break;
                    }
                }
                if (add)
                {
                    yield return entity;
                }
            }
        }

        public IEnumerable<T> GetAll<T>() where T : Entity
        {
            foreach (Entity entity in _entities)
                if (entity is T)
                    yield return entity as T;
        }

        public void Add(Entity entity)
        {
            if (!_entities.Contains(entity) && !_entitiesToAdd.Contains(entity))
            {
                _entitiesToAdd.Add(entity);
                entity.Initialize();
            }
        }

        public void Add(IEnumerable<Entity> entities)
        {
            foreach (Entity entity in entities)
            {
                Add(entity);
            }
        }

        public void Add(params Entity[] entities)
        {
            foreach (Entity entity in entities)
            {
                Add(entity);
            }
        }

        public void Remove(Entity entity)
        {
            if (_entities.Contains(entity) && !_entitiesToRemove.Contains(entity))
            {
                _entitiesToRemove.Add(entity);
            }
        }

        public void Remove<T>() where T : Entity
        {
            foreach (Entity entity in _entities)
            {
                if (entity is T)
                {
                    Remove(entity);
                }
            }
        }

        public void Remove(IEnumerable<Entity> entities)
        {
            foreach (Entity entity in entities)
            {
                Remove(entity);
            }
        }

        public void Remove(params Entity[] entities)
        {
            foreach (Entity entity in entities)
            {
                Remove(entity);
            }
        }

        public void RemoveById(string id)
        {
            Remove(Get(id));
        }

        public void Remove(string tag)
        {
            Remove(GetAll(tag));
        }

        public void RemoveAll(IEnumerable<string> tags)
        {
            Remove(GetAll(tags));
        }

        public void RemoveAll<T>() where T : Entity
        {
            Remove(GetAll<T>());
        }

        public bool Contains(Entity entity)
        {
            return _entities.Contains(entity);
        }

        public bool Contains(string id)
        {
            foreach (Entity entity in _entities)
            {
                if (entity.Id == id)
                {
                    return true;
                }
            }
            return false;
        }

        public bool Contains<T>() where T : Entity
        {
            foreach (Entity entity in _entities)
            {
                if (entity is T)
                {
                    return true;
                }
            }
            return false;
        }

        public IEnumerator<Entity> GetEnumerator()
        {
            return ((IEnumerable<Entity>)_entities).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<Entity>)_entities).GetEnumerator();
        }
    }
}
