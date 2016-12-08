using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using WebApplication.Business.Web.Html;

namespace WebApplication.Business.Web.Mvc.Html
{
	public class HtmlContainer : IHtmlContainer
	{
		#region Fields

		private IList<IHtmlComponent> _children;
		private const TagRenderMode _defaultTagRenderMode = TagRenderMode.Normal;
		private TagRenderMode? _tagRenderMode;

		#endregion

		#region Properties

		public virtual IList<IHtmlComponent> Children => this._children ?? (this._children = new HtmlComponentCollection<IHtmlComponent>(this));
		protected internal virtual TagRenderMode DefaultTagRenderMode => _defaultTagRenderMode;
		public virtual IHtmlContainer Parent { get; set; }

		public virtual TagRenderMode TagRenderMode
		{
			get
			{
				if(this._tagRenderMode == null)
					this._tagRenderMode = this.DefaultTagRenderMode;

				return this._tagRenderMode.Value;
			}
			set { this._tagRenderMode = value; }
		}

		#endregion

		#region Methods

		public virtual IHtmlString ToHtmlString()
		{
			return this.ToHtmlString(this.TagRenderMode);
		}

		public virtual IHtmlString ToHtmlString(TagRenderMode renderMode)
		{
			var innerHtmlParts = new List<string>();

			// ReSharper disable LoopCanBeConvertedToQuery
			foreach(var child in this.Children)
			{
				var htmlTag = child as IHtmlTag;

				innerHtmlParts.Add(htmlTag != null ? htmlTag.ToString(renderMode) : child.ToString());
			}
			// ReSharper restore LoopCanBeConvertedToQuery

			return new HtmlString(string.Join(Environment.NewLine, innerHtmlParts));
		}

		public override string ToString()
		{
			return this.ToString(this.TagRenderMode);
		}

		public virtual string ToString(TagRenderMode renderMode)
		{
			return this.ToHtmlString(renderMode).ToHtmlString();
		}

		#endregion
	}
}