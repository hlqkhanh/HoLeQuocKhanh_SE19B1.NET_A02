using BLL.IServices;
using BLL.Services;
using HoLeQuocKhanhWPF.ViewModels.Base;
using HoLeQuocKhanhWPF.Views.Admins;
using HoLeQuocKhanhWPF.Views.Customers;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HoLeQuocKhanhWPF.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly ICustomerService _customerService;
        private string _email;
        private string _errorMessage;
        private readonly IConfiguration _configuration;

        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                _errorMessage = value;
                OnPropertyChanged();
            }
        }

        public ICommand LoginCommand { get; }

        public LoginViewModel()
        {
            _customerService = new CustomerService();
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            _configuration = builder.Build();

            LoginCommand = new RelayCommand<PasswordBox>(Login);
        }

        private void Login(PasswordBox passwordBox)
        {
            string password = passwordBox.Password;
            string adminEmail = _configuration["AdminAccount:Email"];
            string adminPassword = _configuration["AdminAccount:Password"];

            if (Email == adminEmail && password == adminPassword)
            {
                var adminWindow = new AdminMainWindow();
                adminWindow.Show();
                Application.Current.MainWindow.Close();
            }
            else
            {
                var customer = _customerService.CheckLogin(Email, password);
                if (customer != null)
                {
                    var customerWindow = new CustomerMainWindow(customer);
                    customerWindow.Show();
                    Application.Current.MainWindow.Close();
                }
                else
                {
                    ErrorMessage = "Invalid email or password.";
                }
            }
        }
    }
}
