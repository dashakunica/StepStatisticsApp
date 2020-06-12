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

        public ICommand ClickUserNameCommand { get; set; }

        public string SelectedUserName { get; set; }

        public UsersViewModel()
        {
            ClickUserNameCommand = new RelayCommand<User>(SelectedUserDetails);
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
                };

                UserList.Add(user);
            }
        }

        private void SelectedUserDetails(User obj)
        {
            if (obj != null)
            {
                this.SelectedUserName = obj.Name;
                // better to go for full property instead of calling property change here 
                this.RaisePropertyChanged("SelectedUserName");
            }
        }
    }
}
