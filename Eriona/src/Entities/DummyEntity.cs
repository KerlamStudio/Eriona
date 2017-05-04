using Blurlib.ECS;
using Blurlib.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eriona.Entities
{
    public class DummyEntity : Entity
    {
        public DummyEntity() : base("Dummy", new Transform(100,100,19,34), true, true, true)
        {

        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void OnAdded(Scene scene)
        {
            base.OnAdded(scene);
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
