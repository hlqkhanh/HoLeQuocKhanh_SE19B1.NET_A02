using BLL.IServices;
using BLL.Services;
using BussinessObjects.Models;
using HoLeQuocKhanhWPF.ViewModels.Base;
using HoLeQuocKhanhWPF.Views.Dialogs;
using System.Windows;
using System.Windows.Input;

namespace HoLeQuocKhanhWPF.ViewModels.Customers
{
    public class CustomerProfileViewModel : ViewModelBase
    {
        private readonly ICustomerService _customerService;
        private BussinessObjects.Models.Customer _currentCustomer;

        public BussinessObjects.Models.Customer CurrentCustomer
        {
            get => _currentCustomer;
            set
            {
                _currentCustomer = value;
                OnPropertyChanged();
            }
        }

        public ICommand SaveChangesCommand { get; }
        public ICommand ChangePasswordCommand { get; } 

        public CustomerProfileViewModel(Customer customer)
        {
            _customerService = new CustomerService();
            CurrentCustomer = customer;

            SaveChangesCommand = new RelayCommand<object>(p => SaveChanges());
            ChangePasswordCommand = new RelayCommand<object>(p => ChangePassword()); 
        }

        private void SaveChanges()
        {
            try
            {
                _customerService.UpdateCustomer(CurrentCustomer);
                MessageBox.Show("Profile updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Error updating profile: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ChangePassword()
        {
            var changePasswordDialog = new ChangePasswordDialog(CurrentCustomer);
            changePasswordDialog.ShowDialog();
        }

        public DateTime? CustomerBirthdayProxy
        {
            get
            {
                if (CurrentCustomer?.CustomerBirthday.HasValue ?? false)
                {
                    return CurrentCustomer.CustomerBirthday.Value.ToDateTime(TimeOnly.MinValue);
                }
                return null;
            }
            set
            {
                if (CurrentCustomer != null)
                {
                    if (value.HasValue)
                    {
                        CurrentCustomer.CustomerBirthday = DateOnly.FromDateTime(value.Value);
                    }
                    else
                    {
                        CurrentCustomer.CustomerBirthday = null;
                    }
                    OnPropertyChanged(nameof(CustomerBirthdayProxy));
                }
            }
        }
    }
}