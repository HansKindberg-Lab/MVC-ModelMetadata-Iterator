using System.Web;

namespace WebApplication.Business.Web.Html
{
	public interface IHtmlComponent
	{
		#region Properties

		IHtmlContainer Parent { get; set; }

		#endregion

		#region Methods

		IHtmlString ToHtmlString();

		#endregion
	}
}