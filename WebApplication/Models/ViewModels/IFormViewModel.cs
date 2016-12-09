using System.Collections.Generic;
using WebApplication.Business;
using WebApplication.Business.Web.Html.Forms;

namespace WebApplication.Models.ViewModels
{
	public interface IFormViewModel
	{
		#region Properties

		IEnumerable<IFormComponent> Components { get; }
		ISystemInformation SystemInformation { get; }

		#endregion
	}
}