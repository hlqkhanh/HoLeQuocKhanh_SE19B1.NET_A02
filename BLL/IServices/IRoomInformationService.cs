using BussinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.IServices
{
    public interface IRoomInformationService
    {
        List<RoomInformation> GetRoomInformationList();
        void AddRoomInformation(RoomInformation roomInformation);
        void DeleteRoomInformation(int roomInformationID);
        void UpdateRoomInformation(RoomInformation roomInformation);
        int GetMaxRoomInformationId();

    }
}
