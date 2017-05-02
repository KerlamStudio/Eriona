using Blurlib.ECS;
using Blurlib.Render;
using Blurlib.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

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

        // -TODO-: RenderManager
        // -TODO-: InputManager
        private RenderManager _renderManager;

        public GameCore(int width, int height, string title, bool mouseVisible=true, string contentDir="Content")
        {
            Instance = this;

            _windowWidth = width;
            _windowHeight = height;
            WindowTitle = title;
            IsMouseVisible = mouseVisible;
            IsFixedTimeStep = true;

            _graphics = new GraphicsDeviceManager(this);
            _graphics.DeviceCreated += graphics_DeviceCreated;
            _graphics.PreparingDeviceSettings += graphics_PreparingDeviceSettings;

            Content.RootDirectory = contentDir;
        }

        private void graphics_DeviceCreated(object sender, EventArgs e)
        {
            //EmptyKeys.UserInterface.Engine engine = new MonoGameEngine(GraphicsDevice, _windowWidth, _windowHeight);
        }

        private void graphics_PreparingDeviceSettings(object sender, PreparingDeviceSettingsEventArgs e)
        {
            // =TODO=: understand this lol
            _graphics.PreferredBackBufferWidth = WindowWidth;
            _graphics.PreferredBackBufferHeight = WindowHeight;
            _graphics.PreferMultiSampling = true;
            _graphics.GraphicsProfile = GraphicsProfile.HiDef;
            _graphics.SynchronizeWithVerticalRetrace = true;
            _graphics.PreferredDepthStencilFormat = DepthFormat.Depth24Stencil8;
            e.GraphicsDeviceInformation.PresentationParameters.MultiSampleCount = 16;
        }

        protected override void Initialize()
        {
            base.Initialize();

            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // _renderManager.Initialize();
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            DeltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            // -TODO-: InputManager Upadate

            // Scene update
            if (_scene.IsNotNull() && !_scene.Pause)
            {
                _scene.Update();
            }

            // Switch to the next scene
            if (_scene != _nextScene && _nextScene.IsNotNull())
            {
                _scene?.End();

                _scene = _nextScene;
                
                _scene?.Begin();
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            // _renderManager.Draw();
        }

        protected override void OnExiting(object sender, EventArgs args)
        {
            base.OnExiting(sender, args);

            _scene?.End();
        }
    }
}
