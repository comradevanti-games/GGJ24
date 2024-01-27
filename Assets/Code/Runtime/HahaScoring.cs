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
                > 100 => HahaScore.S,
                > 90 => HahaScore.A,
                > 80 => HahaScore.B,
                > 70 => HahaScore.C,
                > 60 => HahaScore.D,
                > 50 => HahaScore.E,
                _ => HahaScore.F
            };

        public static float HahaValueForPerson(IPerson person, HumorEffect effect)
        {
            var multiplier = person.Preferences[effect.Type];
            return multiplier * effect.Strength;
        }
    }
}