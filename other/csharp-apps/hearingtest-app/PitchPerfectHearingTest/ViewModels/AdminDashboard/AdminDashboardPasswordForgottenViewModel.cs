using PitchPerfectHearingTest.Commands;
using PitchPerfectHearingTest.Models;
using System.Windows.Input;
using System;
using System.Windows.Media;
using PitchPerfectHearingTest.ViewModels.AdminDashboard.Dialogs;
using PitchPerfectHearingTest.Services;
using PitchPerfectHearingTest.Dialogs.DialogService;
using PitchPerfectHearingTest.Interfaces;
using PitchPerfectHearingTest.Dialogs;

namespace PitchPerfectHearingTest.ViewModels
{
    public class AdminDashboardPasswordForgottenViewModel : BaseViewModel
    {
        private string _username;
        private string _email;
        private string _errorLabel = "Voer de juiste combinatie in.";
        private Brush _usernameBackground = Brushes.Black;
        private Brush _emailBackground = Brushes.Black;
        private Brush _errorLabelBackground = Brushes.Black;
        private Brush _resetPasswordButtonBackground = Brushes.LightGray;

        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
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

        public Brush EmailBackground
        {
            get { return _emailBackground; }
            set
            {
                _emailBackground = value;
                OnPropertyChanged(nameof(EmailBackground));
            }
        }

        public Brush ResetPasswordButtonBackground
        {
            get { return _resetPasswordButtonBackground; }
            set
            {
                _resetPasswordButtonBackground = value;
                OnPropertyChanged(nameof(ResetPasswordButtonBackground));
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
        public ICommand ResetPasswordButton { get; }

        public AdminDashboardPasswordForgottenViewModel()
        {
            SetDefaultBackground();

            ResetPasswordButton = new RelayCommand(p =>
            {
                try
                {
                    var auth = new AuthenticationProvider();
                    if (auth.ResetPassword(Username, Email))
                    {
                        ResetPasswordSuccessfull();
                    }
                    
                }
                catch (NullReferenceException e)
                {
                    ErrorLabel = e.Message;
                    SetErrorBackground();
                }
            });
        }
        private void SetDefaultBackground()
        {
            ErrorLabel = "Voer de juiste combinatie in.";
            UsernameBackground = Brushes.Black;
            EmailBackground = Brushes.Black;
            ErrorLabelBackground = Brushes.Black;
            ResetPasswordButtonBackground = Brushes.LightGray;
        }

        private void SetErrorBackground()
        {
            UsernameBackground = Brushes.Red;
            EmailBackground = Brushes.Red;
            ErrorLabelBackground = Brushes.Red;
            ResetPasswordButtonBackground = Brushes.Red;
        }

        private void ResetPasswordSuccessfull()
        {
            ErrorLabel = $"Uw wachtwoord is veranderd naar: {AuthenticationProvider.DefaultPassword}";
            ErrorLabelBackground = Brushes.Green;
        }
    }
}
