using System;
using System.Collections.Generic;
using System.Text;

namespace PitchPerfectHearingTest.ViewModels
{
	public abstract class BaseDialogViewModel : BaseViewModel
	{
		public event EventHandler RequestClose;

		protected void OnRequestClose()
		{
			RequestClose?.Invoke(this, EventArgs.Empty);
		}
	}
}
