using System;
using System.Collections.Generic;
using System.Linq;
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
			return new HtmlString(string.Join(Environment.NewLine, this.Children.Select(child => child.ToHtmlString(indentLevel))));
		}

		public override IHtmlString ToHtmlString(int indentLevel)
		{
			return this.ChildrenToHtmlString(indentLevel);
		}

		#endregion
	}
}