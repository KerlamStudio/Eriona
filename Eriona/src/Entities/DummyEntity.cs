using Blurlib.ECS;
using Blurlib.World;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eriona.Entities
{
    public class DummyEntity : Entity
    {
        public DummyEntity(params Component[] components) : base("Dummy", new Vector2(19,34), true, true, true)
        {
            Add(components);
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void OnAdded(Scene scene)
        {
            base.OnAdded(scene);
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
        }
    }
}
