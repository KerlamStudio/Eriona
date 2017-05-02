﻿using Blurlib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Eriona.Scenes;

namespace Eriona
{
    public class ErionaGame : GameCore
    {
        public ErionaGame(int width, int height, string title, bool mouseVisible = true, string contentDir = "Content") : base(width, height, title, mouseVisible, contentDir)
        {
        }

        protected override void Initialize()
        {
            base.Initialize();

            _nextScene = new DummyScene("Dummy", this);
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}