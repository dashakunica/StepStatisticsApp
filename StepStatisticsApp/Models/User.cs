using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace StepStatisticsApp.Models
{
    public class User
    {
        public string Name { get; set; }

        public int AvarageStepPerMonth { get; set; }

        public int BestStepResult { get; set; }

        public int WorstStepResult { get; set; }

        public Dictionary<int, int> StepStatistics { get; set; }

        public Dictionary<int, int> RankStatistics { get; set; }

        public Dictionary<int, string> StatusStatistics { get; set; }

        public bool IsUnstableUser { get; set; }

        public static int CalculateAvarageStep(Dictionary<int, int> steps)
        {
            int summ = 0;
            foreach (var step in steps)
            {
                summ += step.Value;
            }

            return summ / steps.Count;
        }

        public static int FindBestResult(Dictionary<int, int> steps)
        {
            return steps.Values.Max();
        }

        public static int FindWorstResult(Dictionary<int, int> steps)
        {
            return steps.Values.Min();
        }

        public static bool IsStabStepUser(int avarageStepPerMonth, int bestStepResult)
        {
            double percent = ((double)avarageStepPerMonth / (double)bestStepResult);
            return (1 - percent) > 0.2;
        }
    }
}
