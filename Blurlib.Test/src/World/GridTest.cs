using Blurlib.ECS.Components;
using Blurlib.Physics;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Blurlib.Test.World
{
    public class TempEntity : ECS.Entity
    {
        public Collider collider;
        public Collider collider2;
        public Collider collider3;

        public TempEntity() : base("temp", null, true, true, true)
        {

        }

        public override void Initialize()
        {
            base.Initialize();
            Transform transform = new Transform(25, 25, 100, 100);
            collider = new Collider(transform);

            Transform transform2 = new Transform(50, 50, 100, 100);
            collider2 = new Collider(transform2);

            Transform transform3 = new Transform(200, 200, 100, 100);
            collider3 = new Collider(transform3);

            Add(collider, collider2, collider3);
        }
    }

    [TestFixture]
    public class GridTest
    {
        Grid grid;

        public GridTest()
        {
            grid = new Grid("TEST", 1000, 1000, 50, 50);
            grid.Initialize();
        }

        [Test]
        public void AddTest()
        {
            grid.Reset();

            Transform transform = new Transform(25, 25, 100, 100);

            Collider collider = new Collider(transform);

            Assert.IsEmpty(grid.ColliderList);

            grid.Add(collider);

            Assert.IsNotEmpty(grid.ColliderList);

            Assert.Contains(collider, grid.ComputedCollision.Keys);
        }

        [Test]
        public void RemoveTest()
        {
            grid.Reset();

            Transform transform = new Transform(25, 25, 100, 100);

            Collider collider = new Collider(transform);

            grid.Add(collider);

            grid.Remove(collider);

            Assert.IsEmpty(grid.ColliderList);

            Assert.IsTrue(!grid.ComputedCollision.Keys.Contains(collider));

            grid.ForEach((cell) => Assert.IsTrue(!cell.Colliders.Contains(collider)));
        }

        [Test]
        public void UpdateTest()
        {
            grid.Reset();

            Transform transform = new Transform(25, 25, 100, 100);
            Collider collider = new Collider(transform);

            Transform transform2 = new Transform(50, 50, 100, 100);
            Collider collider2 = new Collider(transform2);

            grid.Add(collider);
            grid.Add(collider2);

            grid.Update();

            collider.Hitbox = new Transform(501, 501, 60, 10);
         
            grid.Update();

            List<Cell> expected = new List<Cell>(grid.GetCells(collider));

            foreach (var c in grid.Cells)
            {
                if (expected.Contains(c))
                    Assert.IsTrue(c.Colliders.Contains(collider), "Cell contain test");
                else
                    Assert.IsFalse(c.Colliders.Contains(collider), "Empty cell contain test at : " + c.GridPosition.X + " , " + c.GridPosition.Y);
            }

        }

        [Test]
        public void GetCellsTest()
        {
            Transform transform = new Transform(0, 0, 100, 100);

            List<Cell> cells = new List<Cell>(grid.GetCells(transform));

            List<Cell> expected = new List<Cell>()
            {


                grid.Cells[0,0],
                grid.Cells[0,1],

                grid.Cells[1,0],
                grid.Cells[1,1],

                grid.Cells[2,0],
                grid.Cells[2,1],

                grid.Cells[0,2],
                grid.Cells[1,2],

                grid.Cells[2,2],

            };

            foreach (var c in cells)
            {
                Assert.IsTrue(expected.Contains(c));
            }
        }

        [Test]
        public void ComputeCollisionTest()
        {
            grid.Reset();

            TempEntity t = new TempEntity();
            t.Initialize();
            t.Awake();

            grid.Add(t.collider);
            grid.Add(t.collider2);
            grid.Add(t.collider3);

            grid.ComputeCollision(t.collider);
            grid.ComputeCollision(t.collider2);
            grid.ComputeCollision(t.collider3);

            Assert.IsTrue(grid.ComputedCollision[t.collider].Contains(t.collider2));

            Assert.IsTrue(grid.ComputedCollision[t.collider2].Contains(t.collider));

            Assert.IsEmpty(grid.ComputedCollision[t.collider3]);
        }

        [Test]
        public void HasCollisionWithTest()
        {
            grid.Reset();

            TempEntity t = new TempEntity();
            t.Initialize();
            t.Awake();

            grid.Add(t.collider);
            grid.Add(t.collider2);
            grid.Add(t.collider3);

            grid.ComputeCollision(t.collider);
            grid.ComputeCollision(t.collider2);
            grid.ComputeCollision(t.collider3);

            Assert.IsTrue(grid.HasCollision(t.collider), "collision 1");
            Assert.IsTrue(grid.HasCollision(t.collider2), "collision 2");
            Assert.IsFalse(grid.HasCollision(t.collider3), "collision 3");
        }
    }
}
