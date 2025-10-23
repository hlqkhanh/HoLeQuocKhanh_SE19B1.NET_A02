using BussinessObjects.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL.DAO 
{
    public class RoomTypeDAO
    {

        public static List<RoomType> GetAll() 
        {
            List<RoomType> list = new List<RoomType>();
            try
            {
                using var context = new FuminiHotelManagementContext(); 
                list = context.RoomTypes.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy tất cả loại phòng: {ex.Message}");
            }
            return list;
        }

        public static void Add(RoomType roomType)
        {
            try
            {
                using var context = new FuminiHotelManagementContext(); 
                if (context.RoomTypes.Any(rt => rt.RoomTypeName == roomType.RoomTypeName))
                {
                    throw new Exception($"Tên loại phòng '{roomType.RoomTypeName}' đã tồn tại.");
                }
                context.RoomTypes.Add(roomType);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi thêm loại phòng: {ex.Message}");
            }
        }

        public static void Update(RoomType roomType)
        {
            try
            {
                using var context = new FuminiHotelManagementContext(); 
                if (context.RoomTypes.Any(rt => rt.RoomTypeName == roomType.RoomTypeName && rt.RoomTypeId != roomType.RoomTypeId)) 
                {
                    throw new Exception($"Tên loại phòng '{roomType.RoomTypeName}' đã được sử dụng bởi loại phòng khác.");
                }
                context.Entry(roomType).State = EntityState.Modified;
                context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new Exception($"Lỗi tương tranh khi cập nhật loại phòng ID {roomType.RoomTypeId}. Dữ liệu có thể đã bị thay đổi. {ex.Message}"); // Sửa RoomTypeId nếu tên property khác
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi cập nhật loại phòng ID {roomType.RoomTypeId}: {ex.Message}"); // Sửa RoomTypeId nếu tên property khác
            }
        }

        public static void Delete(int roomTypeId)
        {
            try
            {
                using var context = new FuminiHotelManagementContext(); 
                var roomTypeToDelete = context.RoomTypes.Find(roomTypeId);
                if (roomTypeToDelete != null)
                {
                    bool isUsed = context.RoomInformations.Any(ri => ri.RoomTypeId == roomTypeId); 
                    if (isUsed)
                    {
                        throw new Exception($"Không thể xóa loại phòng ID {roomTypeId} vì đang được sử dụng bởi các phòng.");
                    }

                    context.RoomTypes.Remove(roomTypeToDelete);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception($"Không tìm thấy loại phòng với ID {roomTypeId} để xóa.");
                }
            }
            catch (DbUpdateException ex) 
            {
                throw new Exception($"Lỗi khi xóa loại phòng ID {roomTypeId} (có thể do ràng buộc khóa ngoại): {ex.InnerException?.Message ?? ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi xóa loại phòng ID {roomTypeId}: {ex.Message}");
            }
        }

        public static RoomType? GetById(int roomTypeId)
        {
            RoomType? roomType = null;
            try
            {
                using var context = new FuminiHotelManagementContext();
                roomType = context.RoomTypes.Find(roomTypeId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy loại phòng theo ID {roomTypeId}: {ex.Message}");
            }
            return roomType;
        }
    }
}