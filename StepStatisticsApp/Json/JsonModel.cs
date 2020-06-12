using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace StepStatisticsApp
{
    public class JsonModel
    {
        public int Rank { get; set; }

        public string User { get; set; }

        public string Status { get; set; }

        public int Steps { get; set; }

    }
}
