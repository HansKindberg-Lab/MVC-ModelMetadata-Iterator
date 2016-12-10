using System.Web;

namespace WebApplication.Business.Web.Html
{
	public class HtmlComment : BasicHtmlNode, IHtmlComment
	{
		#region Properties

		public virtual string Value { get; set; }

		#endregion

		#region Methods

		public override IHtmlString ToHtmlString(int indentLevel)
		{
			return new HtmlString(this.GetTabs(indentLevel) + this.Value);
		}

		#endregion
	}
}