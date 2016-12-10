using System.Collections.Generic;
using WebApplication.Business;
using WebApplication.Business.Web.Html;
using WebApplication.Business.Web.Mvc;
using WebApplication.Models.Forms;

namespace WebApplication.Models.ViewModels
{
	public interface IFormViewModel
	{
		#region Properties

		Form Form { get; }
		IEnumerable<IHtmlNode> HtmlNodes { get; }
		IModelMetadata ModelMetadata { get; }
		ISystemInformation SystemInformation { get; }

		#endregion
	}
}