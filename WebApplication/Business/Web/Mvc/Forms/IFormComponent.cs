using System.ComponentModel.DataAnnotations;
using WebApplication.Business.Web.Html;

namespace WebApplication.Business.Web.Mvc.Forms
{
	public interface IFormComponent : IHtmlComponent
	{
		#region Properties

		DataType? DataType { get; set; }
		IHtmlTag HtmlTag { get; }
		string Id { get; set; }
		IHtmlTag Input { get; }
		IHtmlTag Label { get; }
		string Name { get; set; }

		#endregion
	}
}