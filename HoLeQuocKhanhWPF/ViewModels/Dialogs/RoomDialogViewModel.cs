using BLL.IServices;
using BLL.Services;
using BussinessObjects.Models;
using HoLeQuocKhanhWPF.ViewModels.Base;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace HoLeQuocKhanhWPF.ViewModels.Dialogs
{
    public class RoomDialogViewModel : ViewModelBase
    {
        private readonly IRoomInformationService _roomService;
        private readonly IRoomTypeService _roomTypeService;
        private RoomInformation _room;
        private bool _isEditMode;
        private ObservableCollection<RoomType> _roomTypes;

        public RoomInformation Room
        {
            get => _room;
            set
            {
                _room = value;
                OnPropertyChanged();
            }

        }

        public ObservableCollection<RoomType> RoomTypes
        {
            get => _roomTypes;
            set
            {
                _roomTypes = value;
                OnPropertyChanged();
            }
        }

        public ICommand SaveCommand { get; }

        public RoomDialogViewModel(RoomInformation room = null)
        {
            _roomService = new RoomInformationService();
            _roomTypeService = new RoomTypeService();

            _isEditMode = room != null;
            Room = room ?? new RoomInformation();

            LoadRoomTypes();

            SaveCommand = new RelayCommand<Window>(Save);
        }

        private void LoadRoomTypes()
        {
            RoomTypes = new ObservableCollection<RoomType>(_roomTypeService.GetRoomTypeList());
        }

        private void Save(Window window)
        {
            if (_isEditMode)
            {
                _roomService.UpdateRoomInformation(Room);
            }
            else
            {
                _roomService.AddRoomInformation(Room);
            }
            window.DialogResult = true;
            window.Close();
        }
    }
}