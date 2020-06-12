using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using StepStatisticsApp.Models;

namespace StepStatisticsApp.ViewModels
{
    public class UsersViewModel
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

        public static Dictionary<string, List<int>> UsersStepPair { get; private set; } = new Dictionary<string, List<int>>();

        public UsersViewModel()
        {
            ReadJson();
        }

        private static void ReadJson()
        {
            foreach (var fileName in TestDataFileNames)
            {
                Newtonsoft.Json.JsonSerializer serializer = new Newtonsoft.Json.JsonSerializer();
                JsonModel o;
                using (FileStream s = File.Open(Path.Combine(DefaultRootDirectory, fileName), FileMode.Open))
                using (StreamReader sr = new StreamReader(s))
                using (JsonReader reader = new JsonTextReader(sr))
                {
                    while (reader.Read())
                    {
                        if (reader.TokenType == JsonToken.StartObject)
                        {
                            o = serializer.Deserialize<JsonModel>(reader);
                            Add(o.User, o.Steps);
                        }
                    }
                }
            }
        }

        public static void Add(string key, int value)
        {
            if (UsersStepPair.ContainsKey(key))
            {
                List<int> list = UsersStepPair[key];
                if (list.Contains(value) == false)
                {
                    list.Add(value);
                }
            }
            else
            {
                List<int> list = new List<int>();
                list.Add(value);
                UsersStepPair.Add(key, list);
            }
        }
    }
}
