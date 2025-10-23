using BussinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.IRepositories
{
    public interface IRoomInformationRepository
    {
        List<RoomInformation> GetAllRoomInformation();
        void AddRoomInformation(RoomInformation roomInformation);
        void DeleteRoomInformation(int roomInformationID);
        void UpdateRoomInformation(RoomInformation roomInformation);

        int GetMaxRoomInformationId();

    }
}
