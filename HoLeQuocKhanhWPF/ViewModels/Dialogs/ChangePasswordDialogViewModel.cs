using BLL.IServices;
using BLL.Services;
using BussinessObjects.Models;
using HoLeQuocKhanhWPF.ViewModels.Base;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HoLeQuocKhanhWPF.ViewModels.Dialogs
{
    public class ChangePasswordDialogViewModel : ViewModelBase
    {
        private readonly ICustomerService _customerService;
        private readonly Customer _customer;
        private readonly Window _window;
        private string _errorMessage;

        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged();
            }
        }

        public ICommand SavePasswordCommand { get; }

        public ChangePasswordDialogViewModel(Customer customer, Window window)
        {
            _customerService = new CustomerService();
            _customer = customer;
            _window = window;
            SavePasswordCommand = new RelayCommand<object[]>(SavePassword);
        }

        private void SavePassword(object[] parameters)
        {
            if (parameters == null || parameters.Length != 3) return;

            var currentPasswordBox = parameters[0] as PasswordBox;
            var newPasswordBox = parameters[1] as PasswordBox;
            var confirmPasswordBox = parameters[2] as PasswordBox;

            string currentPassword = currentPasswordBox.Password;
            string newPassword = newPasswordBox.Password;
            string confirmPassword = confirmPasswordBox.Password;

            if (currentPassword != _customer.Password)
            {
                ErrorMessage = "Current password is not correct.";
                return;
            }

            if (string.IsNullOrWhiteSpace(newPassword))
            {
                ErrorMessage = "New password cannot be empty.";
                return;
            }

            if (newPassword != confirmPassword)
            {
                ErrorMessage = "New password and confirmation password do not match.";
                return;
            }

            try
            {
                _customer.Password = newPassword;
                _customerService.UpdateCustomer(_customer);
                MessageBox.Show("Password changed successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                _window.Close();
            }
            catch (System.Exception ex)
            {
                ErrorMessage = $"An error occurred: {ex.Message}";
            }
        }
    }
}