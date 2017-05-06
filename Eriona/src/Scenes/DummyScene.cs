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

        public override void Begin()
        {
            "Begin DummyScene [. . .]".Printl();
            Add(new DummyEntity(new Sprite(GameCore.Instance.Content.Load<Texture2D>("dummy"))));
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
            i++;
            if (i == 400)
                Add(new DummyEntity(new Sprite(GameCore.Instance.Content.Load<Texture2D>("dummy"), true, 2)));
            if (i < 400 )
                Get<DummyEntity>().Get<Sprite>().LocalPosition.X += 1;
            else
                Get<DummyEntity>().Get<Sprite>().LocalPosition.X -= 1;

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
