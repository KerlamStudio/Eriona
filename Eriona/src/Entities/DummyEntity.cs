using Blurlib;
using Blurlib.ECS;
using Blurlib.ECS.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Eriona.Entities
{
    public class DummyEntity : Entity
    {
        public DummyEntity() : base("Dummy", new Vector2(19,34), true, true, true)
        {
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void OnAdded(Scene scene)
        {
            base.OnAdded(scene);
            Add(new Animation());
        }

        public override void Awake()
        {
            base.Awake();

            Get<Animation>().AddAnimation("test", 0.2f,
                new Frame() { Texture = Scene.Resources.Get<Texture2D>("dummy"), TextureOffset = Vector2.Zero },
                new Frame() { Texture = Scene.Resources.Get<Texture2D>("flamme"), TextureOffset = Vector2.Zero });
            Get<Animation>().AddAnimation("test2", 0.2f,
                new Frame() { Texture = Scene.Resources.Get<Texture2D>("dummy"), TextureOffset = Vector2.Zero });
            Get<Animation>().ChangeAnimation("test");
        }

        public override void OnDisable()
        {
            base.OnDisable();
        }

        public override void OnEnable()
        {
            base.OnEnable();
        }

        public override void OnRemove()
        {
            base.OnRemove();
        }

        public override void Update()
        {
            base.Update();
            if (GameCore.InputsManager.IsReleased(Microsoft.Xna.Framework.Input.Keys.Space))
            {
                Get<Animation>().ChangeAnimation("test2");
            }
            else if (GameCore.InputsManager.IsPressed(Microsoft.Xna.Framework.Input.Keys.Space))
            {
                Get<Animation>().ChangeAnimation("test");
            }

        }
    }
}
