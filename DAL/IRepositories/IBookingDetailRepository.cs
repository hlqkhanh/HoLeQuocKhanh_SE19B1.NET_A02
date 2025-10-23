using BussinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.IRepositories
{
    public interface IBookingDetailRepository
    {
        List<BookingDetail> GetAllBookingDetail();
        void AddBookingDetail(BookingDetail bookingDetail);
    }
}
