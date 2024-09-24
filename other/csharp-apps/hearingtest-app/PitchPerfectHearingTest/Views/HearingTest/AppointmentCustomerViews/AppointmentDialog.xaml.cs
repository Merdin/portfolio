﻿using PitchPerfectHearingTest.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PitchPerfectHearingTest.Dialogs
{
    /// <summary>
    /// Interaction logic for AppointmentDialog.xaml
    /// </summary>
    public partial class AppointmentDialog : Window, IDialog
    {
        public bool IsClosed { get; set; }

        public AppointmentDialog()
        {
            InitializeComponent();
        }
    }
}
