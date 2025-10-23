using BussinessObjects.Models;
using DAL.DAO;
using DAL.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class BookingReservationRepository : IBookingReservationRepository
    {
        public void AddBookingReservation(BookingReservation bookingReservation)
        {
            BookingReservationDAO.Add(bookingReservation);
        }

        public List<BookingReservation> GetAllBookingReservation()
        {
            return BookingReservationDAO.GetAll();
        }

        public List<BookingReservation> GetBookingReservationsByCustomerID(int customerID)
        {
            return BookingReservationDAO.GetBookingReservationsByCustomerID(customerID);
        }

        public int GetMaxBookingReservationID()
        {
            return BookingReservationDAO.GetMaxBookingReservationID();
        }

    }
}
