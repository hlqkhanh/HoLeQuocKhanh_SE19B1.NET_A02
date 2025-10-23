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
    public class RoomInformationService : IRoomInformationService
    {
        private readonly IRoomInformationRepository iRoomInformationRepository;

        public RoomInformationService()
        {
            iRoomInformationRepository = new RoomInformationRepository();
        }

        public void AddRoomInformation(RoomInformation roomInformation)
        {
            iRoomInformationRepository.AddRoomInformation(roomInformation);
        }

        public void DeleteRoomInformation(int roomInformationId)
        {
            iRoomInformationRepository.DeleteRoomInformation(roomInformationId);
        }

        public int GetMaxRoomInformationId()
        {
            return iRoomInformationRepository.GetMaxRoomInformationId();
        }

        public List<RoomInformation> GetRoomInformationList()
        {
            return iRoomInformationRepository.GetAllRoomInformation();
        }

        public void UpdateRoomInformation(RoomInformation roomInformation)
        {
            iRoomInformationRepository.UpdateRoomInformation(roomInformation);
        }
    }
}
