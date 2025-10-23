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
    public class BookingDetailService : IBookingDetailService
    {
        private readonly IBookingDetailRepository iBookingDetailRepository;

        public BookingDetailService()
        {
            iBookingDetailRepository = new BookingDetailRepository();
        }

        public void AddBookingDetail(BookingDetail bookingDetail)
        {
            iBookingDetailRepository.AddBookingDetail(bookingDetail);
        }

        public List<BookingDetail> GetBookingDetailList()
        {
            return iBookingDetailRepository.GetAllBookingDetail();
        }
    }
}
