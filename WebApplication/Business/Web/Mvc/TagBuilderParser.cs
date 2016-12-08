using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Web.Mvc;
using HtmlAgilityPack;
using WebApplication.Business.Web.Html;

namespace WebApplication.Business.Web.Mvc
{
	[CLSCompliant(false)]
	public class TagBuilderParser : BasicHtmlParser, IParser<IEnumerable<TagBuilder>>
	{
		#region Constructors

		public TagBuilderParser(IHtmlDocumentFactory htmlDocumentFactory) : base(htmlDocumentFactory) {}

		#endregion

		#region Methods

		public virtual bool CanParse(string value)
		{
			IEnumerable<TagBuilder> tagBuilders;
			return this.TryParse(value, out tagBuilders);
		}

		public virtual IEnumerable<TagBuilder> Parse(string value)
		{
			var htmlNode = this.ParseInternal(value);

			var tagBuilders = new List<TagBuilder>();

			foreach(var childNode in htmlNode.ChildNodes)
			{
				if(childNode.NodeType != HtmlNodeType.Element)
					throw new InvalidOperationException("Cannot parse anything else than element-nodes.");

				var tagBuilder = new TagBuilder(childNode.Name);

				foreach(var attribute in childNode.Attributes)
				{
					tagBuilder.Attributes.Add(attribute.Name, attribute.Value);
				}

				tagBuilder.InnerHtml = childNode.InnerHtml;

				tagBuilders.Add(tagBuilder);
			}

			return tagBuilders.ToArray();
		}

		[SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
		public virtual bool TryParse(string value, out IEnumerable<TagBuilder> result)
		{
			try
			{
				result = this.Parse(value);
				return true;
			}
			catch
			{
				result = null;
				return false;
			}
		}

		#endregion
	}
}