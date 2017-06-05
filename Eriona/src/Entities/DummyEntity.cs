using Blurlib;
using Blurlib.ECS;
using Blurlib.ECS.Components;
using Blurlib.Physics;
using Blurlib.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Eriona.Entities
{
    public class DummyEntity : Entity
    {
        ColliderPhysics collider;

        public DummyEntity() : base("Dummy", new Vector2(0,0), true, true, true)
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

            if (GameCore.InputsManager.IsDown(Keys.Right))
            {
                WorldPosition.X += 5;
            }
            else if (GameCore.InputsManager.IsDown(Keys.Left))
            {
                WorldPosition.X -= 5;
            }

            if (GameCore.InputsManager.IsDown(Keys.Up))
            {
                WorldPosition.Y -= 5;
            }
            else if (GameCore.InputsManager.IsDown(Keys.Down))
            {
                WorldPosition.Y += 5;
            }

            if (GameCore.InputsManager.IsPressed(Keys.Space))
            {
                collider.Forces.AddForce("test", new Vector2(9.83f * collider.Mass, 0));
            }
            if (GameCore.InputsManager.IsPressed(Keys.Enter))
            {
                collider.Forces.RemoveForce("test");
            }

            if (GameCore.InputsManager.IsPressed(Keys.RightShift))
            {
                collider.Velocity = new Vector2(3, 4);
            }

            if (GameCore.InputsManager.IsPressed(Keys.R))
            {
                collider.Velocity = Vector2.Zero;
                collider.Entity.WorldPosition = Vector2.Zero;
            }
            /*
            foreach (Cell cell in collider.GetWorldLayer().GetCurrentCells(collider))
            {
                " - ".Print();
                cell.GridPosition.Print();

            }*/

            collider.GetWorldLayer().GetCurrentCells(collider).Count.Printl();
        }
    }
}
