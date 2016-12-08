using System.Collections.Generic;

namespace WebApplication.Business.Web.Html
{
	public interface IHtmlIdFactory
	{
		#region Methods

		string Create(IEnumerable<string> parts);

		#endregion
	}
}