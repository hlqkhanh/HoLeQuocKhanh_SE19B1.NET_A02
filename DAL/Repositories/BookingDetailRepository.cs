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
    public class BookingDetailRepository : IBookingDetailRepository
    {
        public void AddBookingDetail(BookingDetail bookingDetail)
        {
            BookingDetailDAO.Add(bookingDetail);
        }

        public List<BookingDetail> GetAllBookingDetail()
        {
            return BookingDetailDAO.GetAll();
        }
    }
}
