using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using HtmlAgilityPack;

namespace WebApplication.Business.Web.Html
{
	[CLSCompliant(false)]
	public abstract class BasicHtmlParser
	{
		#region Constructors

		protected BasicHtmlParser(IHtmlDocumentFactory htmlDocumentFactory)
		{
			if(htmlDocumentFactory == null)
				throw new ArgumentNullException(nameof(htmlDocumentFactory));

			this.HtmlDocumentFactory = htmlDocumentFactory;
		}

		#endregion

		#region Properties

		protected internal virtual IHtmlDocumentFactory HtmlDocumentFactory { get; }

		#endregion

		#region Methods

		protected internal virtual HtmlDocument CreateHtmlDocument()
		{
			return this.HtmlDocumentFactory.Create();
		}

		protected internal virtual HtmlNode ParseInternal(string value)
		{
			var htmlDocument = this.CreateHtmlDocument();

			htmlDocument.LoadHtml(value);

			// ReSharper disable InvertIf
			if(htmlDocument.OptionCheckSyntax && htmlDocument.ParseErrors.Any())
			{
				var parseErrors = htmlDocument.ParseErrors.Select(parseError => string.Format(CultureInfo.InvariantCulture, "Code = {0}, SourceText = {1}, Reason = {2}", parseError.Code, parseError.SourceText, parseError.Reason));

				var messages = new List<string>
				{
					"Could not parse the value:",
					"\"" + value + "\"",
					string.Empty,
					"Parse errors:",
					" - " + string.Join(Environment.NewLine + " - ", parseErrors)
				};

				throw new InvalidOperationException(string.Join(Environment.NewLine, messages));
			}
			// ReSharper restore InvertIf

			return htmlDocument.DocumentNode;
		}

		#endregion
	}
}