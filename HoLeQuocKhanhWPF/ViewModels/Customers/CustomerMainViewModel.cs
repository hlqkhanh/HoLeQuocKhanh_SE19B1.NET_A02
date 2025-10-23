using BussinessObjects.Models;
using HoLeQuocKhanhWPF.ViewModels.Base;
using HoLeQuocKhanhWPF.Views; 
using System.Windows;       
using System.Windows.Input;

namespace HoLeQuocKhanhWPF.ViewModels.Customers
{
    public class CustomerMainViewModel : ViewModelBase
    {
        private readonly Customer _currentCustomer;
        private ViewModelBase _currentViewModel;
        private string _welcomeMessage;

        public ViewModelBase CurrentViewModel
        {
            get => _currentViewModel;
            set { _currentViewModel = value; OnPropertyChanged(); }
        }

        public string WelcomeMessage
        {
            get => _welcomeMessage;
            set { _welcomeMessage = value; OnPropertyChanged(); }
        }

        public ICommand ShowProfileCommand { get; }
        public ICommand ShowBookingHistoryCommand { get; }
        public ICommand BookRoomCommand { get; }
        public ICommand LogoutCommand { get; } 

        public CustomerMainViewModel(Customer customer)
        {
            _currentCustomer = customer;
            WelcomeMessage = $"Welcome, {_currentCustomer.CustomerFullName}!";

            ShowProfileCommand = new RelayCommand<object>(p => CurrentViewModel = new CustomerProfileViewModel(_currentCustomer));
            ShowBookingHistoryCommand = new RelayCommand<object>(p => CurrentViewModel = new BookingHistoryViewModel(_currentCustomer));
            BookRoomCommand = new RelayCommand<object>(p => CurrentViewModel = new BookRoomViewModel(_currentCustomer));

            LogoutCommand = new RelayCommand<Window>(Logout);

            CurrentViewModel = new CustomerProfileViewModel(_currentCustomer);
        }

        private void Logout(Window window)
        {
            if (window != null)
            {
                var loginView = new LoginView();
                Application.Current.MainWindow = loginView;
                loginView.Show();
                window.Close();
            }
        }
    }
}