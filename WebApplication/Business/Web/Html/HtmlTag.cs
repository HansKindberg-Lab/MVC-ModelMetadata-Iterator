using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;

namespace WebApplication.Business.Web.Html
{
	public class HtmlTag : HtmlContainer, IHtmlTag
	{
		#region Fields

		private const char _classSeparator = ' ';
		private const bool _defaultSelfClosingWhenNoChildren = false;
		private bool? _selfClosingWhenNoChildren;

		#endregion

		#region Constructors

		public HtmlTag(IHttpEncoder httpEncoder, HtmlTextWriterTag tag)
		{
			if(httpEncoder == null)
				throw new ArgumentNullException(nameof(httpEncoder));

			if(tag == HtmlTextWriterTag.Unknown)
				throw new ArgumentException("The tag cannot be unknown.", nameof(tag));

			this.HttpEncoder = httpEncoder;
			this.Tag = tag;
		}

		#endregion

		#region Properties

		public virtual IDictionary<string, object> Attributes { get; } = new SortedDictionary<string, object>(StringComparer.OrdinalIgnoreCase);
		protected internal virtual string ClassAttributeName => HtmlAttributeKey.Class;
		protected internal virtual char ClassSeparator => _classSeparator;
		protected internal virtual bool DefaultSelfClosingWhenNoChildren => _defaultSelfClosingWhenNoChildren;
		protected internal virtual IHttpEncoder HttpEncoder { get; }
		protected internal virtual string IdAttributeName => HtmlAttributeKey.Id;

		public virtual bool SelfClosingWhenNoChildren
		{
			get
			{
				if(this._selfClosingWhenNoChildren == null)
					this._selfClosingWhenNoChildren = this.DefaultSelfClosingWhenNoChildren;

				return this._selfClosingWhenNoChildren.Value;
			}
			set { this._selfClosingWhenNoChildren = value; }
		}

		public virtual HtmlTextWriterTag Tag { get; }

		[SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase")]
		public virtual string TagName => this.Tag.ToString().ToLowerInvariant();

		#endregion

		#region Methods

		public virtual void AddAttributeIfNotExist(string key, object value)
		{
			this.SetAttribute(key, value, false);
		}

		public virtual void AddAttributesIfNotExist<TKey, TValue>(IDictionary<TKey, TValue> attributes)
		{
			this.SetAttributes(attributes, false);
		}

		public virtual void AddClass(string value)
		{
			var classes = new List<string>();

			if(this.Attributes.ContainsKey(this.ClassAttributeName))
				classes.AddRange(this.SplitClasses(this.ConvertToString(this.Attributes[this.ClassAttributeName])));

			classes.AddRange(this.SplitClasses(value));

			this.Attributes[this.ClassAttributeName] = string.Join(this.ClassSeparator.ToString(), classes);
		}

		protected internal virtual void AppendAttributes(StringBuilder builder)
		{
			if(builder == null)
				throw new ArgumentNullException(nameof(builder));

			foreach(var attribute in this.Attributes)
			{
				var value = this.ConvertToString(attribute.Value);

				if(string.Equals(attribute.Key, this.IdAttributeName, StringComparison.OrdinalIgnoreCase) && string.IsNullOrEmpty(value))
					continue;

				builder.Append(" " + attribute.Key + "=\"" + this.HttpEncoder.HtmlAttributeEncode(value) + "\"");
			}
		}

		protected internal virtual TagRenderMode CalculateRenderMode()
		{
			if(!this.Children.Any() && this.SelfClosingWhenNoChildren)
				return TagRenderMode.SelfClosing;

			return TagRenderMode.Normal;
		}

		protected internal virtual string ConvertToString(object value)
		{
			return value == null ? null : Convert.ToString(value, CultureInfo.InvariantCulture);
		}

		public virtual void SetAttribute(string key, object value)
		{
			this.SetAttribute(key, value, true);
		}

		protected internal virtual void SetAttribute(string key, object value, bool replaceExisting)
		{
			if(string.IsNullOrWhiteSpace(key))
				throw new ArgumentException("The key cannot be null, empty or whitespace.", nameof(key));

			if(replaceExisting || !this.Attributes.ContainsKey(key))
				this.Attributes[key] = value;
		}

		public virtual void SetAttributes<TKey, TValue>(IDictionary<TKey, TValue> attributes)
		{
			this.SetAttributes(attributes, true);
		}

		protected internal virtual void SetAttributes<TKey, TValue>(IDictionary<TKey, TValue> attributes, bool replaceExisting)
		{
			if(attributes == null)
				throw new ArgumentNullException(nameof(attributes));

			foreach(var attribute in attributes)
			{
				this.SetAttribute(this.ConvertToString(attribute.Key), this.ConvertToString(attribute.Value), replaceExisting);
			}
		}

		public virtual void SetId(string id)
		{
			this.SetId(id, true);
		}

		protected internal virtual void SetId(string id, bool replaceExisting)
		{
			this.SetAttribute(this.IdAttributeName, id, replaceExisting);
		}

		public virtual void SetIdIfNotExist(string id)
		{
			this.SetId(id, false);
		}

		protected internal virtual IEnumerable<string> SplitClasses(string value)
		{
			return (value ?? string.Empty).Split(this.ClassSeparator).Where(@class => !string.IsNullOrWhiteSpace(@class));
		}

		public override IHtmlString ToHtmlString(int indentLevel)
		{
			return this.ToHtmlString(this.CalculateRenderMode(), indentLevel);
		}

		public virtual IHtmlString ToHtmlString(TagRenderMode renderMode)
		{
			return this.ToHtmlString(renderMode, 0);
		}

		public virtual IHtmlString ToHtmlString(TagRenderMode renderMode, int indentLevel)
		{
			if(indentLevel == int.MaxValue)
				throw new OverflowException(string.Format(CultureInfo.InvariantCulture, "The indent-level must be less than {0}.", int.MaxValue));

			var builder = new StringBuilder();

			var tabs = this.GetTabs(indentLevel);

			builder.Append(tabs);

			if(renderMode == TagRenderMode.Normal || renderMode == TagRenderMode.SelfClosing || renderMode == TagRenderMode.StartTag)
			{
				builder.Append("<");
				builder.Append(this.TagName);
				this.AppendAttributes(builder);
			}

			// ReSharper disable SwitchStatementMissingSomeCases
			switch(renderMode)
			{
				case TagRenderMode.StartTag:
					builder.Append(">");
					break;

				case TagRenderMode.EndTag:
					builder.Append("</");
					builder.Append(this.TagName);
					builder.Append(">");
					break;

				case TagRenderMode.SelfClosing:
					builder.Append(" />");
					break;

				default:
					builder.Append(">");
					if(this.Children.Any())
					{
						builder.Append(Environment.NewLine);
						builder.Append(this.ChildrenToHtmlString(indentLevel + 1));
						builder.Append(Environment.NewLine);
					}
					builder.Append(tabs);
					builder.Append("</");
					builder.Append(this.TagName);
					builder.Append(">");
					break;
			}
			// ReSharper restore SwitchStatementMissingSomeCases

			return new HtmlString(builder.ToString());
		}

		#endregion
	}
}