using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Dev.ComradeVanti.GGJ24
{
    public static class HahaScoring
    {
        public static HahaScore HahaScoreFromHahaValue(float hahaValue) =>
            hahaValue switch
            {
                > 10 => HahaScore.S,
                > 9 => HahaScore.A,
                > 8 => HahaScore.B,
                > 7 => HahaScore.C,
                > 6 => HahaScore.D,
                > 5 => HahaScore.E,
                _ => HahaScore.F
            };

        public static float HahaValueForPerson(IPerson person, HumorEffect effect)
        {
            var multiplier = person.Preferences[effect.Type];
            return multiplier * effect.Strength;
        }
    }
}