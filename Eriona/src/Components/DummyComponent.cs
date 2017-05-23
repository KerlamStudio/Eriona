using Blurlib.ECS;
using Blurlib.ECS.Components;
using Blurlib.Render;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Eriona.Components
{
    public class DummyComponent : Sprite
    {
        public DummyComponent(Texture2D texture) : base(texture)
        {

        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void OnAdded(Entity entity)
        {
            base.OnAdded(entity);
        }

        public override void OnEnable()
        {
            base.OnEnable();
        }

        public override void OnDisable()
        {
            base.OnDisable();
        }


        public override void OnRemove()
        {
            base.OnRemove();
        }

        public override void Update()
        {
            base.Update();
        }
    }
}
