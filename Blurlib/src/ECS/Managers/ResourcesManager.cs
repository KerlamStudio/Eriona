using Blurlib.Util;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blurlib.ECS.Managers
{
    public class ResourcesManager : IDisposable
    {
        public bool disposed = false;

        private Dictionary<string, Texture2D> _textureList;
        private Dictionary<string, SoundEffect> _soundList;
        private Dictionary<string, SpriteFont> _fontList;

        public ResourcesManager()
        {
            _textureList = new Dictionary<string, Texture2D>();
            _soundList = new Dictionary<string, SoundEffect>();
            _fontList = new Dictionary<string, SpriteFont>();
        }

        public T Get<T>(string id) where T : class
        {
            if (typeof(T).IsAssignableFrom(typeof(Texture2D)))
            {
                if (_textureList.ContainsKey(id))
                {
                    return _textureList[id] as T;
                }
            }
            else if (typeof(T).IsAssignableFrom(typeof(SoundEffect)))
            {
                if (_soundList.ContainsKey(id))
                {
                    return _soundList[id] as T;
                }
            }
            else if (typeof(T).IsAssignableFrom(typeof(SpriteFont)))
            {
                if (_fontList.ContainsKey(id))
                {
                    return _fontList[id] as T;
                }
            }
            return default(T);
        }

        public string GetId<T>(T asset)
        {
            if (asset is Texture2D)
            {
                foreach (KeyValuePair<string, Texture2D> element in _textureList)
                {
                    if (element.Value == asset as Texture2D)
                    {
                        return element.Key;
                    }
                }
            }
            else if (asset is SoundEffect)
            {
                foreach (KeyValuePair<string, SoundEffect> element in _soundList)
                {
                    if (element.Value == asset as SoundEffect)
                    {
                        return element.Key;
                    }
                }
            }
            else if (asset is SpriteFont)
            {
                foreach (KeyValuePair<string, SpriteFont> element in _fontList)
                {
                    if (element.Value == asset as SpriteFont)
                    {
                        return element.Key;
                    }
                }
            }

            return String.Empty;
        }

        public void Add<T>(string id, T asset)
        {
            if (asset is Texture2D)
            {
                if (!_textureList.ContainsKey(id))
                {
                    _textureList.Add(id, asset as Texture2D);
                }
            }
            else if (asset is SoundEffect)
            {
                if (!_soundList.ContainsKey(id))
                {
                    _soundList.Add(id, asset as SoundEffect);
                }
            }
            else if (asset is SpriteFont)
            {
                if (!_fontList.ContainsKey(id))
                {
                    _fontList.Add(id, asset as SpriteFont);
                }
            }
        }

        public void LoadAndAdd<T>(string id)
        {
            try
            {
                Add(id, GameCore.Instance.Content.Load<T>(id));
            }
            catch (Exception e)
            {
                // -TODO-: Replace by log
                ("Unknow asset : '" + id + "'").Printl();
            }
        }

        public void Remove<T>(string id)
        {
            if (typeof(T).IsAssignableFrom(typeof(Texture2D)))
            {
                if (_textureList.ContainsKey(id))
                {
                    _textureList[id].Dispose();
                    _textureList.Remove(id);
                }
            }
            else if (typeof(T).IsAssignableFrom(typeof(SoundEffect)))
            {
                if (_soundList.ContainsKey(id))
                {
                    _soundList[id].Dispose();
                    _soundList.Remove(id);
                }
            }
            else if (typeof(T).IsAssignableFrom(typeof(SpriteFont)))
            {
                if (_fontList.ContainsKey(id))
                {
                    _fontList.Remove(id);
                }
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
                foreach (Texture2D texture in _textureList.Values)
                {
                    texture.Dispose();
                }
                _textureList.Clear();

                foreach (SoundEffect sound in _soundList.Values)
                {
                    sound.Dispose();
                }
                _soundList.Clear();

                _fontList.Clear();
            }
            
            disposed = true;
        }
    }
}
