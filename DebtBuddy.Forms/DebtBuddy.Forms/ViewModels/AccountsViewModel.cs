﻿using DebtBuddy.Forms.Models;
using DebtBuddy.Forms.Services;
using DebtBuddy.Forms.Interfaces.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Xamarin.Forms;
using DebtBuddy.Forms.Extensions;

[assembly: Dependency(typeof(NavigationService))]
[assembly: Dependency(typeof(AccountDataService))]
namespace DebtBuddy.Forms.ViewModels
{
    public class AccountsViewModel : INotifyPropertyChanged
    {
        private INavigationService _navigationService;
        private IAccountDataService _accountDataService;

        ObservableCollection<Account> _accounts;

        public event PropertyChangedEventHandler PropertyChanged;

        public AccountsViewModel()
        {
            _navigationService = DependencyService.Get<INavigationService>();
            _accountDataService = DependencyService.Get<IAccountDataService>();

            ShowCreateAccountViewModel = new Command(async() => await _navigationService.NavigateToCreateAccount());
        }

        public Command ShowCreateAccountViewModel { get; }

        public ObservableCollection<Account> Accounts
        {
            get => _accounts;
            set
            {
                _accounts = value;
                OnPropertyChanged("Accounts");
            }
        }

        public async void LoadAccountsFromDatabase()
        {
            Accounts = (await _accountDataService.GetAllAccounts()).ToObservableCollection();
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
