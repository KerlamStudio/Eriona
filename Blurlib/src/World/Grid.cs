using Microsoft.Xna.Framework;
using System;

namespace Blurlib.src.World
{
    public class Grid
    {
        private Cell[,] _cells;

        public Vector2 Size { get; private set; }
        public Vector2 CellSize { get; private set; }
        public Vector2 CellNb { get; private set; }

        public Grid(float width, float height, float cellWidth, float cellHeight)
        {
            Size = new Vector2(width, height);
            CellSize = new Vector2(cellWidth, cellHeight);
            CellNb = new Vector2(width / cellWidth, height/ cellHeight);            
        }

        public void Initialize()
        {
            _cells = new Cell[(int)CellNb.X, (int)CellNb.Y];

            // Initialize all Cells in the array
            ForEach((cell) => cell = new Cell());
        }

        public void Update()
        {

        }

        public void Add()
        {

        }

        public void Remove()
        {

        }

        public void ForEach(Action<Cell> action)
        {
            for (int i = 0; i < CellNb.X; i++)
            {
                for (int j = 0; j < CellNb.Y; j++)
                {
                    action.Invoke(_cells[i, j]);
                }
            }
            
        }
    }
}
