using GalaSoft.MvvmLight.Command;
using Plugin.Connectivity;
using Soccer.Models;
using Soccer.Services;
using System.ComponentModel;
using System.Windows.Input;


namespace Soccer.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Attributes

        private ApiService apiService;
        private DialogService dialogService;
        private NavigationService navigationService;
        private string email;
        private string password;
        private bool isRunning;
        private bool isEnabled;
        private bool isRemembered;

        #endregion


        #region Properties
        public string Email
        {
            set
            {
                if (email != value)
                {
                    email = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Email"));
                }
            }
            get
            {
                return email;
            }
        }

        public string Password
        {
            set
            {
                if (password != value)
                {
                    password = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Password"));
                }
            }
            get
            {
                return password;
            }
        }

        public bool IsRunning
        {
            set
            {
                if (isRunning != value)
                {
                    isRunning = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsRunning"));
                }
            }
            get
            {
                return isRunning;
            }
        }

        public bool IsEnabled
        {
            set
            {
                if (isEnabled != value)
                {
                    isEnabled = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsEnabled"));
                }
            }
            get
            {
                return isEnabled;
            }
        }

        public bool IsRemembered
        {
            set
            {
                if (isRemembered != value)
                {
                    isRemembered = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsRemembered"));
                }
            }
            get
            {
                return isRemembered;
            }
        }
        #endregion


        #region Constructor

        public LoginViewModel()
        {
            apiService = new ApiService();
            dialogService = new DialogService();
            IsEnabled = true;
            IsRemembered = true;
            navigationService = new NavigationService();


        }

        #endregion


        #region Commands

        public ICommand LoginCommand { get { return new RelayCommand(Login); }}



        private async void Login()

        {
			if (string.IsNullOrEmpty(Email))
			{
				await dialogService.ShowMessage("Error", "You must enter the user email.");
				return;
			}

			if (string.IsNullOrEmpty(Password))
			{
				await dialogService.ShowMessage("Error", "You must enter a password.");
				return;
			}

			IsRunning = true;
			IsEnabled = false;

			//Not used since the nugget is not compatible with Visual Studio for Mac

			//  if (!CrossConnectivity.Current.IsConnected)
			//{
			//	IsRunning = false;
			//	IsEnabled = true;
			//	await dialogService.ShowMessage("Error", "Check you internet connection.");
			//	return;
			//} 


			//var isReachable = await CrossConnectivity.Current.IsRemoteReachable("www.google.com");

			//if (!isReachable)
			//{
			//	IsRunning = false;
			//	IsEnabled = true;
			//	await dialogService.ShowMessage("Error", "Check you internet connection.");
			//	return;
			//}



			var token = await apiService.GetToken("http://soccerapi.azurewebsites.net", Email, Password);

			if (token == null)
			{
				IsRunning = false;
				IsEnabled = true;
				await dialogService.ShowMessage("Error", "The user name or password in incorrect.");
				Password = null;
				return;
			}

			if (string.IsNullOrEmpty(token.AccessToken))
			{
				IsRunning = false;
				IsEnabled = true;
				await dialogService.ShowMessage("Error", token.ErrorDescription);
				Password = null;
				return;
			}

            var response = await apiService.GetUserByEmail(
                "http://soccerapi.azurewebsites.net", "/api",
                "/Users/GetUserByEmail", 
                token.TokenType, token.AccessToken, token.UserName);

			if (!response.IsSuccess)
			{
				IsRunning = false;
				IsEnabled = true;
				await dialogService.ShowMessage("Error", "Problem ocurred retrieving user information, try latter.");
				return;
			}


            Email = null;
            password = null;

            IsRunning = false;
			IsEnabled = true;
            var user = (User)response.Result;
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.CurrentUser = user;
            navigationService.SetMainPage("MasterPage");



        }

        #endregion





    }
}
