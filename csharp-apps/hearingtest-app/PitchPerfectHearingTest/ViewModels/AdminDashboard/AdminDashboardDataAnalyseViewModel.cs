using PitchPerfectHearingTest.Models;
using System;

namespace PitchPerfectHearingTest.ViewModels
{
    public class AdminDashboardDataAnalyseViewModel : BaseViewModel
    {
        public event EventHandler<ChangeViewEventArgs> ChangeView;

        private void CheckToken()
        {
            if (!new AuthenticationProvider().ValidateToken())
            {
                ChangeView?.Invoke(this, new ChangeViewEventArgs(nameof(AdminDashboardLoginViewModel)));
            }
        }
    }
}