using Blurlib.ECS;
using Blurlib.ECS.Components;
using Blurlib.Util;
using System.Collections.Generic;

namespace Blurlib.Physics
{
    public class World
    {
        public Scene Scene { get; private set; }

        private Dictionary<string, Grid> _layers { get; set; }

        public Grid MainLayer { get; private set; }

        public World(Scene scene, Grid mainLayout = null)
        {
            Scene = scene;

            _layers = new Dictionary<string, Grid>();

            MainLayer = mainLayout ?? new Grid("MAIN", 1000, 1000, 100, 100);

            _layers.Add(MainLayer.Id, MainLayer);
        }

        public virtual void Initialize()
        {
            foreach (Grid layer in _layers.Values)
            {
                layer.Initialize();
            }
        }

        public virtual void Update()
        {
            foreach (Grid layer in _layers.Values)
            {
                layer.Update();
            }
        }

        public void Add(Collider collider, string layer = null)
        {
            if (layer.IsNotNull() && _layers.ContainsKey(layer))
            {
                _layers[layer].Add(collider);
                collider.WorldLayer = layer;
            }
            else
            {
                MainLayer.Add(collider);
                collider.WorldLayer = MainLayer.Id;
            }
        }

        public void Remove(Collider collider)
        {
            if (_layers.ContainsKey(collider.WorldLayer))
            {
                _layers[collider.WorldLayer].Remove(collider);
            }

            collider.WorldLayer = "";
        }

        public void AddLayer(string id, Grid layer)
        {
            if (!_layers.ContainsKey(id))
            {
                _layers.Add(id, layer);
                layer.Initialize();
            }
        }

        public Grid GetLayer(string id)
        {
            if (_layers.ContainsKey(id))
            {
                return _layers[id];
            }
            else
            {
                return null;
            }
        }

    }
}
