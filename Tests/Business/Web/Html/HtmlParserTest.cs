using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebApplication.Business.Web;
using WebApplication.Business.Web.Html;

namespace Tests.Business.Web.Html
{
	[CLSCompliant(false)]
	[TestClass]
	public class HtmlParserTest
	{
		#region Properties

		protected internal virtual IHtmlDocumentFactory HtmlDocumentFactory { get; } = new HtmlDocumentFactory();

		#endregion

		#region Methods

		protected internal virtual HtmlParser CreateHtmlParser()
		{
			return new HtmlParser(this.HtmlDocumentFactory, Mock.Of<IHttpEncoder>());
		}

		[TestMethod]
		public void ParseTest()
		{
			var htmlParser = this.CreateHtmlParser();

			var htmlComponents = htmlParser.Parse("<p>P-text</p><!-- Comment --><p>P-text</p>Text<div>Div-text</div>Text");

			Assert.AreEqual(6, htmlComponents.Count());
		}

		#endregion
	}
}