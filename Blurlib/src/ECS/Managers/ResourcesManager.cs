using Blurlib.Util;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Blurlib.ECS.Managers
{
    public class ResourcesManager : IDisposable
    {
        public bool disposed = false;

        private Hashtable _resourcesList;

        public ResourcesManager()
        {
            _resourcesList = new Hashtable();
        }

        public T Get<T>(string id) where T : class, IDisposable
        {
            if (_resourcesList.ContainsKey(id))
            {
                return _resourcesList[id] as T;
            }
            return default(T);
        }

        public string GetId<T>(T asset) where T : class, IDisposable
        {
            foreach (KeyValuePair<string, object> element in _resourcesList.Values)
            {
                if (element.Value is T && element.Value as T == asset)
                {
                    return element.Key;
                }
            }
            return String.Empty;
        }

        public void Add<T>(string id, T asset) where T : class, IDisposable
        {
            if (!_resourcesList.ContainsKey(id))
            {
                _resourcesList.Add(id, asset);
            }
        }

        public void LoadAndAdd<T>(string id) where T : class, IDisposable
        {
            try
            {
                Add(id, GameCore.Instance.Content.Load<T>(id));
            }
            catch (Exception e)
            {
                // -TODO-: Replace by log
                (e.ToString() + " '" + id + "'").Printl();
            }
        }

        public void Remove<T>(string id) where T : class, IDisposable
        {
            if (_resourcesList.ContainsKey(id))
            {
                (_resourcesList[id] as T ).Dispose();
                _resourcesList.Remove(id);
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                foreach (IDisposable element in _resourcesList.Values)
                {
                    element.Dispose();
                }
                _resourcesList.Clear();
            }
            
            disposed = true;
        }
    }
}
