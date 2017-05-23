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
        public DummyScene(string id, bool pause = false) : base(id, pause)
        {
        }

        public override void LoadContent()
        {
            base.LoadContent();
            Resources.LoadAndAdd<Texture2D>("dummy");
            Resources.LoadAndAdd<Texture2D>("flamme");
        }

        public override void Begin()
        {
            base.Begin();
            "Begin DummyScene [. . .]".Printl();
            dummy = new DummyEntity();
            Add(dummy);
        }

        public override void BeforeUpdate()
        {
            base.BeforeUpdate();
            "Before Update DummyScene [. . .]".Printl();
        }

        public int i = 0;

        public override void Update()
        {
            base.Update();

            "Update DummyScene [. . .]".Printl();
            //("Elapsed millisecond : " + GameCore.DeltaTime.ToString()).Printl();
            "".Printl();
            
            if (GameCore.InputsManager.IsPressed(Microsoft.Xna.Framework.Input.Keys.A))
            {
                dummy.Visible = !dummy.Visible;
            }
            
        }

        public override void AfterUpdate()
        {
            base.AfterUpdate();
            "After Update DummyScene [. . .]".Printl();
        }

        public override void End()
        {
            "End DummyScene [. . .]".Printl();
        }
    }
}
