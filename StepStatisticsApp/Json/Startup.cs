using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.IO;
using Newtonsoft.Json;

namespace StepStatisticsApp
{
    public static class Startup
    {
        private static readonly IEnumerable<String> TestDataFileNames = new ReadOnlyCollection<string>
            (new List<String>
            {
            @"day1.json",
            @"day2.json",
            @"day3.json",
            @"day4.json",
            @"day5.json",
            @"day6.json",
            @"day7.json",
            @"day8.json",
            @"day9.json",
            @"day10.json",
            @"day11.json",
            @"day12.json",
            @"day13.json",
            @"day14.json",
            @"day15.json",
            @"day16.json",
            @"day17.json",
            @"day18.json",
            @"day19.json",
            @"day20.json",
            @"day21.json",
            @"day22.json",
            @"day23.json",
            @"day24.json",
            @"day25.json",
            @"day26.json",
            @"day27.json",
            @"day28.json",
            @"day29.json",
            @"day30.json",
            });

        private static readonly string DefaultRootDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestData");

        static Startup()
        {
            ReadJson();
        }

        public static Dictionary<string, Dictionary<int, int>> UsersStepPair { get; private set; } = new Dictionary<string, Dictionary<int, int>>();

        public static Dictionary<string, Dictionary<int, int>> UsersRankPair { get; private set; } = new Dictionary<string, Dictionary<int, int>>();

        public static Dictionary<string, Dictionary<int, string>> UsersStatusPair { get; private set; } = new Dictionary<string, Dictionary<int, string>>();

        private static void ReadJson()
        {
            int day = 1;
            foreach (var fileName in TestDataFileNames)
            {
                Newtonsoft.Json.JsonSerializer serializer = new Newtonsoft.Json.JsonSerializer();
                JsonModel jsonModel;
                using (FileStream s = File.Open(Path.Combine(DefaultRootDirectory, fileName), FileMode.Open))
                using (StreamReader sr = new StreamReader(s))
                using (JsonReader reader = new JsonTextReader(sr))
                {
                    while (reader.Read())
                    {
                        if (reader.TokenType == JsonToken.StartObject)
                        {
                            jsonModel = serializer.Deserialize<JsonModel>(reader);
                            AddToDict(day, jsonModel.User, jsonModel.Steps, jsonModel.Rank, jsonModel.Status);
                        }
                    }
                }

                day++;
            }
        }

        private static void AddToDict(int day, string key, int stepValue, int rankValue, string statusValue)
        {
            if (UsersStepPair.ContainsKey(key))
            {
                Dictionary<int, int> stepList = UsersStepPair[key];
                Dictionary<int, int> rankList = UsersRankPair[key];
                Dictionary<int, string> statusList = UsersStatusPair[key];

                stepList.Add(day, stepValue);
                rankList.Add(day, rankValue);
                statusList.Add(day, statusValue);
            }
            else
            {
                Dictionary<int, int> stepList= new Dictionary<int, int>();
                Dictionary<int, int> rankList = new Dictionary<int, int>();
                Dictionary<int, string> statusList = new Dictionary<int, string>();

                stepList.Add(day, stepValue);
                rankList.Add(day, rankValue);
                statusList.Add(day, statusValue);

                UsersStepPair.Add(key, stepList);
                UsersRankPair.Add(key, rankList);
                UsersStatusPair.Add(key, statusList);
            }
        }
    }
}
