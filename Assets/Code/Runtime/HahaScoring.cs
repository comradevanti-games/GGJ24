using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Dev.ComradeVanti.GGJ24
{
    public static class HahaScoring
    {
        private static HahaScore HahaScoreFromHahaValue(float hahaValue) =>
            hahaValue switch
            {
                > 10 => HahaScore.S,
                > 8 => HahaScore.A,
                > 6 => HahaScore.B,
                > 4 => HahaScore.C,
                > 2 => HahaScore.D,
                > 0 => HahaScore.E,
                _ => HahaScore.F
            };

        public static HahaScore HahaScoreForPerson(IPerson person, HumorEffect effect)
        {
            var multiplier = person.Preferences[effect.Type];
            var hahaValue = multiplier * effect.Strength;
            return HahaScoreFromHahaValue(hahaValue);
        }

        public static HahaScore AverageHahaScores(IEnumerable<HahaScore> scores)
        {
            var average = (float) scores.Cast<int>().Average();
            return (HahaScore) Mathf.FloorToInt(average);
        }
    }
}