﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blurlib.ECS.Managers
{
    public class EntitiesManager : IEnumerable<Entity>, IEnumerable
    {
        public Scene Scene { get; internal set; }

        private List<Entity> _entities;
        private List<Entity> _entitiesToAdd;
        private List<Entity> _entitiesToRemove;

        public EntitiesManager(Scene scene)
        {
            Scene = scene;

            _entities = new List<Entity>();
            _entitiesToAdd = new List<Entity>();
            _entitiesToRemove = new List<Entity>();
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

        public Entity Get(string tag)
        {
            throw new NotImplementedException();
        }

        public T Get<T>(string tag)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Entity> GetAll(string tag)
        {
            throw new NotImplementedException();
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
            Add(entities as IEnumerable<Entity>);
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
            Remove(entities as IEnumerable<Entity>);
        }

        public void RemoveById(string id)
        {
            throw new NotImplementedException();
        }

        public void Remove(string tag)
        {
            throw new NotImplementedException();
        }

        public void RemoveAll(string[] tags)
        {
            throw new NotImplementedException();
        }

        public void RemoveAll<T>() where T : Entity
        {
            Remove(GetAll<T>() as IEnumerable<Entity>);
        }

        public bool Contain(Entity entity)
        {
            throw new NotImplementedException();
        }

        public bool Contain(string id)
        {
            throw new NotImplementedException();
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