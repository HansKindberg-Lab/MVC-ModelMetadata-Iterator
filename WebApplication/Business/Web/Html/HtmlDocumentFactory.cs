using System;
using HtmlAgilityPack;

namespace WebApplication.Business.Web.Html
{
	public class HtmlDocumentFactory : IHtmlDocumentFactory
	{
		#region Constructors

		public HtmlDocumentFactory()
		{
			//HtmlNode.ElementsFlags.Remove("form");
			//HtmlNode.ElementsFlags.Remove("option");
		}

		#endregion

		#region Methods

		[CLSCompliant(false)]
		public virtual HtmlDocument Create()
		{
			return new HtmlDocument
			{
				OptionCheckSyntax = true,
				OptionWriteEmptyNodes = true
			};
		}

		#endregion
	}
}