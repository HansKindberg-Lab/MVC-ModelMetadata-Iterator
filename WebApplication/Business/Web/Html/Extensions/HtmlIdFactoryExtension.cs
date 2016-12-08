using System;

namespace WebApplication.Business.Web.Html.Extensions
{
	public static class HtmlIdFactoryExtension
	{
		#region Methods

		public static string Create(this IHtmlIdFactory htmlIdFactory, params string[] parts)
		{
			if(htmlIdFactory == null)
				throw new ArgumentNullException(nameof(htmlIdFactory));

			return htmlIdFactory.Create(parts);
		}

		#endregion
	}
}