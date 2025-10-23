using BussinessObjects.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL.DAO 
{
    public class RoomInformationDAO
    {

        public static List<RoomInformation> GetAll()
        {
            List<RoomInformation> list = new List<RoomInformation>();
            try
            {
                using var context = new FuminiHotelManagementContext(); 
                list = context.RoomInformations
                               .Include(r => r.RoomType) 
                               .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy tất cả thông tin phòng: {ex.Message}");
            }
            return list;
        }

        public static void Add(RoomInformation room)
        {
            try
            {
                using var context = new FuminiHotelManagementContext(); 
                if (context.RoomInformations.Any(r => r.RoomNumber == room.RoomNumber))
                {
                    throw new Exception($"Số phòng '{room.RoomNumber}' đã tồn tại.");
                }
                context.RoomInformations.Add(room);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi thêm phòng: {ex.Message}");
            }
        }

        public static void Update(RoomInformation room)
        {
            try
            {
                using var context = new FuminiHotelManagementContext(); 
                if (context.RoomInformations.Any(r => r.RoomNumber == room.RoomNumber && r.RoomId != room.RoomId))
                {
                    throw new Exception($"Số phòng '{room.RoomNumber}' đã được sử dụng bởi phòng khác.");
                }
                context.Entry(room).State = EntityState.Modified;
                context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new Exception($"Lỗi tương tranh khi cập nhật phòng ID {room.RoomId}. Dữ liệu có thể đã bị thay đổi. {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi cập nhật phòng ID {room.RoomId}: {ex.Message}");
            }
        }

        public static void Delete(int roomId) 
        {
            try
            {
                using var context = new FuminiHotelManagementContext(); 
                var roomToDelete = context.RoomInformations.Find(roomId);
                if (roomToDelete != null)
                {
                    bool isInBookingDetails = context.BookingDetails.Any(bd => bd.RoomId == roomId);
                    if (isInBookingDetails)
                    {
                        throw new Exception($"Không thể xóa phòng ID {roomId} vì đã có trong lịch sử đặt phòng.");
                    }

                    context.RoomInformations.Remove(roomToDelete);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception($"Không tìm thấy phòng với ID {roomId} để xóa.");
                }
            }
            catch (DbUpdateException ex) 
            {
                throw new Exception($"Lỗi khi xóa phòng ID {roomId} (có thể do ràng buộc khóa ngoại): {ex.InnerException?.Message ?? ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi xóa phòng ID {roomId}: {ex.Message}");
            }
        }

        public static RoomInformation? GetRoomInformation(int roomID)
        {
            RoomInformation? room = null;
            try
            {
                using var context = new FuminiHotelManagementContext(); 
                room = context.RoomInformations
                              .Include(r => r.RoomType)
                              .FirstOrDefault(r => r.RoomId == roomID);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy thông tin phòng theo ID {roomID}: {ex.Message}");
            }
            return room;
        }

        public static List<RoomInformation> SearchRoomByNumber(string roomNumber)
        {
            List<RoomInformation> list = new List<RoomInformation>();
            try
            {
                using var context = new FuminiHotelManagementContext();
                list = context.RoomInformations
                               .Include(r => r.RoomType)
                               .Where(r => r.RoomNumber.Contains(roomNumber))
                               .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi tìm phòng theo số '{roomNumber}': {ex.Message}");
            }
            return list;
        }

        public static int GetMaxRoomInformationID()
        {
            var roomInformations = GetAll();
            if (roomInformations.Count == 0)
                return 0;

            return roomInformations.Max(b => b.RoomId);
        }
    }
}