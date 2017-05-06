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
            throw new NotImplementedException();
        }

        public IEnumerable<Entity> GetAll(string tag)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll<T>() where T : Entity
        {
            return _entities.GetAll<T>();
        }

        public bool Add<T>(T entity) where T : Entity
        {
            return _entities.Add(entity);
        }

        public bool Add(IEnumerable<Entity> entities)
        {
            return _entities.Add(entities);
        }

        public bool Add(params Entity[] entities)
        {
            return _entities.Add(entities);
        }

        public bool Remove(Entity entity)
        {
            return _entities.Remove(entity);
        }

        public bool Remove<T>() where T : Entity
        {
            return _entities.Remove<T>();
        }

        public bool Remove(IEnumerable<Entity> entities)
        {
            return _entities.Remove(entities);
        }

        public bool Remove(params Entity[] entities)
        {
            return _entities.Remove(entities);
        }

        public bool RemoveById(string id)
        {
            return _entities.RemoveById(id);
        }

        public bool Remove(string tag)
        {
            return _entities.Remove(tag);
        }

        public bool RemoveAll(string[] tags)
        {
            return _entities.RemoveAll(tags);
        }

        public bool RemoveAll<T>() where T : Entity
        {
            return _entities.RemoveAll<T>();
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
