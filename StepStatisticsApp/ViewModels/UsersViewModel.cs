using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using StepStatisticsApp.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using LiveCharts;
using LiveCharts.Wpf;
using LiveCharts.Defaults;

namespace StepStatisticsApp
{
    public class UsersViewModel : ViewModelBase
    {
        public ObservableCollection<User> UserList { get; set; }

        public ICommand ClickUserCommand { get; set; }

        //public List<int> SelectedUser { get; set; }

        public ObservableCollection<KeyValuePair<int, int>> Data { get; set; } = new ObservableCollection<KeyValuePair<int, int>>();

        public UsersViewModel()
        {
            Data.Add(new KeyValuePair<int, int>(1, 1));
            ClickUserCommand = new RelayCommand<User>(SelectedUserDetails);
            UserList = new ObservableCollection<User>();
            var allData = Startup.UsersStepPair;
            foreach (var userData in allData)
            {
                int avSteps = User.CalculateAvarageStep(userData.Value);
                int minSteps = User.FindBestResult(userData.Value);
                int maxSteps = User.FindWorstResult(userData.Value);

                var user = new User()
                {
                    Name = userData.Key,
                    AvarageStepPerMonth = avSteps,
                    BestStepResult = minSteps,
                    WorstStepResult = maxSteps,
                    StepStatistics = userData.Value,
                };

                UserList.Add(user);
            }
        }

        private void SelectedUserDetails(User obj)
        {
            if (obj != null)
            {
                Data.Clear();
                for (int i = 1; i < obj.StepStatistics.Count; i++)
                {
                    Data.Add(new KeyValuePair<int, int>(obj.StepStatistics[i - 1], i));
                }

                this.RaisePropertyChanged(nameof(Data));
            }
        }
    }
}
