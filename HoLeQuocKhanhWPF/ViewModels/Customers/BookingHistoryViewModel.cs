using BLL.IServices;
using BLL.Services;
using BussinessObjects.Models;
using HoLeQuocKhanhWPF.ViewModels.Base;
using HoLeQuocKhanhWPF.Views.Dialogs; 
using System.Collections.ObjectModel;
using System.Windows.Input; 

namespace HoLeQuocKhanhWPF.ViewModels.Customers
{
    public class BookingHistoryViewModel : ViewModelBase
    {
        private readonly IBookingReservationService _bookingReservationService;
        private readonly Customer _currentCustomer;
        private ObservableCollection<BookingReservation> _bookingHistory;

        public ObservableCollection<BookingReservation> BookingHistory
        {
            get => _bookingHistory;
            set { _bookingHistory = value; OnPropertyChanged(); }
        }

        public ICommand ViewDetailCommand { get; }

        public BookingHistoryViewModel(Customer customer)
        {
            _bookingReservationService = new BookingReservationService();
            _currentCustomer = customer;
            LoadBookingHistory();

            
            ViewDetailCommand = new RelayCommand<BookingReservation>(ViewDetail);
        }

        private void LoadBookingHistory()
        {
            var history = _bookingReservationService.GetBookingReservationsByCustomerID(_currentCustomer.CustomerId);
            if (history != null)
            {
                BookingHistory = new ObservableCollection<BookingReservation>(history);
            }
            else
            {
                BookingHistory = new ObservableCollection<BookingReservation>();
            }
        }

        private void ViewDetail(BookingReservation reservation)
        {
            if (reservation != null)
            {
                var detailDialog = new BookingDetailDialog(reservation);
                detailDialog.ShowDialog();
            }
        }
    }
}