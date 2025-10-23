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
    public class RoomTypeService : IRoomTypeService
    {
        private readonly IRoomTypeRepository iRoomTypeRepository;

        public RoomTypeService()
        {
            iRoomTypeRepository = new RoomTypeRepository();
        }

        public void AddRoomType(RoomType roomType)
        {
            iRoomTypeRepository.AddRoomType(roomType);
        }

        public void DeleteRoomType(int roomTypeId)
        {
            iRoomTypeRepository.DeleteRoomType(roomTypeId);
        }

        public List<RoomType> GetRoomTypeList()
        {
            return iRoomTypeRepository.GetRoomTypeList();
        }

        public void UpdateRoomType(RoomType roomType)
        {
            iRoomTypeRepository.UpdateRoomType(roomType);
        }
    }
}
