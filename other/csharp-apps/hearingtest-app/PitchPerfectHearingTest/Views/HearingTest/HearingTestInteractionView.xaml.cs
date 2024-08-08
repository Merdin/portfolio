using System.Windows;
using System.Windows.Controls;

namespace PitchPerfectHearingTest.Views.HearingTest
{
    /// <summary>
    /// Interaction logic for HearingTestInteractionView.xaml
    /// </summary>
    public partial class HearingTestInteractionView : UserControl
    {
        public HearingTestInteractionView()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Focus();
        }
    }
}
