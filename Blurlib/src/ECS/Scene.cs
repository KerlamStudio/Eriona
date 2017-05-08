using Blurlib.ECS.Managers;
using Blurlib.ECS;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blurlib.ECS
{
    public abstract class Scene
    {
        private string _id;
        public string Id
        {
            get { return _id; }
        }

        // -TODO-: Pause
        private bool _pause;
        public bool Pause
        {
            get { return _pause; }
            set { throw new NotImplementedException(); }
        }
        
        public ResourcesManager SceneResources;

        protected Color _backgroundColor;

        private EntitiesManager _entities;

        public Scene(string id, bool pause = false)
        {
            _id = id;
            _backgroundColor = Color.CornflowerBlue;
            _pause = pause;

            SceneResources = new ResourcesManager();

            _entities = new EntitiesManager(this);
        }

        public T Get<T>() where T : Entity
        {
            return _entities.Get<T>();
        }

        public Entity Get(string tag)
        {
            return _entities.Get(tag);
        }

        public T Get<T>(string tag) where T : Entity
        {
            return _entities.Get<T>(tag);
        }

        public IEnumerable<Entity> GetAll(string tag)
        {
            return _entities.GetAll(tag);
        }

        public IEnumerable<T> GetAll<T>() where T : Entity
        {
            return _entities.GetAll<T>();
        }

        public void Add<T>(T entity) where T : Entity
        {
            _entities.Add(entity);
        }

        public void Add(IEnumerable<Entity> entities)
        {
            _entities.Add(entities);
        }

        public void Add(params Entity[] entities)
        {
            _entities.Add(entities);
        }

        public void Remove(Entity entity)
        {
            _entities.Remove(entity);
        }

        public void Remove<T>() where T : Entity
        {
            _entities.Remove<T>();
        }

        public void Remove(IEnumerable<Entity> entities)
        {
            _entities.Remove(entities);
        }

        public void Remove(params Entity[] entities)
        {
            _entities.Remove(entities);
        }

        public void RemoveById(string id)
        {
            _entities.RemoveById(id);
        }

        public void Remove(string tag)
        {
            _entities.Remove(tag);
        }

        public void RemoveAll(string[] tags)
        {
            _entities.RemoveAll(tags);
        }

        public void RemoveAll<T>() where T : Entity
        {
            _entities.RemoveAll<T>();
        }

        public bool Contain(Entity entity)
        {
            return _entities.Contain(entity);
        }

        public bool Contain(string id)
        {
            return _entities.Contain(id);
        }

        public abstract void Begin();

        public virtual void BeforeUpdate()
        {
            _entities.RefreshLists();
        }

        public virtual void Update()
        {
            _entities.Update();
        }

        public virtual void AfterUpdate()
        {

        }

        public virtual void End()
        {
            foreach (Entity entity in _entities)
                entity.OnRemove();
        }

    }
}
