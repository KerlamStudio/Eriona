using Blurlib.World;
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
        public void UpdateTest()
        {

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
    }
}
