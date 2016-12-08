using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using HtmlAgilityPack;
using WebApplication.Business.Web.Html;

namespace WebApplication.Business.Web.Mvc.Html
{
	[CLSCompliant(false)]
	public class HtmlParser : BasicHtmlParser, IParser<IEnumerable<IHtmlComponent>>
	{
		#region Fields

		private const bool _defaultEncodeText = false;
		private bool? _encodeText;

		#endregion

		#region Constructors

		public HtmlParser(IHtmlDocumentFactory htmlDocumentFactory, IHttpEncoder httpEncoder) : base(htmlDocumentFactory)
		{
			if(httpEncoder == null)
				throw new ArgumentNullException(nameof(httpEncoder));

			this.HttpEncoder = httpEncoder;
		}

		#endregion

		#region Properties

		protected internal virtual bool DefaultEncodeText => _defaultEncodeText;

		public virtual bool EncodeText
		{
			get
			{
				if(this._encodeText == null)
					this._encodeText = this.DefaultEncodeText;

				return this._encodeText.Value;
			}
			set { this._encodeText = value; }
		}

		protected internal virtual IHttpEncoder HttpEncoder { get; }

		#endregion

		#region Methods

		public virtual bool CanParse(string value)
		{
			IEnumerable<IHtmlComponent> htmlComponents;
			return this.TryParse(value, out htmlComponents);
		}

		protected internal virtual IHtmlComponent CreateHtmlComponent(HtmlNode htmlNode)
		{
			if(htmlNode == null)
				throw new ArgumentNullException(nameof(htmlNode));

			// ReSharper disable SwitchStatementMissingSomeCases
			switch(htmlNode.NodeType)
			{
				case HtmlNodeType.Document:
					throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Invalid node-type, \"{0}\".", htmlNode.NodeType));
				case HtmlNodeType.Comment:
					return new HtmlComment {Value = htmlNode.InnerText};
				case HtmlNodeType.Element:
					return new HtmlTag((HtmlTextWriterTag) Enum.Parse(typeof(HtmlTextWriterTag), htmlNode.Name, true));
				default:
					return new HtmlText(this.HttpEncoder) {Encode = this.EncodeText, Value = htmlNode.InnerText};
			}
			// ReSharper restore SwitchStatementMissingSomeCases
		}

		protected internal virtual IEnumerable<IHtmlComponent> CreateHtmlComponents(IEnumerable<HtmlNode> htmlNodes)
		{
			if(htmlNodes == null)
				throw new ArgumentNullException(nameof(htmlNodes));

			var htmlComponents = new List<IHtmlComponent>();

			foreach(var htmlNode in htmlNodes)
			{
				var htmlComponent = this.CreateHtmlComponent(htmlNode);

				var htmlTag = htmlComponent as IHtmlTag;

				if(htmlTag != null)
				{
					foreach(var attribute in htmlNode.Attributes)
					{
						htmlTag.Attributes.Add(attribute.Name, attribute.Value);
					}

					foreach(var child in this.CreateHtmlComponents(htmlNode.ChildNodes))
					{
						htmlTag.Children.Add(child);
					}
				}

				htmlComponents.Add(htmlComponent);
			}

			return htmlComponents.ToArray();
		}

		public virtual IEnumerable<IHtmlComponent> Parse(string value)
		{
			var htmlNode = this.ParseInternal(value);

			return this.CreateHtmlComponents(htmlNode.ChildNodes).ToArray();
		}

		[SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
		public virtual bool TryParse(string value, out IEnumerable<IHtmlComponent> result)
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