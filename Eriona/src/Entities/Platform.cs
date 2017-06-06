using Blurlib.ECS;
using Blurlib.ECS.Components;
using Blurlib.Physics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eriona.Entities
{
    public class Platform : Entity
    {
        SolidCollider solid1;

        public Platform(Vector2 position) : base("Platform", position, visible:true, collidable:true)
        {

        }

        public override void Initialize()
        {
            base.Initialize();            
        }

        public override void OnAdded(Scene scene)
        {
            base.OnAdded(scene);
            solid1 = new SolidCollider(new Transform(10, 480, 1180, 15));
            Add(solid1);
            Add(new Sprite(Scene.Resources.Get<Texture2D>("target_dummy"), textureClip:Rectangle.Empty));
        }
    }
}
