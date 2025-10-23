using BussinessObjects.Models;
using HoLeQuocKhanhWPF.ViewModels.Dialogs;
using System.Windows;

namespace HoLeQuocKhanhWPF.Views.Dialogs
{
    public partial class RoomDialog : Window
    {
        public RoomDialog(RoomInformation room = null)
        {
            InitializeComponent();
            DataContext = new RoomDialogViewModel(room);
        }
    }
}