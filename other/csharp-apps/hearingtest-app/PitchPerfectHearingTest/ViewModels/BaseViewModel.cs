using PitchPerfectHearingTest.Models;
using System.ComponentModel;

namespace PitchPerfectHearingTest.ViewModels
{
    public delegate void TestSound();

    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public SessionContext SessionContext
        {
            get { return SessionContext.Instance; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}