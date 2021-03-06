﻿using Blurlib.ECS;
using Blurlib.Input;
using Blurlib.Render;
using Blurlib.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Blurlib
{
    public class GameCore : Game  
    {
        public static GameCore Instance { get; private set; }
        public static float DeltaTime { get; private set; }

        protected GraphicsDeviceManager _graphics;
        protected SpriteBatch _spriteBatch;

        private int _windowWidth;
        public int WindowWidth
        {
            get { return _windowWidth; }
            set { throw new NotImplementedException(); }
        }

        private int _windowHeight;
        public int WindowHeight
        {
            get { return _windowHeight; }
            set { throw new NotImplementedException(); }
        }

        public string WindowTitle
        {
            get { return Window.Title; }
            set { Window.Title = value; }
        }

        protected Scene _scene;
        public Scene Scene
        {
            get { return _scene; }
            set { _nextScene = value; }
        }

        protected Scene _nextScene;
        public Scene NextScene
        {
            get { return _nextScene; }
        }
        
        public RenderManager RenderManager;

        public static InputsManager InputsManager;

        public GameCore(int width, int height, string title, bool mouseVisible=true, string contentDir="Content")
        {
            Instance = this;

            RenderManager = new RenderManager();
            InputsManager = new InputsManager();

            _windowWidth = width;
            _windowHeight = height;
            WindowTitle = title;
            IsMouseVisible = mouseVisible;
            

            _graphics = new GraphicsDeviceManager(this);
            _graphics.DeviceCreated += Graphics_DeviceCreated;
            _graphics.PreparingDeviceSettings += Graphics_PreparingDeviceSettings;

            Content.RootDirectory = contentDir;


            _graphics.PreferredBackBufferWidth = WindowWidth;
            _graphics.PreferredBackBufferHeight = WindowHeight;

            _graphics.SynchronizeWithVerticalRetrace = false;
            IsFixedTimeStep = true;
            TargetElapsedTime = TimeSpan.FromMilliseconds(1000.0f / 60);
            _graphics.ApplyChanges();
        }

        private void Graphics_DeviceCreated(object sender, EventArgs e)
        {
            //EmptyKeys.UserInterface.Engine engine = new MonoGameEngine(GraphicsDevice, _windowWidth, _windowHeight);
        }

        private void Graphics_PreparingDeviceSettings(object sender, PreparingDeviceSettingsEventArgs e)
        {
            // -TODO-: understand this lol
            _graphics.PreferMultiSampling = true;
            // -TODO-: Create setting to GraphicsProfile.Reach for old computer
            _graphics.GraphicsProfile = GraphicsProfile.HiDef;

            //_graphics.SynchronizeWithVerticalRetrace = false;
            //IsFixedTimeStep = true;
            // or
            // -TODO-: Test difference
            

            _graphics.PreferredDepthStencilFormat = DepthFormat.Depth24Stencil8;
            e.GraphicsDeviceInformation.PresentationParameters.MultiSampleCount = 16;
            //_graphics.ApplyChanges();
        }

        protected override void Initialize()
        {
            base.Initialize();

            _spriteBatch = new SpriteBatch(GraphicsDevice);

            RenderManager.Initialize();

            _graphics.ApplyChanges();
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            

            DeltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            InputsManager.Update();

            // Scene update
            if (_scene.IsNotNull() && !_scene.Pause)
            {
                _scene.BeforeUpdate();
                _scene.Update();
                _scene.AfterUpdate();
            }

            // Switch to the next scene
            // =TODO=: pass the previous scene as argument to the next
            if (_scene != _nextScene && _nextScene.IsNotNull())
            {
                _scene?.End();

                _scene?.Dispose();

                _scene = _nextScene;

                _scene?.LoadContent();

                _scene?.Begin();
            }

            RenderManager.Update();
        }

        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);            

            RenderManager.Render(_spriteBatch);
        }

        protected override void OnExiting(object sender, EventArgs args)
        {
            base.OnExiting(sender, args);

            _scene?.End();

            _scene?.Dispose();
        }
    }
}
