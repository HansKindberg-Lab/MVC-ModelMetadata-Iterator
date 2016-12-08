using System;
using HtmlAgilityPack;

namespace WebApplication.Business.Web.Html
{
	[CLSCompliant(false)]
	public interface IHtmlDocumentFactory
	{
		#region Methods

		HtmlDocument Create();

		#endregion
	}
}