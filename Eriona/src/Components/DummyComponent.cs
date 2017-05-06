using Blurlib.ECS;
using Blurlib.Render;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Eriona.Components
{
    public class DummyComponent : Component, IDraw
    {
        public int ZIndex => 2;

        public Texture2D Texture { get; set; }

        public Rectangle? TextureClip => null;

        public Vector2 TexturePosition => WorldPosition;

        public Vector2 TextureLocalTranslate => Vector2.Zero;

        public Color TextureColorFilter => Color.Blue;

        public DummyComponent() : base(true, true)
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
