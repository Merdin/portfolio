using PitchPerfectHearingTest.Interfaces;
using System;
using System.Windows;

namespace PitchPerfectHearingTest.Views.Introduction
{
    /// <summary>
    /// Interaction logic for IntroductionDialog.xaml
    /// </summary>
    public partial class IntroductionDialog : Window, IDialog
    {
        public bool IsClosed { get; set; }

        public IntroductionDialog()
        {
            InitializeComponent();
            this.Closed += DialogWindowClosed;
            
        }

        private void DialogWindowClosed(object sender, EventArgs e)
        {
            this.IsClosed = true;
        }
    }
}
