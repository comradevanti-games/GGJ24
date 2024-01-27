#nullable enable

using System;
using UnityEngine;

namespace Dev.ComradeVanti.GGJ24
{
    /// <summary>
    /// Utility functions for finding singleton components in the current scene.
    /// Such components must be on a game-object tagged "Singletons".
    /// There may only be one such game-object per scene.
    /// </summary>
    public static class Singletons
    {
        private static GameObject TryGetSingletonHost()
        {
            var host = GameObject.FindWithTag("Singletons");
            if (!host) throw new NullReferenceException("No singleton host in the current scene!");
            return host;
        }

        /// <summary>
        /// Attempts to find a singleton of a specific type. Null if not found.
        /// </summary>
        public static T? TryFind<T>() =>
            TryGetSingletonHost().GetComponent<T>();

        /// <summary>
        /// Attempts to find a singleton of a specific type. 
        /// </summary>
        /// <exception cref="NullReferenceException">
        /// Thrown if singleton is not found.
        /// </exception>
        public static T Require<T>()
        {
            var singleton = TryFind<T>();
            if (singleton == null)
                throw new NullReferenceException($"Singleton of type {typeof(T).Name} was required but not found.");
            return singleton!;
        }
    }
}