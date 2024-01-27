#nullable enable

using System;
using UnityEngine;

namespace Dev.ComradeVanti.GGJ24
{
    /// <summary>
    /// Contains logic for interacting with the persisted act-index.
    /// </summary>
    public static class ActIndexPersistence
    {
        private const string PlayerPrefKey = "ActIndex";

        private const int FirstIndex = 0;


        public static int Get() =>
            PlayerPrefs.GetInt(PlayerPrefKey, FirstIndex);

        public static void Set(int actIndex) =>
            PlayerPrefs.SetInt(PlayerPrefKey, actIndex);

        public static void Reset() =>
            Set(0);
    }
}