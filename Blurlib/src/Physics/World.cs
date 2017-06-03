using Blurlib.ECS;
using Blurlib.ECS.Components;
using Blurlib.Util;
using System.Collections.Generic;

namespace Blurlib.Physics
{
    public class World
    {
        public Scene Scene { get; private set; }

        public Dictionary<string, Grid> Layers { get; private set; }

        public Grid MainLayer { get; private set; }

        public World(Scene scene, Grid mainLayout = null)
        {
            Scene = scene;

            Layers = new Dictionary<string, Grid>();

            MainLayer = mainLayout ?? new Grid("MAIN", 1000, 1000, 50, 50);

            Layers.Add(MainLayer.Id, MainLayer);
        }

        public virtual void Initialize()
        {
            foreach (Grid layer in Layers.Values)
            {
                layer.Initialize();
            }
        }

        public virtual void Update()
        {
            foreach (Grid layer in Layers.Values)
            {
                layer.Update();
            }
        }

        public void Add(Collider collider, string layer = null)
        {
            if (layer.IsNotNull() && Layers.ContainsKey(layer))
            {
                Layers[layer].Add(collider);
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
            if (Layers.ContainsKey(collider.WorldLayer))
            {
                Layers[collider.WorldLayer].Remove(collider);
            }

            collider.WorldLayer = "";
        }

    }
}
