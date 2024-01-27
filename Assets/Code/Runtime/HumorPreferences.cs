using System;
using Dev.ComradeVanti.EnumDict;
using UnityEngine;

namespace Dev.ComradeVanti.GGJ24
{
    [Serializable]
    public class HumorPreferences
    {
        [SerializeField] private EnumDict<HumorTypes, float> enjoymentValues =
            new EnumDict<HumorTypes, float>();


        /// <summary>
        /// Gets a value that indicate how much a person enjoys the given
        /// humor-type.
        /// </summary>
        public float this[HumorTypes humorType] => enjoymentValues[humorType];
    }
}