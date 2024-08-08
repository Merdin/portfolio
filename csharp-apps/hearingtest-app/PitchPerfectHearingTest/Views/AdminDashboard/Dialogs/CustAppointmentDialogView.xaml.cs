using PitchPerfectHearingTest.Services;
using System.Windows.Controls;

namespace PitchPerfectHearingTest.Views.AdminDashboard
{
    /// <summary>
    /// Interaction logic for CustAppointmentDialogView.xaml
    /// </summary>
    public partial class CustAppointmentDialogView : UserControl
    {
        public CustAppointmentDialogView()
        {
            InitializeComponent();
            InitializeAudiogram();
        }

        private void InitializeAudiogram()
        {
            view.Child = new AudiogramService().GenerateAudiogram();
        }
    }
}