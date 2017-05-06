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

        protected GameCore _gameCore;

        ResourcesManager SceneResources;

        protected Color _backgroundColor;
        
        public Scene(string id, GameCore gameCore, bool pause = false)
        {
            _id = id;
            _gameCore = gameCore;
            _backgroundColor = Color.CornflowerBlue;
            _pause = pause;

            SceneResources = new ResourcesManager();
        }


        // -TODO-: Getters/adders/removers of entity

        public void Add<T>(T entity) where T : Entity
        {
            throw new NotImplementedException();
        }

        public abstract void Begin();

        public virtual void BeforeUpdate()
        {

        }

        public virtual void Update()
        {

        }

        public virtual void AfterUpdate()
        {

        }

        public abstract void End();

    }
}
