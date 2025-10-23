using BLL.IServices;
using BLL.Services;
using BussinessObjects.Models;
using HoLeQuocKhanhWPF.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HoLeQuocKhanhWPF.ViewModels.Customers
{
    public class BookRoomViewModel : ViewModelBase
    {
        private readonly IRoomInformationService _roomService;
        private readonly IBookingDetailService _bookingDetailService;
        private readonly IBookingReservationService _bookingReservationService;
        private readonly Customer _currentCustomer;


        private DateTime _startDate = DateTime.Now.Date;
        private DateTime _endDate = DateTime.Now.AddDays(1).Date;
        private ObservableCollection<RoomInformation> _availableRooms;
        private ObservableCollection<RoomInformation> _selectedRoomsForBooking = new ObservableCollection<RoomInformation>();
        private decimal _totalCartPrice;

        public DateTime StartDate
        {
            get => _startDate;
            set { _startDate = value; OnPropertyChanged(); RecalculateCartPrice(); } 
        }

        public DateTime EndDate
        {
            get => _endDate;
            set { _endDate = value; OnPropertyChanged(); RecalculateCartPrice(); } 
        }

        public ObservableCollection<RoomInformation> AvailableRooms
        {
            get => _availableRooms;
            set { _availableRooms = value; OnPropertyChanged(); }
        }

        public ObservableCollection<RoomInformation> SelectedRoomsForBooking
        {
            get => _selectedRoomsForBooking;
            set { _selectedRoomsForBooking = value; OnPropertyChanged(); RecalculateCartPrice(); }
        }

        public decimal TotalCartPrice
        {
            get => _totalCartPrice;
            set { _totalCartPrice = value; OnPropertyChanged(); }
        }

        public ICommand SearchCommand { get; }
        public ICommand BookCommand { get; }
        public ICommand AddToCartCommand { get; }
        public ICommand RemoveFromCartCommand { get; }

        public BookRoomViewModel(Customer customer)
        {
            _currentCustomer = customer;
            _roomService = new RoomInformationService();
            _bookingDetailService = new BookingDetailService();
            _bookingReservationService = new BookingReservationService();

            SearchCommand = new RelayCommand<object>(p => SearchAvailableRooms());
            BookCommand = new RelayCommand<object>(p => BookRoomsInCart(), p => CanBookRooms()); 

            AddToCartCommand = new RelayCommand<RoomInformation>(AddToCart);
            RemoveFromCartCommand = new RelayCommand<RoomInformation>(RemoveFromCart);

            SearchAvailableRooms();
        }

        private void SearchAvailableRooms()
        {
            if (StartDate >= EndDate)
            {
                MessageBox.Show("End date must be after start date.", "Invalid Date", MessageBoxButton.OK, MessageBoxImage.Warning);
                AvailableRooms = new ObservableCollection<RoomInformation>(); 
                return;
            }

            var allRooms = _roomService.GetRoomInformationList();
            if (allRooms == null)
            {
                AvailableRooms = new ObservableCollection<RoomInformation>();
                return;
            }

            var allBookingDetails = _bookingDetailService.GetBookingDetailList() ?? new List<BookingDetail>();

            var unavailableRoomIds = allBookingDetails
                .Where(detail => detail.StartDate < DateOnly.FromDateTime(EndDate) &&
                 detail.EndDate > DateOnly.FromDateTime(StartDate))
                .Select(detail => detail.RoomId)
                .Distinct()
                .ToList();

            var availableRoomsList = allRooms
                .Where(room => !unavailableRoomIds.Contains(room.RoomId))
                .ToList();

            AvailableRooms = new ObservableCollection<RoomInformation>(availableRoomsList);

            var roomsToRemoveFromCart = SelectedRoomsForBooking.Where(cartRoom => !availableRoomsList.Any(availRoom => availRoom.RoomId == cartRoom.RoomId)).ToList();
            foreach (var roomToRemove in roomsToRemoveFromCart)
            {
                SelectedRoomsForBooking.Remove(roomToRemove);
            }
            RecalculateCartPrice(); 
        }

        private void AddToCart(RoomInformation roomToAdd)
        {
            if (roomToAdd != null && !SelectedRoomsForBooking.Any(r => r.RoomId == roomToAdd.RoomId))
            {
                SelectedRoomsForBooking.Add(roomToAdd);
                RecalculateCartPrice();
            }
        }

        private void RemoveFromCart(RoomInformation roomToRemove)
        {
            if (roomToRemove != null)
            {
                SelectedRoomsForBooking.Remove(roomToRemove);
                RecalculateCartPrice();
            }
        }

        private void RecalculateCartPrice()
        {
            if (StartDate >= EndDate)
            {
                TotalCartPrice = 0;
                return;
            }
            int numberOfDays = (EndDate - StartDate).Days;
            if (numberOfDays <= 0) numberOfDays = 1; 

            TotalCartPrice = SelectedRoomsForBooking.Sum(room => numberOfDays * room.RoomPricePerDay.GetValueOrDefault());
        }

        private bool CanBookRooms()
        {
            return SelectedRoomsForBooking.Any() && StartDate < EndDate;
        }


        private void BookRoomsInCart()
        {
            if (!SelectedRoomsForBooking.Any())
            {
                MessageBox.Show("Your booking cart is empty.", "Empty Cart", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (StartDate >= EndDate)
            {
                MessageBox.Show("End date must be after start date.", "Invalid Date", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }


            string roomNumbers = string.Join(", ", SelectedRoomsForBooking.Select(r => r.RoomNumber));
            RecalculateCartPrice(); 

            var result = MessageBox.Show($"Confirm booking for the following rooms from {StartDate:dd/MM/yyyy} to {EndDate:dd/MM/yyyy}?\n\nRooms: {roomNumbers}\nTotal Price: {TotalCartPrice:C}",
                                          "Confirm Booking", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    int numberOfDays = (EndDate - StartDate).Days;

                    int newReservationId = _bookingReservationService.GetMaxBookingReservationID() + 1;
                    var newReservation = new BookingReservation
                    {
                        BookingReservationId = newReservationId,
                        BookingDate = DateOnly.FromDateTime(DateTime.Now),
                        TotalPrice = TotalCartPrice,
                        CustomerId = _currentCustomer.CustomerId,
                        BookingStatus = 1
                    };
                    _bookingReservationService.AddBookingReservation(newReservation);



                    var newBookingDetails = new List<BookingDetail>();
                    foreach (var room in SelectedRoomsForBooking)
                    {
                        var newDetail = new BookingDetail
                        {
                            BookingReservationId = newReservationId,
                            RoomId = room.RoomId,
                            StartDate = DateOnly.FromDateTime(StartDate), 
                            EndDate = DateOnly.FromDateTime(EndDate),
                            ActualPrice = room.RoomPricePerDay
                        };
                        newBookingDetails.Add(newDetail);
                    }

                    foreach (var bd in newBookingDetails)
                    {
                        _bookingDetailService.AddBookingDetail(bd);
                    }

                    MessageBox.Show("Rooms booked successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                    SelectedRoomsForBooking.Clear();
                    SearchAvailableRooms();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred during booking: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}