using System.Collections.Generic;

namespace WebApplication.Business.Web.Html
{
	public interface IHtmlContainer : IHtmlNode
	{
		#region Properties

		IList<IHtmlNode> Children { get; }

		#endregion
	}
}