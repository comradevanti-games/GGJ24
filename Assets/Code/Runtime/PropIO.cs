using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Dev.ComradeVanti.GGJ24
{
    /// <summary>
    /// Contains functions for loading <see cref="IProp"/> objects.
    /// </summary>
    public static class PropIO
    {
        private const string Path = "Props";


        /// <summary>
        /// Loads all props.
        /// </summary>
        public static Task<IImmutableSet<IProp>> LoadAllAsync() =>
            Task.Run(() =>
                Resources.LoadAll<PropAsset>(Path)
                         .Cast<IProp>()
                         .ToImmutableHashSet() as IImmutableSet<IProp>);
    }
}