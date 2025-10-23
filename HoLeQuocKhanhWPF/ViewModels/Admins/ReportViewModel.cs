using BLL.IServices;
using BLL.Services;
using BussinessObjects.Models;
using HoLeQuocKhanhWPF.ViewModels.Base;
using HoLeQuocKhanhWPF.ViewModels.Dialogs;
using HoLeQuocKhanhWPF.Views.Dialogs;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace HoLeQuocKhanhWPF.ViewModels.Admins
{
    public class ReportViewModel : ViewModelBase
    {
        private readonly IBookingReservationService _bookingService;
        private DateTime _startDate = DateTime.Now.AddMonths(-1);
        private DateTime _endDate = DateTime.Now;
        private ObservableCollection<BookingReservation> _reportData;

        public DateTime StartDate
        {
            get => _startDate;
            set
            {
                _startDate = value;
                OnPropertyChanged();
            }
        }

        public DateTime EndDate
        {
            get => _endDate;
            set
            {
                _endDate = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<BookingReservation> ReportData
        {
            get => _reportData;
            set
            {
                _reportData = value;
                OnPropertyChanged();
            }
        }

        public ICommand GenerateReportCommand { get; }
        // THÊM COMMAND MỚI
        public ICommand ViewDetailsCommand { get; }

        public ReportViewModel()
        {
            _bookingService = new BookingReservationService();
            GenerateReportCommand = new RelayCommand<object>(p => GenerateReport());
            ViewDetailsCommand = new RelayCommand<BookingReservation>(ViewDetails);
            GenerateReport(); 
        }

        private void GenerateReport()
        {
            if (StartDate > EndDate)
            {
                MessageBox.Show("Start date cannot be after end date.", "Invalid Date Range", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var allBookings = _bookingService.GetAllBookingReservation();
            var filteredBookings = allBookings
                .Where(b => b.BookingDate.HasValue &&
                        b.BookingDate.Value.ToDateTime(TimeOnly.MinValue) >= StartDate.Date &&
                        b.BookingDate.Value.ToDateTime(TimeOnly.MinValue) <= EndDate.Date)
                .OrderByDescending(b => b.BookingDate)
                .ToList();

            ReportData = new ObservableCollection<BookingReservation>(filteredBookings);
        }

        private void ViewDetails(BookingReservation selectedBooking)
        {
            if (selectedBooking == null || selectedBooking.BookingDetails == null || !selectedBooking.BookingDetails.Any())
            {
                MessageBox.Show("No details available for this booking.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var detailViewModel = new BookingDetailDialogViewModel(selectedBooking);

            var detailDialog = new BookingDetailDialog(selectedBooking) 
            {
                DataContext = detailViewModel
            };

            detailDialog.ShowDialog();
        }
    }
}