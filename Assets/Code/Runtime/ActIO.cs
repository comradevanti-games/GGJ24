#nullable enable

using System.IO;
using System.Threading.Tasks;
using UnityEngine;

namespace Dev.ComradeVanti.GGJ24
{
    /// <summary>
    /// Contains functions for loading <see cref="IAct"/> objects from
    /// storage.
    /// </summary>
    public static class ActIO
    {
        private const string ActsPath = "Acts";

        /// <summary>
        /// Attempts to load an act with a given index. Returns null if
        /// no act with that index exists.
        /// </summary>
        public static Task<IAct?> TryLoadAsync(int index)
        {
            var actPath = Path.Join(ActsPath, $"Act{index}");
            var completionSource = new TaskCompletionSource<IAct?>();

            var request = Resources.LoadAsync<ActAsset>(actPath);
            request.completed += _ =>
                completionSource.SetResult(request.asset as ActAsset);

            return completionSource.Task;
        }
    }
}