#nullable enable

using System;
using System.Collections.Immutable;
using System.Linq;
using UnityEngine;

namespace Dev.ComradeVanti.GGJ24
{
    [Serializable]
    public class Crowd : ICrowd
    {
        [SerializeField] private Person[] people = Array.Empty<Person>();


        public ImmutableArray<IPerson> People =>
            people.Cast<IPerson>().ToImmutableArray();
    }
}