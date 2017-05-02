﻿using Blurlib.ECS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blurlib;
using Microsoft.Xna.Framework;
using Blurlib.Util;

namespace Eriona.Scenes
{
    public class DummyScene : Scene
    {
        public DummyScene(string id, GameCore gameCore, bool pause = false) : base(id, gameCore, pause)
        {
        }

        public override void Begin()
        {
            "Begin DummyScene [. . .]".Printl();
        }

        public override void Update()
        {
            base.Update();
            "Update DummyScene [. . .]".Printl();
            ("Elapsed millisecond : " + GameCore.DeltaTime.ToString()).Printl();
            "".Printl();

        }

        public override void End()
        {
            "End DummyScene [. . .]".Printl();
        }
    }
}