using PitchPerfectHearingTest.Commands;
using PitchPerfectHearingTest.DataAccess;
using PitchPerfectHearingTest.DataAccess.Repositories;
using PitchPerfectHearingTest.Dialogs;
using PitchPerfectHearingTest.Dialogs.DialogService;
using PitchPerfectHearingTest.Dialogs.DialogYesNo;
using PitchPerfectHearingTest.Models;
using PitchPerfectHearingTest.Services;
using PitchPerfectHearingTest.ViewModels.AdminDashboard.Dialogs;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PitchPerfectHearingTest.ViewModels
{
    public class AppointmentAddContactInfoViewModel:BaseDialogViewModel
    {
        public event EventHandler<ChangeViewEventArgs> ViewChangeRequest;

        private WeekDayConverter weekDayConverter;

        private string _customerNameTextBox;
        private string _customerEmailTextBox;
        private string _customerAgeTextBox;

        public string CustomerNameTextBox { get { return _customerNameTextBox; } set { _customerNameTextBox = value; OnPropertyChanged("CustomerNameTextBox"); } }
        public string CustomerEmailTextBox { get { return _customerEmailTextBox; } set { _customerEmailTextBox = value; OnPropertyChanged("CustomerEmailTextBox"); } }
        public string CustomerAgeTextBox { get { return _customerAgeTextBox; } set { _customerAgeTextBox = value; OnPropertyChanged("CustomerAgeTextBox"); } }

        public AppointmentAddContactInfoViewModel()
        {
            weekDayConverter = new WeekDayConverter();
            SaveAppointment = new RelayCommand(SaveAppointmentToDataBase);
            BackToSelectTimeView = new RelayCommand(p => SwitchToSelectTimeView());
        }

        public ICommand SaveAppointment { get; set; }
        public ICommand BackToSelectTimeView { get; set; }
        public string SelectedTimeSlot
        {
            get
            {
                return ReturnSelectedTimeSlot();
            }
        }

        private bool SaveContactInfoToDatabase(object parameter)
        {
            //Check for empty fields
            bool textBoxEmpty = TextBoxValidation();
            if(textBoxEmpty == true)
            {
                MessageBox.Show("Volledig invullen alstublieft!!");
                return false;
            }
            else
            {
                Customer customer = new Customer(CustomerNameTextBox, CustomerEmailTextBox, int.Parse(CustomerAgeTextBox));
                var vm = new DialogYesNoViewModel("Contactgegevens bevestigen\n" +
                                                  "    Naam: " + CustomerNameTextBox + "\n" +
                                                  "    Email: " + CustomerEmailTextBox + "\n" +
                                                  "    Leeftijd: " + CustomerAgeTextBox);

                DialogResult result = DialogService.OpenDialog(vm, parameter as Window);
            //save customer to database
                if (result == DialogResult.Yes)
                {
                    using (var context = new ApplicationContext())
                    {
                        var customerRepo = new CustomerRepository(context);
                        customerRepo.Create(customer);

                        var parsed = int.TryParse(App.Current.Properties["TestResultId"].ToString(), out var resultId);
                        if (parsed)
                        {
                            var resultRepo = new HearingTestResultRepository(context);
                            var testResult = resultRepo.GetSingle(resultId);
                            testResult.Customer = customer.CustomerId;
                            resultRepo.Update(testResult);
                        }

                    }
                    //MessageBox.Show("saved");
                    SwitchView();

                }
                return true;
            }
        }
        
        private void SaveAppointmentToDataBase(object parameter)
        {
            if (SaveContactInfoToDatabase(parameter))
            {
                //Get timeslot andfrom Sessioncontext plus hearingtest dummy record
                var parsed = int.TryParse(App.Current.Properties["TestResultId"].ToString(), out var resultId);
                if (parsed && resultId > 0)
                {
                    var result = new HearingTestResultRepository(new ApplicationContext()).GetSingle(resultId);
                    if (result != null)
                    {
                        Appointment appointment = new Appointment(AppointmentType.CustomerAppointment, SessionContext.SelectedTimeSlot.TimeSlotId, result.HearingTestResultId);

                        using (var context = new ApplicationContext())
                        {
                            var appointmentRepo = new AppointmentRepository(context);
                            appointmentRepo.Create(appointment);
                            //MessageBox.Show("afspraak opgeslagen");
                        }
                    }
                }
            }
        }
        //Method to display selected timeslot
        private string ReturnSelectedTimeSlot()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(weekDayConverter.DayOfWeekDutch(SessionContext.SelectedTimeSlot.StartTime.DayOfWeek));
            sb.Append(" ");
            sb.Append(SessionContext.SelectedTimeSlot.StartTime.Day);
            sb.Append(" ");
            sb.Append(SessionContext.SelectedTimeSlot.StartTime.ToString("MMMM",CultureInfo.InvariantCulture));
            sb.Append("\n");
            sb.Append("          ");
            sb.Append(SessionContext.SelectedTimeSlot.StartTime.ToShortTimeString());
            sb.Append(" - ");
            sb.Append(SessionContext.SelectedTimeSlot.EndTime.ToShortTimeString());
            return sb.ToString();
        }

        // Method to ensure user enters numbers only to prevent exception when parsing char to int
        public void OnPreviewKeyDownEvent(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
                e.Handled = true;
        }

        //User must fill in all fields to continue (error protection)
        private bool TextBoxValidation()
        {
            bool atleastOneTextboxEmpty =
                    new string[] { CustomerNameTextBox,CustomerEmailTextBox, CustomerAgeTextBox }
                                .Any(t => String.IsNullOrEmpty(t));
            return atleastOneTextboxEmpty;
        }

        private void ClearTextBoxes()
        {
            CustomerNameTextBox = "";
            CustomerEmailTextBox = "";
            CustomerAgeTextBox = "";
        }

        private void SwitchView()
        {
            ViewChangeRequest?.Invoke(this, new ChangeViewEventArgs(nameof(AppointmentSuccessViewModel)));
        }
        private void SwitchToSelectTimeView()
        {
            ViewChangeRequest?.Invoke(this, new ChangeViewEventArgs(nameof(AppointmentSelectTimeDialogViewModel)));
        }
    }

    public class WeekDayConverter
    {
        public string DayOfWeekDutch(DayOfWeek dow)
        {
            string day = "";

            switch (dow)
            {
                case (DayOfWeek.Monday):
                    day = "Maandag";
                    break;
                case (DayOfWeek.Tuesday):
                    day = "Dinsdag";
                    break;
                case (DayOfWeek.Wednesday):
                    day = "Woensdag";
                    break;
                case (DayOfWeek.Thursday):
                    day = "Donderdag";
                    break;
                case (DayOfWeek.Friday):
                    day = "Vrijdag";
                    break;
                case (DayOfWeek.Saturday):
                    day = "Zaterdag";
                    break;
                case (DayOfWeek.Sunday):
                    day = "Zondag";
                    break;
            }
            return day;
        }
    }
}
