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

                    TempRemove(collider);

                    foreach (Cell cell in next_cell)
                    {
                        cell.Colliders.Add(collider);
                    }

                    // OR

                    // ForEach((cell) => { if (cell.Colliders.Contains(collider)) { cell.Colliders.Remove(collider); } );
                    // GetCells(collider.WorldTransform).ForEach((cell) => { if (!cell.Colliders.Contains(collider)) cell.Colliders.Add(collider); } ));
                }

                ComputeCollision(collider);
            }

            foreach (Pair<Collider> pair in CollisionPair)
            {
                if (pair.first is ColliderPhysics)
                {
                    ColliderPhysics pairFirst = pair.first as ColliderPhysics;
                    Vector3 data = (Vector3)pair.UserData;

                    if (pair.second.Solid)
                    {
                        pairFirst.Entity.WorldPosition += pairFirst.Velocity * GameCore.DeltaTime * (data.Z);

                        //data.Z.Printl();
                        pairFirst.Velocity = Vector2.Zero;
                        /*
                        "Velocity : ".Print();
                        pairFirst.Velocity.Printl();
                        "Position : ".Print();
                        pairFirst.WorldTransform.Printl();
                        "Time : ".Print();
                        data.Z.Printl();
                        */
                    }
                    pair.first.OnCollide(pair.second, data);
                    pair.second.OnCollide(pair.first, -data);
                }
                else
                {                    
                    pair.first.OnCollide(pair.second);
                    pair.second.OnCollide(pair.first);
                }
            }

            foreach (Collider collider in ColliderList)
            {
                if (collider is ColliderPhysics)
                {
                    Vector2 move = (collider as ColliderPhysics).Velocity * GameCore.DeltaTime;

                    (collider as ColliderPhysics).Entity.WorldPosition.X += (float)Math.Round(move.X);
                    (collider as ColliderPhysics).Entity.WorldPosition.Y += (float)Math.Round(move.Y);
                }
            }
        }

        private void UpdatePhysics()
        {
            foreach (Collider collider in ColliderList)
            {
                if (collider is ColliderPhysics && collider.Active && !collider.Static)
                {
                    ColliderPhysics colliderPhy = collider as ColliderPhysics;

                    colliderPhy.Velocity += (colliderPhy.Acceleration - colliderPhy.Friction * colliderPhy.Velocity) * GameCore.DeltaTime;

                    /*
                    if (colliderPhy.Velocity.X < 0.01f && colliderPhy.Velocity.X > -0.01f)
                        colliderPhy.Velocity.X = 0;
                    if (colliderPhy.Velocity.Y < 0.01f && colliderPhy.Velocity.Y > -0.01f)
                        colliderPhy.Velocity.Y = 0;
                        */
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

        private void TempRemove(Collider collider)
        {
            ForEach((cell) => { if (cell.Colliders.Contains(collider)) { cell.Colliders.Remove(collider); } });
        }

        public void Remove(Collider collider)
        {
            if (!ColliderList.Contains(collider))
            {
                return;
            }
            /*
            foreach (Cell cell in GetCells(collider.WorldTransform))
            {
                if (cell.Colliders.Contains(collider))
                {
                    cell.Colliders.Remove(collider);
                }
            }
            */
            // OR
            ForEach((cell) => { if (cell.Colliders.Contains(collider)) { cell.Colliders.Remove(collider); } });

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

            IEnumerable<Cell> to_test;

            ColliderPhysics colliderPhy = null;
            Vector2 velocity = Vector2.Zero;

            if (collider is ColliderPhysics)
            {
                colliderPhy = collider as ColliderPhysics;
                velocity = colliderPhy.Velocity * GameCore.DeltaTime;
                to_test = GetCells(GetTransformVelocity(colliderPhy.WorldTransform, velocity));
            }
            else
            {
                to_test = GetCells(collider.WorldTransform);
            }

            foreach (Cell cell in to_test)            
            {
                foreach (Collider other in cell.Colliders)
                {
                    test_pair.second = other;

                    if (collider == other || !other.Collidable || CollisionPair.Contains(test_pair))
                        continue;

                    if (colliderPhy.IsNotNull())
                    {
                        if (SweptAABB(colliderPhy, other, velocity, out Vector3 normal))
                        {
                            ComputedCollision[collider].Add(other);
                            ComputedCollision[other].Add(collider);
                            CollisionPair.Add(new Pair<Collider>(collider, other, normal));
                        }
                    }
                    else if (collider.WorldTransform.IntersectBox(other.WorldTransform))
                    {
                        ComputedCollision[collider].Add(other);
                        ComputedCollision[other].Add(collider);
                        CollisionPair.Add(new Pair<Collider>(collider, other));
                    }
                }
            }
        }

        Transform GetTransformVelocity(Transform box, Vector2 velocity)
        {
            Transform to_return = new Transform();
            to_return.X = velocity.X > 0 ? box.X : box.X + velocity.X;
            to_return.Y = velocity.Y > 0 ? box.Y : box.Y + velocity.Y;
            to_return.Width = velocity.X > 0 ? velocity.X + box.Width : box.Width - velocity.X;
            to_return.Height = velocity.Y > 0 ? velocity.Y + box.Height : box.Height - velocity.Y;

            return to_return;
        }

        public static bool SweptAABB(ColliderPhysics origin, Collider other, out Vector3 normal)
        {
            return SweptAABB(origin, other, origin.Velocity, out normal);
        }

        public static bool SweptAABB(ColliderPhysics origin, Collider other, Vector2 velocity, out Vector3 normal)
        {
            Vector2 invEntry, invExit, entry, exit;

            if (velocity.X > 0)
            {
                invEntry.X = other.WorldTransform.Left - origin.WorldTransform.Right;
                invExit.X = other.WorldTransform.Right - origin.WorldTransform.Left;
            }
            else
            {
                invEntry.X = other.WorldTransform.Right - origin.WorldTransform.Left;
                invExit.X = other.WorldTransform.Left - origin.WorldTransform.Right;
            }

            if (velocity.Y > 0)
            {
                invEntry.Y = other.WorldTransform.Top - origin.WorldTransform.Bottom;
                invExit.Y = other.WorldTransform.Bottom - origin.WorldTransform.Top;
            }
            else
            {
                invEntry.Y = other.WorldTransform.Bottom - origin.WorldTransform.Top;
                invExit.Y = other.WorldTransform.Top - origin.WorldTransform.Bottom;
            }

            if (Math.Abs(velocity.X) < 0.00001f)
            {
                entry.X = float.MinValue;
                exit.X = float.MaxValue;
            }
            else
            {
                entry.X = invEntry.X / velocity.X;
                exit.X = invExit.X / velocity.X;
            }

            if (Math.Abs(velocity.Y) < 0.00001f)
            {
                entry.Y = float.MinValue;
                exit.Y = float.MaxValue;
            }
            else
            {
                entry.Y = invEntry.Y / velocity.Y;
                exit.Y = invExit.Y / velocity.Y;
            }

            if (entry.Y > 1.0f) entry.Y = float.MinValue;
            if (entry.X > 1.0f) entry.X = float.MinValue;

            var entryTime = Math.Max(entry.X, entry.Y);
            var exitTime = Math.Min(exit.X, exit.Y);

            if (
                (entryTime > exitTime || entry.X < 0.0f && entry.Y < 0.0f) ||
                (entry.X < 0.0f && (origin.WorldTransform.Right < other.WorldTransform.Left || origin.WorldTransform.Left > other.WorldTransform.Right)) ||
                entry.Y < 0.0f && (origin.WorldTransform.Bottom < other.WorldTransform.Top || origin.WorldTransform.Top > other.WorldTransform.Bottom))
            {
                normal = Vector3.Zero;
                return false;
            }

            Vector2 to_normal;

            if (entry.X > entry.Y)
            {
                to_normal = (invEntry.X < 0.0f) || (Math.Abs(invEntry.X) < 0.00001f && invExit.X < 0) ? Vector2.UnitX : -Vector2.UnitX;
            }
            else
            {
                to_normal = (invEntry.Y < 0.0f || (Math.Abs(invEntry.Y) < 0.00001f && invExit.Y < 0)) ? Vector2.UnitY : -Vector2.UnitY;
            }

            normal = new Vector3(to_normal.X, to_normal.Y, entryTime);

            return true;
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
            foreach (Collider c in ColliderList)
            {
                Primitives2D.DrawRectangle(spritebatch, c.WorldTransform.Bounds, HasCollision(c) ? Color.Red : c.DebugColor);
            }

            for (int i = 1; i <= CellNb.X; i++)
            {
                Primitives2D.DrawLine(spritebatch, new Vector2(Position.X + i * CellSize.X, Position.Y), new Vector2(Position.X + i * CellSize.X, Position.Y + Size.Y), new Color(0, 0, 0, 0.3f));
            }

            for (int i = 1; i <= CellNb.X; i++)
            {
                Primitives2D.DrawLine(spritebatch, new Vector2(Position.X, Position.Y + i * CellSize.Y), new Vector2(Position.X + Size.X, Position.Y + i * CellSize.Y), new Color(0, 0, 0, 0.3f));
            }
        }
    }
}
