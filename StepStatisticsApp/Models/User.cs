using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace StepStatisticsApp.Models
{
    public class User : INotifyPropertyChanged
    {
        public string Name { get; set; }

        public int AvarageStepPerMonth { get; set; }

        public int BestStepResult { get; set; }

        public int WorstStepResult { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

    }
}
