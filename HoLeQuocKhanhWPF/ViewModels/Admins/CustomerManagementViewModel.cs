using BLL.IServices;
using BLL.Services;
using BussinessObjects.Models;
using HoLeQuocKhanhWPF.ViewModels.Base;
using HoLeQuocKhanhWPF.Views.Dialogs;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace HoLeQuocKhanhWPF.ViewModels.Admins
{
    public class CustomerManagementViewModel : ViewModelBase
    {
        private readonly ICustomerService _customerService;
        private ObservableCollection<Customer> _customers;
        private Customer _selectedCustomer;
        private string _searchText;

        public ObservableCollection<Customer> Customers
        {
            get { return _customers; }
            set
            {
                _customers = value;
                OnPropertyChanged();
            }
        }

        public Customer SelectedCustomer
        {
            get { return _selectedCustomer; }
            set
            {
                _selectedCustomer = value;
                OnPropertyChanged();
            }
        }

        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                OnPropertyChanged();
            }
        }

        public ICommand AddCustomerCommand { get; }
        public ICommand EditCustomerCommand { get; }
        public ICommand DeleteCustomerCommand { get; }
        public ICommand SearchCommand { get; }

        public CustomerManagementViewModel()
        {
            _customerService = new CustomerService();
            LoadCustomers();

            AddCustomerCommand = new RelayCommand<object>(p => AddCustomer());
            
            EditCustomerCommand = new RelayCommand<object>(p => EditCustomer());
            DeleteCustomerCommand = new RelayCommand<object>(p => DeleteCustomer());
            SearchCommand = new RelayCommand<object>(p => Search());
        }

        private void LoadCustomers()
        {
            Customers = new ObservableCollection<Customer>(_customerService.GetAllCustomer());
        }

        private void AddCustomer()
        {
            var dialog = new CustomerDialog();
            if (dialog.ShowDialog() == true)
            {
                LoadCustomers();
            }
        }

      
        private void EditCustomer()
        {
            
            if (SelectedCustomer != null)
            {
                var dialog = new CustomerDialog(SelectedCustomer);
                if (dialog.ShowDialog() == true)
                {
                    LoadCustomers();
                }
            }
            else
            {
                MessageBox.Show("Please select a customer to edit.", "Information");
            }
        }

        private void DeleteCustomer()
        {
            if (SelectedCustomer != null)
            {
                if (MessageBox.Show("Are you sure you want to delete this customer?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    _customerService.DeleteCustomer(SelectedCustomer.CustomerId);
                    LoadCustomers();
                }
            }
            else
            {
                MessageBox.Show("Please select a customer to delete.", "Information");
            }
        }

        private void Search()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                LoadCustomers();
            }
            else
            {
                Customers = new ObservableCollection<Customer>(_customerService.GetAllCustomer().Where(c => c.CustomerFullName.Contains(SearchText, System.StringComparison.OrdinalIgnoreCase)));
            }
        }
    }
}