using BLL.IServices;
using BussinessObjects.Models;
using DAL.IRepositories;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class BookingReservationService : IBookingReservationService
    {
        private readonly IBookingReservationRepository iBookingReservationRepository;

        public BookingReservationService()
        {
            iBookingReservationRepository = new BookingReservationRepository();
        }

        public void AddBookingReservation(BookingReservation bookingReservation)
        {
            iBookingReservationRepository.AddBookingReservation(bookingReservation);
        }

        public List<BookingReservation> GetAllBookingReservation()
        {
            return iBookingReservationRepository.GetAllBookingReservation();
        }

        public List<BookingReservation> GetBookingReservationsByCustomerID(int customerID)
        {
            return iBookingReservationRepository.GetBookingReservationsByCustomerID(customerID);
        }

        public int GetMaxBookingReservationID()
        {
            return iBookingReservationRepository.GetMaxBookingReservationID();
        }
    }
}
