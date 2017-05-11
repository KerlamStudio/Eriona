using Blurlib.ECS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blurlib;
using Microsoft.Xna.Framework;
using Blurlib.Util;
using Blurlib.ECS.Components;
using Microsoft.Xna.Framework.Graphics;
using Eriona.Entities;

namespace Eriona.Scenes
{
    public class DummyScene : Scene
    {
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
            Add(new DummyEntity());
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
