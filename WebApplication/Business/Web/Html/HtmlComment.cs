using System.Web;

namespace WebApplication.Business.Web.Html
{
	public class HtmlComment : IHtmlComponent
	{
		#region Properties

		public virtual IHtmlContainer Parent { get; set; }
		public virtual string Value { get; set; }

		#endregion

		#region Methods

		public virtual IHtmlString ToHtmlString()
		{
			return new HtmlString(this.Value ?? string.Empty);
		}

		public override string ToString()
		{
			return this.ToHtmlString().ToHtmlString();
		}

		#endregion
	}
}