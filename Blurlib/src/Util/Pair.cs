using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blurlib.Util
{
    public struct Pair<T> : IEquatable<Pair<T>> where T : class
    {
        public T first;
        public T second;


        public Pair(T first, T second)
        {
            this.first = first;
            this.second = second;
        }


        public void Clear()
        {
            first = second = null;
        }


        public bool Equals(Pair<T> other)
        {
            // these two ways should be functionaly equivalent
            return first == other.first && second == other.second;

            //return EqualityComparer<T>.Default.Equals( first, other.first ) &&
            //	EqualityComparer<T>.Default.Equals( second, other.second );
        }


        public override int GetHashCode()
        {
            return (EqualityComparer<T>.Default.GetHashCode(first) + EqualityComparer<T>.Default.GetHashCode(second)) * 37;
        }
        
    }
}
