using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blurlib.World
{
    public class Transform
    {
        public static Transform Zero = new Transform(0, 0, 0, 0);

        public float X              { get;                                                      set; }
        public float Y              { get;                                                      set; }

        public float Width          { get;                                                      set; }
        public float Height         { get;                                                      set; }

        public float Top            { get { return Y; }                                         set { Y = value; } }
        public float Bottom         { get { return Y + Height; }                                set { Y = value - Height; } }
        public float Left           { get { return X; }                                         set { X = value; } }
        public float Right          { get { return X + Width; }                                 set { X = value - Width; } }

        public float TopLeftX       { get { return Left; }                                      set { Left = value; } }
        public float TopLeftY       { get { return Top; }                                       set { Top = value; } }
        public float TopRightX      { get { return Right; }                                     set { Right = value; } }
        public float TopRightY      { get { return Top; }                                       set { Top = value; } }

        public float BottomLeftX    { get { return Left; }                                      set { Left = value; } }
        public float BottomLeftY    { get { return Bottom; }                                    set { Bottom = value; } }
        public float BottomRightX   { get { return Right; }                                     set { Right = value; } }
        public float BottomRightY   { get { return Bottom; }                                    set { Bottom = value; } }
 
        public float MiddleTopX     { get { return X + Width / 2; }                             set { X = value - Width / 2; } }
        public float MiddleTopY     { get { return Top; }                                       set { Top = value; } }
        public float MiddleLeftX    { get { return Left; }                                      set { Left = value; } }
        public float MiddleLeftY    { get { return Y + Height / 2; }                            set { Y = value - Height / 2; } }
        public float MiddleRightX   { get { return Right; }                                     set { Right = value; } }
        public float MiddleRightY   { get { return Y + Height / 2; }                            set { Y = value - Height / 2; } }
        public float MiddleBottomX  { get { return Bottom; }                                    set { Bottom = value; } }
        public float MiddleBottomY  { get { return X + Width / 2; }                             set { X = value - Width / 2; } }

        public float CenterX        { get { return X + Width / 2; }                             set { X = value - Width / 2; } }
        public float CenterY        { get { return Y + Height / 2; }                            set { Y = value - Height / 2; } }


        public Vector2 Position     { get { return new Vector2(TopLeftX, TopLeftY); }           set { TopLeftX = value.X; TopLeftY = value.Y; } }

        public Vector2 TopLeft      { get { return new Vector2(TopLeftX, TopLeftY); }           set { TopLeftX = value.X; TopLeftY = value.Y; } }
        public Vector2 TopRight     { get { return new Vector2(TopRightX, TopRightY); }         set { TopRightX = value.X; TopRightY = value.Y; } }

        public Vector2 BottomLeft   { get { return new Vector2(BottomLeftX, BottomLeftY); }     set { BottomLeftX = value.X; BottomLeftY = value.Y; } }
        public Vector2 BottomRight  { get { return new Vector2(BottomRightX, BottomRightY); }   set { BottomRightX = value.X; BottomRightY = value.Y; } }

        public Vector2 MiddleTop    { get { return new Vector2(MiddleTopX, MiddleTopY); }       set { MiddleTopX = value.X; MiddleTopY = value.Y; } }
        public Vector2 MiddleLeft   { get { return new Vector2(MiddleLeftX, MiddleLeftY); }     set { MiddleLeftX = value.X; MiddleLeftY = value.Y; } }
        public Vector2 MiddleRight  { get { return new Vector2(MiddleRightX, MiddleRightY); }   set { MiddleRightX = value.X; MiddleRightY = value.Y; } }
        public Vector2 MiddleBottom { get { return new Vector2(MiddleBottomX, MiddleBottomY); } set { MiddleBottomX = value.X; MiddleBottomY = value.Y; } }

        public Vector2 Center       { get { return new Vector2(CenterX, CenterY); }             set { CenterX = value.X; CenterY = value.Y; } }
        public Vector2 Size         { get { return new Vector2(Width, Height); }                set { Width = value.X; Height = value.Y; } }

        public Rectangle Bounds
        {
            get { return new Rectangle(Convert.ToInt32(X), Convert.ToInt32(Y), Convert.ToInt32(Width), Convert.ToInt32(Height)); }
            set { X = value.X; Y = value.Y; Width = value.Width; Height = value.Height; }
        }

        public Transform()
        {
            X = 0;
            Y = 0;
            Width = 0;
            Height = 0;
        }

        public Transform(float x, float y, float width, float height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public Transform(Vector2 position, Vector2 size)
        {
            Position = position;
            Size = size;
        }

        public Transform(float x, float y, Vector2 size)
        {
            X = x;
            Y = y;
            Size = size;
        }

        public Transform(Vector2 position, float width, float height)
        {
            Position = position;
            Width = width;
            Height = height;
        }

        public bool Intersects(Transform other)
        {
            return Bounds.Intersects(other.Bounds);

        }

        public bool AABBCollision(Transform other)
        {
            throw new NotImplementedException();
        }

        public bool AABBCollision(Transform other, out Transform intersection)
        {
            throw new NotImplementedException();
        }

        public static Transform operator+(Transform a, Transform b)
        {
            return new Transform(a.Position + b.Position, a.Size + b.Size);
        }

        public static Transform operator -(Transform a, Transform b)
        {
            return new Transform(a.Position - b.Position, a.Size - b.Size);
        }

        public static Transform operator *(Transform a, Transform b)
        {
            return new Transform(a.Position * b.Position, a.Size * b.Size);
        }

        public static Transform operator /(Transform a, Transform b)
        {
            return new Transform(a.Position / b.Position, a.Size / b.Size);
        }

        public static bool operator ==(Transform a, Transform b)
        {
            return a.Position == b.Position && a.Size == b.Size;
        }

        public static bool operator !=(Transform a, Transform b)
        {
            return a.Position != b.Position || a.Size != b.Size;
        }

        public override bool Equals(object obj)
        {
            return this == (obj as Transform);
        }

        public override int GetHashCode()
        {
            return X.GetHashCode() * Y.GetHashCode() * Width.GetHashCode() * Height.GetHashCode();
        }

    }
}
