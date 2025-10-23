using HoLeQuocKhanhWPF.ViewModels.Base;
using HoLeQuocKhanhWPF.Views; 
using System.Windows;        
using System.Windows.Input;

namespace HoLeQuocKhanhWPF.ViewModels.Admins
{
    public class AdminMainViewModel : ViewModelBase
    {
        private ViewModelBase _currentViewModel;

        public ViewModelBase CurrentViewModel
        {
            get { return _currentViewModel; }
            set
            {
                _currentViewModel = value;
                OnPropertyChanged();
            }
        }

        public ICommand ShowCustomerManagementCommand { get; }
        public ICommand ShowRoomManagementCommand { get; }
        public ICommand ShowReportCommand { get; }
        public ICommand LogoutCommand { get; }

        public AdminMainViewModel()
        {
            ShowCustomerManagementCommand = new RelayCommand<object>(p => CurrentViewModel = new CustomerManagementViewModel());
            ShowRoomManagementCommand = new RelayCommand<object>(p => CurrentViewModel = new RoomManagementViewModel());
            ShowReportCommand = new RelayCommand<object>(p => CurrentViewModel = new ReportViewModel());

            LogoutCommand = new RelayCommand<Window>(Logout);

            CurrentViewModel = new CustomerManagementViewModel();
        }

        private void Logout(Window window)
        {
            if (window != null)
            {
                var loginView = new LoginView();
                
                Application.Current.MainWindow = loginView;
                loginView.Show();
                
                window.Close();
            }
        }
    }
}