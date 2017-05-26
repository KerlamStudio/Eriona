using Blurlib.ECS.Components;
using Blurlib.World;
using Microsoft.Xna.Framework;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blurlib.Test.World
{
    [TestFixture]
    public class GridTest
    {
        Grid grid;

        public GridTest()
        {
            grid = new Grid(1000, 1000, 50, 50);
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

            Transform transform = new Transform(25, 25, 100, 100);
            Collider collider = new Collider(transform);

            Transform transform2 = new Transform(50, 50, 100, 100);
            Collider collider2 = new Collider(transform2);

            Transform transform3 = new Transform(200, 200, 100, 100);
            Collider collider3 = new Collider(transform3);

            grid.Add(collider);
            grid.Add(collider2);
            grid.Add(collider3);

            grid.ComputeCollision(collider);
            grid.ComputeCollision(collider2);
            grid.ComputeCollision(collider3);

            Assert.IsTrue(grid.ComputedCollision[collider].Contains(collider2));

            Assert.IsTrue(grid.ComputedCollision[collider2].Contains(collider));

            Assert.IsEmpty(grid.ComputedCollision[collider3]);
        }

        [Test]
        public void HasCollisionWithTest()
        {
            grid.Reset();

            Transform transform = new Transform(25, 25, 100, 100);
            Collider collider = new Collider(transform);

            Transform transform2 = new Transform(50, 50, 100, 100);
            Collider collider2 = new Collider(transform2);

            Transform transform3 = new Transform(200, 200, 100, 100);
            Collider collider3 = new Collider(transform3);

            grid.Add(collider);
            grid.Add(collider2);
            grid.Add(collider3);

            grid.ComputeCollision(collider);
            grid.ComputeCollision(collider3);

            Assert.IsTrue(grid.HasCollision(collider));
            Assert.IsTrue(grid.HasCollision(collider2));
            Assert.IsFalse(grid.HasCollision(collider3));
        }
    }
}
