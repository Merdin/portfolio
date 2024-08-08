using PitchPerfectHearingTest.Interfaces;
using PitchPerfectHearingTest.ViewModels;
using PitchPerfectHearingTest.Views.Introduction;
using System.Windows;

namespace PitchPerfectHearingTest.Services
{
	/**
	 * T = The Dialog (type of Window & IDialog)
	 * U = BaseDialogViewModel (class that inherits the BaseDialogViewModel)
	 *  new WindowDialogService<ExampleIDialogWindow, ExampleBaseDialogViewModel>()
	 */
    public class WindowDialogService<T, U> : IDialogService<T, U> where T : Window, IDialog where U : BaseDialogViewModel
	{
		public bool? ShowDialog(string title, T dialog, U dataContext)
		{
			dataContext.RequestClose += (sender, e) => 
			{ 
				dialog.IsClosed = true; 
				dialog.Close(); 
			};

			dialog.Title = title;
			dialog.DataContext = dataContext;
			dialog.ShowDialog();
			

			return dialog.IsClosed;
		}
	}
}
