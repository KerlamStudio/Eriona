using Blurlib.ECS.Components;
using Blurlib.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Blurlib.Physics
{
    public class Grid
    {
        public string Id { get; private set; }

        private Cell[,] _cells;
        public Cell[,] Cells { get { return _cells; } }

        public Vector2 Size { get; private set; }
        public Vector2 CellSize { get; private set; }
        public Vector2 InverseCellSize { get; private set; }
        public Vector2 CellNb { get; private set; }
        public Vector2 Position { get; private set; }
        public HashSet<Collider> ColliderList { get; private set; }
        public Dictionary<Collider, HashSet<Collider>> ComputedCollision { get; private set; }
        public HashSet<Pair<Collider>> CollisionPair { get; private set; }

        public Grid(string id, float width, float height, float cellWidth, float cellHeight, float x = 0, float y = 0)
        {
            Id = id;
            Size = new Vector2(width, height);
            CellSize = new Vector2(cellWidth, cellHeight);
            InverseCellSize = new Vector2(1 / cellWidth, 1 / cellHeight);
            CellNb = new Vector2(width * InverseCellSize.X, height * InverseCellSize.Y);
            Position = new Vector2(x, y);
            ColliderList = new HashSet<Collider>();
            ComputedCollision = new Dictionary<Collider, HashSet<Collider>>();
            CollisionPair = new HashSet<Pair<Collider>>();
        }

        public void Initialize()
        {
            _cells = new Cell[(int)CellNb.X, (int)CellNb.Y];

            // Initialize all Cells in the array
            for (int i = 0; i < (int)CellNb.X; i++)
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

            UpdatePhysics();

            foreach (Collider collider in ColliderList)
            {
                if (collider.Changed)
                {
                    List<Cell> next_cell = new List<Cell>(GetCells(collider.WorldTransform));
                    /*
                    foreach (Cell cell in GetCells(
                        collider.LastPosition.X,
                        collider.LastPosition.Y,
                        collider.LastHitbox.Width,
                        collider.LastHitbox.Height))*/
                   // =TODO= : optimize grid update
                    foreach (Cell cell in GetCurrentCells(collider))
                    {
                        cell.Colliders.Remove(collider);
                        /*
                        if (!next_cell.Contains(cell))
                        {
                            cell.Colliders.Remove(collider);
                        }
                        else
                        {
                            next_cell.Remove(cell);
                        }
                        */
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

            foreach (KeyValuePair<Collider, HashSet<Collider>> pair in ComputedCollision)
            {
                foreach(Collider to_collide in pair.Value)
                {
                    pair.Key.OnCollide(to_collide);
                }
            }
        }

        private void UpdatePhysics()
        {
            foreach (Collider collider in ColliderList)
            {
                if (collider is ColliderPhysics && collider.Active)
                {
                    ColliderPhysics colliderPhy = collider as ColliderPhysics;

                    colliderPhy.Velocity += (colliderPhy.Acceleration - colliderPhy.Friction * colliderPhy.Velocity) * GameCore.DeltaTime;

                    if (colliderPhy.Velocity.X < 0.01f && colliderPhy.Velocity.X > -0.01f)
                        colliderPhy.Velocity.X = 0;
                    if (colliderPhy.Velocity.Y < 0.01f && colliderPhy.Velocity.Y > -0.01f)
                        colliderPhy.Velocity.Y = 0;

                    colliderPhy.Entity.WorldPosition += Extension.ConvertMetersToPixels(colliderPhy.Velocity) * GameCore.DeltaTime;
                }
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

        public List<Cell> GetCurrentCells(Collider collider)
        {
            List<Cell> to = new List<Cell>();
            foreach (Cell cell in _cells)
            {
                if (cell.Colliders.Contains(collider))
                    to.Add(cell);
            }
            return to;
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
            /*
            int px = (int)((x - Position.X) * InverseCellSize.X);
            int py = (int)((y - Position.Y) * InverseCellSize.Y);

            
            for (int i = 0; i <= w * InverseCellSize.X; i++)
            {
                for (int j = 0; j <= h * InverseCellSize.Y; j++)
                {
                    if (i + px >= 0 && i + px < CellNb.X && j + py >= 0 && j + py < CellNb.Y)
                        yield return _cells[i + px, j + py];
                }
            }
            */

            int minX = (int)Math.Truncate(x / Size.X * CellNb.X);
            int maxX = (int)Math.Truncate((x + w) / Size.X * CellNb.X);

            int minY = (int)Math.Truncate(y / Size.Y * CellNb.Y);
            int maxY = (int)Math.Truncate((y + h) / Size.Y * CellNb.Y);

            for (int i = minX; i <= maxX; i++)
            {
                for (int j = minY; j <= maxY; j++)
                {
                    if (i >= 0 && i < CellNb.X && j + j >= 0 && j < CellNb.Y)
                        yield return _cells[i, j];
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

            Pair<Collider> test_pair = new Pair<Collider>(collider, null);

            foreach (Cell cell in GetCells(collider.WorldTransform))
            {
                foreach (Collider other in cell.Colliders)
                {
                    test_pair.second = other;

                    if (collider == other || !other.Collidable || CollisionPair.Contains(test_pair))
                        continue;

                    if (collider.WorldTransform.IntersectBox(other.WorldTransform))
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

        public override int GetHashCode()
        {
            int hash = 17;

            hash = hash * 23 + Cells.GetHashCode();
            hash = hash * 23 + Size.GetHashCode();
            hash = hash * 23 + CellSize.GetHashCode();
            hash = hash * 23 + Position.GetHashCode();

            return hash;
        }

        public void DebugDraw(SpriteBatch spritebatch)
        {
            for (int i = 0; i < CellNb.X; i++)
            {
                Primitives2D.DrawLine(spritebatch, new Vector2(Position.X + i * CellSize.X, Position.Y), new Vector2(Position.X + i * CellSize.X, Position.Y + Size.Y), new Color(0, 0, 0, 0.3f));
            }

            for (int i = 0; i < CellNb.X; i++)
            {
                Primitives2D.DrawLine(spritebatch, new Vector2(Position.X, Position.Y + i * CellSize.Y), new Vector2(Position.X + Size.X, Position.Y + i * CellSize.Y), new Color(0, 0, 0, 0.3f));
            }
        }
    }
}
