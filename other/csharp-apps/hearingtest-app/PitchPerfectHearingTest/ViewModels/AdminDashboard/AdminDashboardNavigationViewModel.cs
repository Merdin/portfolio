using PitchPerfectHearingTest.Commands;
using PitchPerfectHearingTest.Models;
using System;
using System.Windows.Input;

namespace PitchPerfectHearingTest.ViewModels
{
    public class AdminDashboardNavigationViewModel : BaseViewModel
    {
        public ICommand GoToAdminDashboardDataAnalyseView { get; set; }
        public ICommand GoToAdminDashboardTimeslotsView { get; set; }
        public ICommand GoToAdminDashboardTimeslotLockViewModel { get; set; }

        public event EventHandler<ChangeViewEventArgs> ChangeView;

        public AdminDashboardNavigationViewModel()
        {
            GoToAdminDashboardDataAnalyseView = new ChangeViewCommand(p => ChangeView.Invoke(this, new ChangeViewEventArgs(nameof(AdminDashboardDataAnalyseViewModel))));
            GoToAdminDashboardTimeslotsView = new ChangeViewCommand(p => ChangeView.Invoke(this, new ChangeViewEventArgs(nameof(AdminDashboardTimeslotsViewModel))));
            GoToAdminDashboardTimeslotLockViewModel = new ChangeViewCommand(p => ChangeView.Invoke(this, new ChangeViewEventArgs(nameof(AdminDashboardTimeslotLockViewModel))));
        }
    }
}