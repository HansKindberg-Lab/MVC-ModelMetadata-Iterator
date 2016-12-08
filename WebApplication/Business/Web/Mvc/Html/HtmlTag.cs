using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using WebApplication.Business.Web.Html;

namespace WebApplication.Business.Web.Mvc.Html
{
	public class HtmlTag : HtmlContainer, IHtmlTag
	{
		#region Fields

		private const string _classAttributeName = "class";
		private const char _classSeparator = ' ';
		private TagBuilder _tagBuilder;

		#endregion

		#region Constructors

		public HtmlTag(HtmlTextWriterTag tagType)
		{
			if(tagType == HtmlTextWriterTag.Unknown)
				throw new ArgumentException("The tag-type cannot be unknown.", nameof(tagType));

			this.TagType = tagType;
		}

		#endregion

		#region Properties

		public virtual IDictionary<string, string> Attributes => this.TagBuilder.Attributes;
		protected internal virtual string ClassAttributeName => _classAttributeName;
		protected internal virtual char ClassSeparator => _classSeparator;

		protected internal virtual string IdAttributeDotReplacement
		{
			get { return this.TagBuilder.IdAttributeDotReplacement; }
			set { this.TagBuilder.IdAttributeDotReplacement = value; }
		}

		protected internal virtual TagBuilder TagBuilder => this._tagBuilder ?? (this._tagBuilder = new TagBuilder(this.TagName));

		[SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase")]
		public virtual string TagName => this.TagType.ToString().ToLowerInvariant();

		public virtual HtmlTextWriterTag TagType { get; }

		#endregion

		#region Methods

		public virtual void AddClass(string value)
		{
			var classes = new List<string>();

			if(this.Attributes.ContainsKey(this.ClassAttributeName))
				classes.AddRange(this.SplitClasses(this.Attributes[this.ClassAttributeName]));

			classes.AddRange(this.SplitClasses(value));

			this.Attributes[this.ClassAttributeName] = string.Join(this.ClassSeparator.ToString(), classes);
		}

		public virtual void MergeAttribute(string key, string value)
		{
			this.TagBuilder.MergeAttribute(key, value);
		}

		public virtual void MergeAttribute(string key, string value, bool replaceExisting)
		{
			this.TagBuilder.MergeAttribute(key, value, replaceExisting);
		}

		public virtual void MergeAttributes<TKey, TValue>(IDictionary<TKey, TValue> attributes)
		{
			this.TagBuilder.MergeAttributes(attributes);
		}

		public virtual void MergeAttributes<TKey, TValue>(IDictionary<TKey, TValue> attributes, bool replaceExisting)
		{
			this.TagBuilder.MergeAttributes(attributes, replaceExisting);
		}

		protected internal virtual IEnumerable<string> SplitClasses(string value)
		{
			return (value ?? string.Empty).Split(this.ClassSeparator).Where(@class => !string.IsNullOrWhiteSpace(@class));
		}

		public override IHtmlString ToHtmlString(TagRenderMode renderMode)
		{
			var tagBuilder = new TagBuilder(this.TagName)
			{
				IdAttributeDotReplacement = this.IdAttributeDotReplacement
			};

			foreach(var attribute in this.Attributes.OrderBy(attribute => attribute.Key))
			{
				tagBuilder.Attributes.Add(attribute);
			}

			tagBuilder.InnerHtml = base.ToHtmlString(renderMode).ToHtmlString();

			return new HtmlString(tagBuilder.ToString(renderMode));
		}

		#endregion
	}
}