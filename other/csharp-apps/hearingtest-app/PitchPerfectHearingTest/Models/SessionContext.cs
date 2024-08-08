namespace PitchPerfectHearingTest.Models
{
    public sealed class SessionContext
    {
        private static readonly SessionContext _instance = new SessionContext();

        public static SessionContext Instance
        {
            get { return _instance; }
        }

        public HearingTest HearingTest { get; private set; }
        public TimeSlot SelectedTimeSlot { get; set; }

        static SessionContext() { }
        private SessionContext() 
        {
            HearingTest = new HearingTest();
        }

        public void ResetHearingTest()
        {
            HearingTest = new HearingTest();
        }
    }
}