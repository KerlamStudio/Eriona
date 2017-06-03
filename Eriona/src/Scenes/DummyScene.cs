using Blurlib.ECS;
using Blurlib;
using Blurlib.Util;
using Microsoft.Xna.Framework.Graphics;
using Eriona.Entities;

namespace Eriona.Scenes
{
    public class DummyScene : Scene
    {
        DummyEntity dummy;
        public DummyScene(string id, bool pause = false) : base(id, null, pause)
        {
        }

        public override void LoadContent()
        {
            base.LoadContent();
            Resources.LoadAndAdd<Texture2D>("dummy");
            Resources.LoadAndAdd<Texture2D>("flamme");
            Resources.LoadAndAdd<Texture2D>("target_dummy");
        }

        public override void Begin()
        {
            base.Begin();
            dummy = new DummyEntity();
            Add(dummy);
        }

        public override void BeforeUpdate()
        {
            base.BeforeUpdate();
        }

        public override void Update()
        {
            base.Update();
            
            //("Elapsed second : " + GameCore.DeltaTime.ToString()).Printl();
        }

        public override void AfterUpdate()
        {
            base.AfterUpdate();
        }

        public override void End()
        {
        }
    }
}
