using BussinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.IServices
{
    public interface IBookingReservationService
    {
        List<BookingReservation> GetBookingReservationsByCustomerID(int customerID);
        List<BookingReservation> GetAllBookingReservation();
        void AddBookingReservation(BookingReservation bookingReservation);

        int GetMaxBookingReservationID();

    }
}
