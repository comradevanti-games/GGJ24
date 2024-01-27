#nullable enable

using System;
using UnityEngine;

namespace Dev.ComradeVanti.GGJ24
{
    [Serializable]
    public class Person : IPerson
    {

        [SerializeField] private HumorPreferences preferences = null!;


        public HumorPreferences Preferences => preferences;

    }
}