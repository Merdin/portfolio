namespace PitchPerfectHearingTest.Interfaces
{
    public interface IDialogService<T, U>
    {
        bool? ShowDialog(string title, T dialog, U dataContext);
    }
}