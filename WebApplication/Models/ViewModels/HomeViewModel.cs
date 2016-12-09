using System;

namespace WebApplication.Models.ViewModels
{
	public class HomeViewModel : IHomeViewModel
	{
		#region Constructors

		public HomeViewModel(IFormViewModel formViewModel)
		{
			if(formViewModel == null)
				throw new ArgumentNullException(nameof(formViewModel));

			this.FormViewModel = formViewModel;
		}

		#endregion

		#region Properties

		public virtual IFormViewModel FormViewModel { get; }

		#endregion
	}
}