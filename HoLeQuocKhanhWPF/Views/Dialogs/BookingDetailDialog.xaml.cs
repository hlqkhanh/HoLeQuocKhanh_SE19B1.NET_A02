using BussinessObjects.Models;
using HoLeQuocKhanhWPF.ViewModels.Dialogs;
using System.Windows;
using System.Windows.Input;

namespace HoLeQuocKhanhWPF.Views.Dialogs
{
    public partial class BookingDetailDialog : Window
    {
        public BookingDetailDialog(BookingReservation reservation)
        {
            InitializeComponent();
            DataContext = new BookingDetailDialogViewModel(reservation);
        }

       
    }
}