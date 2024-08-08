using PitchPerfectHearingTest.Interfaces;
using PitchPerfectHearingTest.Services;
using System.Windows;
using System.Windows.Controls;

namespace PitchPerfectHearingTest.Dialogs
{
    /// <summary>
    /// Interaction logic for AudiogramDialog.xaml
    /// </summary>
    public partial class AudiogramDialog : Window, IDialog
    {
        public bool IsClosed { get; set; }

        public AudiogramDialog()
        {
            InitializeComponent();
            InitializeAudiogram();
        }

        private void InitializeAudiogram()
        {
            grdCanvas.Children.Add(new Viewbox{ Child = new AudiogramService().GenerateAudiogram() });
        }
    }
}