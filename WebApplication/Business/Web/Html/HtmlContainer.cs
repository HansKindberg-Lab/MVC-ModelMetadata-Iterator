using System;
using System.Collections.Generic;
using System.Web;

namespace WebApplication.Business.Web.Html
{
	public class HtmlContainer : BasicHtmlNode, IHtmlContainer
	{
		#region Fields

		private IList<IHtmlNode> _children;

		#endregion

		#region Properties

		public virtual IList<IHtmlNode> Children => this._children ?? (this._children = new HtmlComponentCollection<IHtmlNode>(this));

		#endregion

		#region Methods

		protected internal virtual IHtmlString ChildrenToHtmlString(int indentLevel)
		{
			var html = string.Empty;

			for(var i = 0; i < this.Children.Count; i++)
			{
				var child = this.Children[i];

				if(child is IHtmlText)
				{
					html += child.ToHtmlString();
				}
				else
				{
					if(i > 0)
						html += Environment.NewLine;

					html += child.ToHtmlString(indentLevel);
				}

				//if(i == this.Children.Count - 1)
				//	html += Environment.NewLine;
			}

			return new HtmlString(html);
		}

		public override IHtmlString ToHtmlString(int indentLevel)
		{
			return this.ChildrenToHtmlString(indentLevel);
		}

		#endregion
	}
}