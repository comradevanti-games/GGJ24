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
                > 24 => HahaScore.S,
                > 20 => HahaScore.A,
                > 16 => HahaScore.B,
                > 12 => HahaScore.C,
                > 8 => HahaScore.D,
                > 4 => HahaScore.E,
                _ => HahaScore.F
            };

        public static float HahaValueForPerson(IPerson person, HumorEffect effect)
        {
            var multiplier = person.Preferences[effect.Type];
            return multiplier * effect.Strength;
        }
    }
}