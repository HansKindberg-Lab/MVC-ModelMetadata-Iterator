using System.Collections.Generic;

namespace WebApplication.Business.Web.Html
{
	public interface IHtmlContainer : IHtmlComponent
	{
		#region Properties

		IList<IHtmlComponent> Children { get; }

		#endregion
	}
}