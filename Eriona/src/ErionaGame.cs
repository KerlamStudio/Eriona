using Blurlib;
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

            _nextScene = new DummyScene("Dummy");
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
