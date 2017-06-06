using System;
using System.Collections.Generic;

namespace Blurlib.Util
{
    public struct Pair<T> : IEquatable<Pair<T>> where T : class
    {
        public T first;
        public T second;

        public object UserData;

        public Pair(T first, T second, object userDAta = null)
        {
            this.first = first;
            this.second = second;
            UserData = userDAta;
        }


        public void Clear()
        {
            first = second = null;
            UserData = null;
        }


        public bool Equals(Pair<T> other)
        {
            // these two ways should be functionaly equivalent
            return (first == other.first && second == other.second) || (first == other.second && second == other.first);

            //return EqualityComparer<T>.Default.Equals( first, other.first ) &&
            //	EqualityComparer<T>.Default.Equals( second, other.second );
        }


        public override int GetHashCode()
        {
            return (EqualityComparer<T>.Default.GetHashCode(first) + EqualityComparer<T>.Default.GetHashCode(second)) * 37;
        }
        
    }
}
