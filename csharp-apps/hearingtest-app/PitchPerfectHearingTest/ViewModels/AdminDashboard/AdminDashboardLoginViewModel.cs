using PitchPerfectHearingTest.Commands;
using PitchPerfectHearingTest.Models;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace PitchPerfectHearingTest.ViewModels
{
    public class AdminDashboardLoginViewModel : BaseViewModel
    {
        private string _username;
        private string _password;
        private string _errorLabel = "Voer gegevens in.";
        private Brush _usernameBackground = Brushes.Black;
        private Brush _passwordBackground = Brushes.Black;
        private Brush _errorLabelBackground = Brushes.Black;
        private Brush _loginButtonBackground = Brushes.LightGray;

        public string Username 
        { 
            get { return _username; }
            set 
            { 
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        public string Password 
        { 
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
        public string ErrorLabel 
        { 
            get { return _errorLabel; }
            set
            {
                _errorLabel = value;
                OnPropertyChanged(nameof(ErrorLabel));
            } 
        }

        public Brush UsernameBackground
        {
            get { return _usernameBackground; }
            set
            {
                _usernameBackground = value;
                OnPropertyChanged(nameof(UsernameBackground));
            }
        }

        public Brush PasswordBackground
        {
            get { return _passwordBackground; }
            set
            {
                _passwordBackground = value;
                OnPropertyChanged(nameof(PasswordBackground));
            }
        }

        public Brush LoginButtonBackground
        {
            get { return _loginButtonBackground; }
            set 
            {
                _loginButtonBackground = value;
                OnPropertyChanged(nameof(LoginButtonBackground));
            }
        }

        public Brush ErrorLabelBackground
        {
            get { return _errorLabelBackground; }
            set
            {
                _errorLabelBackground = value;
                OnPropertyChanged(nameof(ErrorLabelBackground));
            }
        }
        public ICommand LoginButton { get; }
        public ICommand PasswordForgottenButton { get; }

        public event EventHandler<ChangeViewEventArgs> ChangeView;

        public AdminDashboardLoginViewModel()
        {
            SetDefaultBackground();

            LoginButton = new RelayCommand(p =>
            {
                try {
                    SetDefaultBackground();

                    var authenticationProvider = new AuthenticationProvider();
                    var response = authenticationProvider.AuthenticateUser(Username, p);
                    if (response.IsValidated)
                    {
                        if (Application.Current.Properties.Contains("AuthenticatedUser"))
                        {
                            Application.Current.Properties["AuthenticatedUser"] = response.AuthenticatedUser;
                        }
                        else
                        {
                            Application.Current.Properties.Add("AuthenticatedUser", response.AuthenticatedUser);
                        }
                    }

                    SwitchToNavigation();

                } catch (NullReferenceException e) {
                    ErrorLabel = e.Message;
                    SetErrorBackground();
                } catch (UnauthorizedAccessException e) {
                    ErrorLabel = e.Message;
                    SetErrorBackground();
                }
            });

            PasswordForgottenButton = new RelayCommand(p =>
            {
                SetDefaultBackground();
                ChangeView?.Invoke(this, new ChangeViewEventArgs(nameof(AdminDashboardPasswordForgottenViewModel)));
            });
        }

        private void SwitchToNavigation()
        {
            ChangeView?.Invoke(this, new ChangeViewEventArgs(nameof(AdminDashboardNavigationViewModel)));
        }

        private void SetDefaultBackground()
        {
            ErrorLabel = "Voer gegevens in.";
            UsernameBackground = Brushes.Black;
            PasswordBackground = Brushes.Black;
            ErrorLabelBackground = Brushes.Black;
            LoginButtonBackground = Brushes.LightGray;
        }

        private void SetErrorBackground()
        {
            UsernameBackground = Brushes.Red;
            PasswordBackground = Brushes.Red;
            ErrorLabelBackground = Brushes.Red;
            LoginButtonBackground = Brushes.Red;
        }
    }
}
