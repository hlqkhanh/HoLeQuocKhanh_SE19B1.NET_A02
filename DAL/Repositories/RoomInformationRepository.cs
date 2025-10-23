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
    public class RoomInformationRepository : IRoomInformationRepository
    {
        public void AddRoomInformation(RoomInformation roomInformation)
        {
            RoomInformationDAO.Add(roomInformation);
        }

        public void DeleteRoomInformation(int roomInformationID)
        {
            RoomInformationDAO.Delete(roomInformationID);
        }

        public List<RoomInformation> GetAllRoomInformation()
        {
            return RoomInformationDAO.GetAll();
        }

        public int GetMaxRoomInformationId()
        {
            return RoomInformationDAO.GetMaxRoomInformationID();
        }

        public void UpdateRoomInformation(RoomInformation roomInformation)
        {
            RoomInformationDAO.Update(roomInformation);
        }
    }
}
