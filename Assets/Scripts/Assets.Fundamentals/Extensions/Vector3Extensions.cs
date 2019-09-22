using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Fundamentals.Extensions
{
    public static class Vector3Extensions
    {
        public static Vector3 FlatOut(this Vector3 point) => new Vector3(point.x, 0, point.z);
    }
}
