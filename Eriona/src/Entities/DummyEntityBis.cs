using Blurlib;
using Blurlib.ECS;
using Blurlib.ECS.Components;
using Blurlib.Physics;
using Blurlib.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Eriona.Entities
{
    public class DummyEntityBis : Entity
    {
        ColliderPhysics collider;

        public DummyEntityBis(string id="") : base("DummyBis" + id, new Vector2(0, 0), true, true, true)
        {
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void OnAdded(Scene scene)
        {
            base.OnAdded(scene);
            Add(new Sprite(Scene.Resources.Get<Texture2D>("target_dummy"), true, 0, null, new Vector2(-Scene.Resources.Get<Texture2D>("target_dummy").Width / 2, -Scene.Resources.Get<Texture2D>("target_dummy").Height / 2)));
            collider = new ColliderPhysics(
                new Transform(
                    -Scene.Resources.Get<Texture2D>("target_dummy").Width / 2,
                    -Scene.Resources.Get<Texture2D>("target_dummy").Height / 2,
                    Scene.Resources.Get<Texture2D>("target_dummy").Width,
                    Scene.Resources.Get<Texture2D>("target_dummy").Height),
                0.5f, null, 0.5f);
            Add(collider);
        }

        public override void Awake()
        {
            base.Awake();
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

            if (GameCore.InputsManager.IsDown(Keys.D))
            {
                WorldPosition.X += 5;
            }
            else if (GameCore.InputsManager.IsDown(Keys.Q))
            {
                WorldPosition.X -= 5;
            }

            if (GameCore.InputsManager.IsDown(Keys.Z))
            {
                WorldPosition.Y -= 5;
            }
            else if (GameCore.InputsManager.IsDown(Keys.S))
            {
                WorldPosition.Y += 5;
            }

            /*
            if (collider.GetWorldLayer().HasCollision(collider))
            { ("Collide ! " + (collider.GetWorldLayer().GetCollisions(collider) as ICollection<Collider>).Count.ToString()).Printl(); }
            */

        }
    }
}
