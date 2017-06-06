using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blurlib.Util
{
    public class BTuple : IEquatable<BTuple>                           
    {
        public object First;
        public object Second;
        public object UserData;

        public BTuple()
        {
        }

        public BTuple(object first, object second, object userData=null)
        {
            First = first;
            Second = second;
            UserData = userData;
        }

        public void Clear()
        {
            First = null;
            Second = null;
            UserData = null;
        }

        public bool Equals(BTuple other)
        {
            return First == other.First && Second == other.Second;
        }
    }
}
