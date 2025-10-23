using BussinessObjects.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL.DAO 
{
    public class BookingReservationDAO
    {

        public static List<BookingReservation> GetAll()
        {
            List<BookingReservation> list = new List<BookingReservation>();
            try
            {
                using var context = new FuminiHotelManagementContext(); 
                list = context.BookingReservations
                               .Include(br => br.Customer) 
                               .Include(br => br.BookingDetails) 
                                   .ThenInclude(bd => bd.Room) 
                               .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy tất cả đặt phòng: {ex.Message}");
            }
            return list;
        }

        public static List<BookingReservation> GetBookingReservationsByCustomerID(int customerID)
        {
            List<BookingReservation> list = new List<BookingReservation>();
            try
            {
                using var context = new FuminiHotelManagementContext(); 
                list = context.BookingReservations
                               .Where(br => br.CustomerId == customerID) 
                               .Include(br => br.Customer)
                               .Include(br => br.BookingDetails)
                                   .ThenInclude(bd => bd.Room) 
                               .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy đặt phòng theo ID khách hàng: {ex.Message}");
            }
            return list;
        }

        public static BookingReservation? GetBookingReservation(int bookingReservationID)
        {
            BookingReservation? reservation = null;
            try
            {
                using var context = new FuminiHotelManagementContext();
                reservation = context.BookingReservations
                                     .Include(br => br.Customer)
                                     .Include(br => br.BookingDetails)
                                         .ThenInclude(bd => bd.Room) 
                                     .FirstOrDefault(br => br.BookingReservationId == bookingReservationID); 
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy đặt phòng theo ID: {ex.Message}");
            }
            return reservation;
        }

        public static void Add(BookingReservation reservation)
        {
            try
            {
                using var context = new FuminiHotelManagementContext();
                context.BookingReservations.Add(reservation);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi thêm đặt phòng: {ex.Message}");
            }
        }

        public static void Update(BookingReservation reservation)
        {
            try
            {
                using var context = new FuminiHotelManagementContext();
                context.Entry(reservation).State = EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi cập nhật đặt phòng: {ex.Message}");
            }
        }

        public static void Delete(int bookingReservationId)
        {
            try
            {
                using var context = new FuminiHotelManagementContext();
                var reservationToDelete = context.BookingReservations
                                                 .Include(br => br.BookingDetails)
                                                 .FirstOrDefault(br => br.BookingReservationId == bookingReservationId);

                if (reservationToDelete != null)
                {

                    context.BookingReservations.Remove(reservationToDelete);
                    context.SaveChanges();
                }
            }
            catch (DbUpdateException ex)
            {
                throw new Exception($"Lỗi khi xóa đặt phòng (có thể do ràng buộc khóa ngoại): {ex.InnerException?.Message ?? ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi xóa đặt phòng: {ex.Message}");
            }
        }

        public static int GetMaxBookingReservationID()
        {
            var bookingReservations = GetAll();
            if (bookingReservations.Count == 0)
                return 0;

            return bookingReservations.Max(b => b.BookingReservationId);
        }
    }
}