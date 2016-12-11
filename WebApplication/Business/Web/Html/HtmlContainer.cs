using System;
using System.Collections.Generic;
using System.Text;
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
			var builder = new StringBuilder();

			for(var i = 0; i < this.Children.Count; i++)
			{
				var child = this.Children[i];

				if(child is IHtmlText)
				{
					builder.Append(child.ToHtmlString());
				}
				else
				{
					if(i > 0)
						builder.Append(Environment.NewLine);

					builder.Append(child.ToHtmlString(indentLevel));
				}
			}

			return new HtmlString(builder.ToString());
		}

		public override IHtmlString ToHtmlString(int indentLevel)
		{
			return this.ChildrenToHtmlString(indentLevel);
		}

		#endregion
	}
}