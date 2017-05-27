using Blurlib.ECS.Components;
using Blurlib.Util;
using Blurlib.World;
using Microsoft.Xna.Framework;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Blurlib.World
{
    public class Grid
    {
        private Cell[,] _cells;
        public Cell[,] Cells { get { return _cells; } }

        public Vector2 Size { get; private set; }
        public Vector2 CellSize { get; private set; }
        public Vector2 CellNb { get; private set; }
        public Vector2 Position { get; private set; }
        public HashSet<Collider> ColliderList { get; private set; }
        public Dictionary<Collider, HashSet<Collider>> ComputedCollision { get; private set; }
        public HashSet<Pair<Collider>> CollisionPair { get; private set; }
        
        public Grid(float width, float height, float cellWidth, float cellHeight, float x=0, float y=0)
        {
            Size = new Vector2(width, height);
            CellSize = new Vector2(cellWidth, cellHeight);
            CellNb = new Vector2(width / cellWidth, height/ cellHeight);
            Position = new Vector2(x, y);
            ColliderList = new HashSet<Collider>();
            ComputedCollision = new Dictionary<Collider, HashSet<Collider>>();
            CollisionPair = new HashSet<Pair<Collider>>();
        }

        public void Initialize()
        {
            _cells = new Cell[(int)CellNb.X, (int)CellNb.Y];

            // Initialize all Cells in the array
            for (int i=0; i < (int)CellNb.X; i++)
            {
                for (int j = 0; j < (int)CellNb.Y; j++)
                {
                    _cells[i, j] = new Cell()
                    {
                        GridPosition = new Vector2(i, j),
                        WordlPosition = new Vector2(Position.X + i * CellSize.X, Position.Y + j * CellSize.Y),
                        Colliders = new HashSet<Collider>()
                    };
                }
            }
        }

        // !TODO! : Verify 
        public void Update()
        {
            CollisionPair.Clear();

            foreach (HashSet<Collider> collider_list in ComputedCollision.Values)
            {
                collider_list.Clear();
            }

            foreach (Collider collider in ColliderList)
            {
                if (collider.Changed)
                {
                    List<Cell> next_cell = new List<Cell>(GetCells(collider.WorldTransform));

                    foreach (Cell cell in GetCells(
                        collider.LastPosition.X, 
                        collider.LastPosition.Y, 
                        collider.LastHitbox.Width, 
                        collider.LastHitbox.Height))
                    {
                        if (!next_cell.Contains(cell))
                        {
                            cell.Colliders.Remove(collider);
                        }
                        else
                        {
                            next_cell.Remove(cell);
                        }
                    }
                    
                    foreach (Cell cell in next_cell)
                    {
                        if (!cell.Colliders.Contains(collider))
                        {
                            cell.Colliders.Add(collider);
                        }
                    }

                    // OR

                    // ForEach((cell) => { if (cell.Colliders.Contains(collider)) { cell.Colliders.Remove(collider); } );
                    // GetCells(collider.WorldTransform).ForEach((cell) => { if (!cell.Colliders.Contains(collider)) cell.Colliders.Add(collider); } ));
                }

                ComputeCollision(collider);
            }
        }

        public void Add(Collider collider)
        {
            if (ColliderList.Contains(collider))
            {
                return;
            }

            foreach (Cell cell in GetCells(collider.WorldTransform))
            {
                cell.Colliders.Add(collider);
            }

            ComputedCollision[collider] = new HashSet<Collider>();

            ColliderList.Add(collider);
        }

        public void Remove(Collider collider)
        {
            if (!ColliderList.Contains(collider))
            {
                return;
            }

            foreach (Cell cell in GetCells(collider.WorldTransform))
            {
                if (cell.Colliders.Contains(collider))
                {
                    cell.Colliders.Remove(collider);
                }
            }

            // OR
            // ForEach((cell) => { if (cell.Colliders.Contains(collider)) { cell.Colliders.Remove(collider); } );

            ComputedCollision.Remove(collider);

            ColliderList.Remove(collider);
        }

        public void Reset()
        {
            ForEach((cell) => cell.Colliders.Clear());

            ComputedCollision.Clear();

            ColliderList.Clear();
        }

        public IEnumerable<Cell> GetCells(Collider collider)
        {
            return GetCells(collider.WorldTransform);
        }

        public IEnumerable<Cell> GetCells(Transform hitbox)
        {
            return GetCells(hitbox.X, hitbox.Y, hitbox.Width, hitbox.Height);
        }

        public IEnumerable<Cell> GetCells(float x, float y, float w, float h)
        {
            for (int i = 0; i <= w / CellSize.X; i++)
            {
                for (int j = 0; j <= h / CellSize.Y; j++)
                {
                    yield return _cells[i + (int)(x / CellSize.X), j + (int)(y / CellSize.Y)];
                }
            }
        }
                
        public bool HasCollision(Collider collider)
        {
            if (ComputedCollision.ContainsKey(collider) && ComputedCollision[collider].Count > 0)
            {
                return true;
            }

            return false;
        }

        public IEnumerable<Collider> GetCollisions(Collider collider)
        {
            if (!ComputedCollision.ContainsKey(collider))
            {
                return null;
            }

            return ComputedCollision[collider];
        }

        public void ComputeCollision(Collider collider)
        {
            if (!ComputedCollision.ContainsKey(collider) || !collider.Collidable)
            {
                return;
            }

            Pair<Collider> test_pair = new Pair<Collider>();
            test_pair.first = collider;

            foreach (Cell cell in GetCells(collider.WorldTransform))
            {
                foreach (Collider other in cell.Colliders)
                {
                    test_pair.second = other;
                        
                    if (collider == other || !other.Collidable || CollisionPair.Contains(test_pair))
                        continue;

                    if (collider.Hitbox.IntersectBox(other.Hitbox))
                    {
                        ComputedCollision[collider].Add(other);
                        ComputedCollision[other].Add(collider);
                        CollisionPair.Add(new Pair<Collider>(collider, other));
                    }
                }
            }            
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
