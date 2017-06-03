using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Blurlib.Physics;
using Microsoft.Xna.Framework;
using Blurlib.Util;

namespace Blurlib.Test.Physics
{
    [TestFixture]
    public class ForceListTest
    {
        [Test]
        public void ForceTest()
        {
            float mass = 1.5f;
            float g = 9.81f;

            ForceList forces = new ForceList(mass);

            Assert.IsTrue(Vector2.Zero == forces.Acceleration);

            forces.AddForce("Poids", new Vector2(0, mass * -g));

            Assert.IsTrue(new Vector2(0, -g) == forces.Acceleration);

            forces.AddForce("JetPack", new Vector2(2, 12));

            Assert.IsTrue(new Vector2(0, -g) != forces.Acceleration);

            forces.RemoveForce("poids");

            Assert.IsTrue(new Vector2(2, 12) / mass == forces.Acceleration);

            forces.UpdateForce("jetpack", new Vector2(1, 1));

            Assert.IsTrue(new Vector2(1, 1) / mass == forces.Acceleration);
        }
    }
}
