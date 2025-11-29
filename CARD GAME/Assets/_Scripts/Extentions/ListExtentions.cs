using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ListExtentions 
{
    public static T Draw<T>(this List<T> list)
    {
        // Return null immediately if the list is empty (no change here, but cleaner).
        if (list.Count == 0) return default;

        // Use RemoveAt for better performance than list.Remove(t)
        int r = Random.Range(0, list.Count);
        T t = list[r];

        // Use RemoveAt to remove the element by index, which is faster.
        list.RemoveAt(r);

        return t;
    }
}
