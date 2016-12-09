using System.Web;

namespace WebApplication.Business.Web.Html
{
	public interface IHtmlNode
	{
		#region Properties

		IHtmlContainer Parent { get; }

		#endregion

		#region Methods

		IHtmlString ToHtmlString();
		IHtmlString ToHtmlString(int indentLevel);

		#endregion
	}
}