using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Fundamentals.Extensions
{
    public static class ArrayExtensions
    {
        public static T[] ClearAll<T>(this T[] @this)
        {
            Array.Clear(@this, 0, @this.Length);
            return @this;
        }
    }
}
