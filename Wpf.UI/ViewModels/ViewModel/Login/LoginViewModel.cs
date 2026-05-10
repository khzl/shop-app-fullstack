using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Wpf.UI.ClientServices.Interfaces;
using Wpf.UI.Shared;
using Wpf.UI.ViewModels.Base;

namespace Wpf.UI.ViewModels.ViewModel.Login
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly IAuthClientService _authClientService;

        // Constructor to inject the authentication client service
        public LoginViewModel(IAuthClientService authClientService)
        {
            _authClientService = authClientService;
            LoginCommand = new RelayCommand(async _ => await Login());
        }

        #region Properties 
        private string? _email;
        public string? Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }
        private string? _password;
        public string? Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }
        public ICommand LoginCommand { get; } // readOnly command property
        #endregion

        private async Task Login()
        {
            var result = await _authClientService.LoginAsync(Email, Password);
            // Navigate to Dashboard
        }
    }
}
