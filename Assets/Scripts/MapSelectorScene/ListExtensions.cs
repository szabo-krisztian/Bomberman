using System;
using System.Collections.Generic;

public static class ListExtensions
{
    private static System.Random rng = new System.Random();
    
    /// <summary>
    /// Shuffles a collection of data.
    /// </summary>
    /// <typeparam name="T">Generic type.</typeparam>
    /// <param name="list">Collection paratemer.</param>
    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}
