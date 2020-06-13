using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using StepStatisticsApp.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace StepStatisticsApp
{
    public class UsersViewModel : ViewModelBase
    {
        public ObservableCollection<User> UserList { get; set; }

        public ICommand ClickChartCommand { get; set; }

        public ICommand ClickExportCommand { get; set; }

        public ObservableCollection<KeyValuePair<int, int>> Data { get; set; } = new ObservableCollection<KeyValuePair<int, int>>();

        public UsersViewModel()
        {
            ClickChartCommand = new RelayCommand<User>(SelectedUserDetails);
            ClickExportCommand = new RelayCommand<User>(ExportTo);

            UserList = new ObservableCollection<User>();

            var stepData = Startup.UsersStepPair;
            var rankData = Startup.UsersRankPair;
            var statusData = Startup.UsersStatusPair;

            foreach (var userData in stepData)
            {
                var rankDict = rankData[userData.Key];
                var statusDict = statusData[userData.Key];

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
                    RankStatistics = rankDict,
                    StatusStatistics = statusDict,
                };

                UserList.Add(user);
            }
        }

        private void SelectedUserDetails(User obj)
        {
            if (obj != null)
            {
                Data.Clear();
                foreach (var item in obj.StepStatistics)
                {
                    Data.Add(new KeyValuePair<int, int>(item.Value, item.Key));
                }

                this.RaisePropertyChanged(nameof(Data));
            }
        }

        private void ExportTo(User obj)
        {
            string xmlPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data.xml");

            FileStream filestream = default;
            try
            {
                filestream = File.Create(xmlPath);
            }
            catch (FileNotFoundException)
            {
            }
            catch (UnauthorizedAccessException)
            {
            }
            catch (DirectoryNotFoundException)
            {
            }

            using var stream = new StreamWriter(filestream);
            using var writer = new XMLWriter(stream);
            writer.Write(obj);
        }
    }
}
