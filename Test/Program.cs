using System;
using StepStatisticsApp.ViewModels;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            UsersViewModel.Inicialize();
            var result = UsersViewModel.UsersStepPair;
            foreach (var item in result)
            {
                Console.WriteLine(item.Key);
                foreach (var step in item.Value)
                {
                    Console.WriteLine(step);
                }
                Console.WriteLine("..............");
            }
        }
    }
}
