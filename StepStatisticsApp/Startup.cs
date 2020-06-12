using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.IO;
using Microsoft.Extensions.Configuration;

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

        private static readonly string DefaultRootDirectory = AppDomain.CurrentDomain.BaseDirectory;

        static Startup()
        {
            foreach (var fileName in TestDataFileNames)
            {
                if (!File.Exists(fileName))
                {
                    Environment.Exit(1);
                }

                Configuration = new ConfigurationBuilder().AddJsonFile(fileName).Build();
            }
        }

        public static IConfiguration Configuration { get; set; }
    }
}
