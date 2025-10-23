using BussinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.IServices
{
    public interface IBookingDetailService
    {
        List<BookingDetail> GetBookingDetailList();
        void AddBookingDetail(BookingDetail bookingDetail);
    }
}
