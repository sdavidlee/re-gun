using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public static class PlayerAssignerExtensions
{
    public static List<string> GetKeys(this IEnumerable<PropertyInfo> @this, object obj)
    {
        List<string> values = new List<string>();

        foreach (var prop in @this)
            values.Add(prop.GetValue(obj).ToString());

        return values;
    }

    public static List<string> RemoveElements(this List<string> @this, params string[] toBeRemoved)
    {
        var result = @this.ToList();

        foreach (var e in @this)
            foreach (var item in toBeRemoved)
                if (e.Contains(item))
                    result.Remove(e);

        return result;
    }
}