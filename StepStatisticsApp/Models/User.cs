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

        public static int CalculateAvarageStep(List<int> steps)
        {
            int summ = 0;
            foreach (var step in steps)
            {
                summ += step;
            }

            return summ / steps.Count;
        }

        public static int FindBestResult(List<int> steps)
        {
            return steps.Max();
        }

        public static int FindWorstResult(List<int> steps)
        {
            return steps.Min();
        }
    }
}
