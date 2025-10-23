using BussinessObjects.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DAO
{
    public class BookingDetailDAO
    {

        public static List<BookingDetail> GetAll()
        {
            List<BookingDetail> list = new List<BookingDetail>();
            try
            {
                using var context = new FuminiHotelManagementContext(); 
                list = context.BookingDetails
                               .Include(bd => bd.BookingReservation)
                               .Include(bd => bd.Room)
                               .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy tất cả chi tiết đặt phòng: {ex.Message}");
            }
            return list;
        }

        public static void Add(BookingDetail bookingDetail)
        {
            try
            {
                
                using var context = new FuminiHotelManagementContext();
                context.BookingDetails.Add(bookingDetail);
                context.SaveChanges(); 
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi thêm chi tiết đặt phòng: {ex.Message}");
            }
        }

        public static List<BookingDetail> GetBookingDetailsByReservationID(int bookingReservationID)
        {
            List<BookingDetail> list = new List<BookingDetail>();
            try
            {
                using var context = new FuminiHotelManagementContext();
                list = context.BookingDetails
                               .Where(bd => bd.BookingReservationId == bookingReservationID)
                               .Include(bd => bd.BookingReservation)
                               .Include(bd => bd.Room) 
                               .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy chi tiết đặt phòng theo ID đặt phòng: {ex.Message}");
            }
            return list;
        }

        public static void Update(BookingDetail bookingDetail)
        {
            try
            {
                using var context = new FuminiHotelManagementContext();
                context.Entry(bookingDetail).State = EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi cập nhật chi tiết đặt phòng: {ex.Message}");
            }
        }

        public static void Delete(int bookingReservationId, int roomId) 
        {
            try
            {
                using var context = new FuminiHotelManagementContext();
                var detailToDelete = context.BookingDetails.Find(bookingReservationId, roomId);
                if (detailToDelete != null)
                {
                    context.BookingDetails.Remove(detailToDelete);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi xóa chi tiết đặt phòng: {ex.Message}");
            }
        }

        public static BookingDetail? GetById(int bookingReservationId, int roomId)
        {
            try
            {
                using var context = new FuminiHotelManagementContext();
                return context.BookingDetails.Find(bookingReservationId, roomId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy chi tiết đặt phòng bằng ID: {ex.Message}");
            }
        }
    }
}
