using BussinessObjects.Models;
using HoLeQuocKhanhWPF.ViewModels.Dialogs;
using System.Windows;

namespace HoLeQuocKhanhWPF.Views.Dialogs
{
    public partial class ChangePasswordDialog : Window
    {
        public ChangePasswordDialog(Customer customer)
        {
            InitializeComponent();
            DataContext = new ChangePasswordDialogViewModel(customer, this);
        }
    }
}