﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Blurlib.Util
{
    public static class Extension
    {
        
        public static string GenerateUniqueId(string type = "None")
        {
            string id = "";
            id += type[0].ToString().ToUpper();
            id += (type.Length - 1).ToString();
            id += Guid.NewGuid().ToString("N");
            return id;
        }
        
        public static byte[] HashStrToByte(string input)
        {
            return MD5.Create().ComputeHash(System.Text.Encoding.ASCII.GetBytes(input));
        }
        
        public static string HashStrToHex(string input, string methode = "X4")
        {
            byte[] hash = MD5.Create().ComputeHash(System.Text.Encoding.ASCII.GetBytes(input));

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < input.Length; i++)
            {
                sb.Append(hash[i].ToString(methode));
            }
            return sb.ToString();
        }
        
        public static float Marge(float a, float b)
        {
            return (a - b) / 2;
        }
        
        public static void Print(this object o)
        {
            if (o.IsNotNull())
                Console.Write(o.ToString());
            else
                Console.Write("null");
        }
        
        public static void Printl(this object o)
        {
            if (o.IsNotNull())
                Console.WriteLine(o.ToString());
            else
                Console.WriteLine("null");
        }
        
        public static bool IsNull(this object source)
        {
            return source == null;
        }
        
        public static bool IsNotNull(this object source)
        {
            return source != null;
        }
        
        public static Vector2 GetPosition(this Rectangle rect)
        {
            return rect.Location.ToVector2();
        }

        public static bool CompareLists<T>(List<T> l1, List<T> l2)
        {
            bool proceed;
            foreach (var e1 in l1)
            {
                proceed = false;
                foreach (var e2 in l2)
                {
                    if (e1.Equals(e2))
                        proceed = true;
                }
                if (!proceed)
                    return false;
            }
            return true;
        }

        public static void SetPosition(this Rectangle rect, Vector2 pos)
        {
            rect.Location = pos.ToPoint();
        }

        public static void Default<T>(this List<T> list, T value, int count)
        {
            list.AddRange(Enumerable.Repeat(value, count)); ;
        }

        public static void Add<T>(this ICollection<T> list, IEnumerable<T> toAdd)
        {
            foreach (T element in toAdd )
            {
                list.Add(element);
            }
        }

        public static void Remove<T>(this ICollection<T> list, IEnumerable<T> toRemove)
        {
            foreach (T element in toRemove)
            {
                if (list.Contains(element))
                    list.Remove(element);
            }
        }

        public static void Benchmark(Action m1, Action m2, int loop = 100000000)
        {
            m1();
            m2();

            var s1 = Stopwatch.StartNew();
            for (int i = 0; i < loop; i++)
            {
                m1();
            }
            s1.Stop();

            var s2 = Stopwatch.StartNew();
            for (int i = 0; i < loop; i++)
            {
                m2();
            }
            s2.Stop();

            Console.WriteLine(((double)(s1.Elapsed.TotalMilliseconds *
                1000000) / loop).ToString("0.00 ns"));
            Console.WriteLine(((double)(s2.Elapsed.TotalMilliseconds *
                1000000) / loop).ToString("0.00 ns"));
            Console.Read();
        }

        public static void ForEach<T>(this IEnumerable<T> list, Action<T> action)
        {
            foreach (T element in list)
            {
                action?.Invoke(element);
            }
        }

        public static float ConvertMetersToPixels(float meters)
        {
            return meters * Constants.MeterToPixels;
        }

        public static float ConvertPixelsToMeters(float pixels)
        {
            return pixels / Constants.MeterToPixels;
        }
        // -TODO- : Add converter methods
        public static Vector2 ConvertMetersToPixels(Vector2 meters)
        {
            return new Vector2(ConvertMetersToPixels(meters.X), ConvertMetersToPixels(meters.Y));
        }
    }
}
