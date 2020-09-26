using System.Collections.Generic;
using UnityEngine;

public static class Deconstructors
{
    public static void Deconstruct<T1, T2>(this KeyValuePair<T1, T2> tuple, out T1 key, out T2 value)
    {
        key = tuple.Key;
        value = tuple.Value;
    }
    public static void Deconstruct(this Vector2Int vector2Int, out int x, out int y)
    {
        x = vector2Int.x;
        y = vector2Int.y;
    }
}
