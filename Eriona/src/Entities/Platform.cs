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
        Collider solid1;

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
            solid1 = new Collider(new Transform(110, 480, 900, 15), solid:true);
            Add(solid1);
        }
    }
}
