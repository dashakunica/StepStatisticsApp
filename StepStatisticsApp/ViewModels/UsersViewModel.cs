using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using StepStatisticsApp.Models;
using System.Text;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Globalization;
using System.Windows;

namespace StepStatisticsApp
{

    public class UsersViewModel : ViewModelBase
    {
        string XmlPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data.xml");

        public ObservableCollection<User> UserList { get; set; }

        public String ExportedUserNames { get; set; } 
        
        public ICommand ClickChartCommand { get; set; }

        public ICommand ClickExportCommand { get; set; }

        public ObservableCollection<KeyValuePair<int, int>> Data { get; set; } = new ObservableCollection<KeyValuePair<int, int>>();

        public UsersViewModel()
        {
            if (File.Exists(XmlPath))
            {
                File.Delete(XmlPath);
            }

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
                int maxSteps = User.FindBestResult(userData.Value);
                int minSteps = User.FindWorstResult(userData.Value);
                bool isStable = User.IsStabStepUser(avSteps, maxSteps);

                var user = new User()
                {
                    Name = userData.Key,
                    AvarageStepPerMonth = avSteps,
                    BestStepResult = maxSteps,
                    WorstStepResult = minSteps,
                    StepStatistics = userData.Value,
                    RankStatistics = rankDict,
                    StatusStatistics = statusDict,
                    IsUnstableUser = isStable,
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
                    Data.Add(new KeyValuePair<int, int>(item.Key, item.Value));
                }

                this.RaisePropertyChanged(nameof(Data));
            }
        }

        private void ExportTo(User obj)
        {
            FileStream filestream = default;
            try
            {
                filestream = File.Open(XmlPath, FileMode.OpenOrCreate);
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
            using var writer = new XMLUserWriter(stream);
            writer.Write(obj);
            ExportedUserNames = obj.Name.ToString(CultureInfo.CurrentCulture);
            this.RaisePropertyChanged("ExportedUserNames");
            MessageBox.Show($"User {obj.Name} successfuly export into {XmlPath} path.");
        }
    }
}
