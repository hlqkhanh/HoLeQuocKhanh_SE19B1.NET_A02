using BussinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.IRepositories
{
    public interface IRoomTypeRepository
    {
        List<RoomType> GetRoomTypeList();
        void AddRoomType(RoomType roomType);
        void DeleteRoomType(int roomTypeID);
        void UpdateRoomType(RoomType roomType);

    }
}
