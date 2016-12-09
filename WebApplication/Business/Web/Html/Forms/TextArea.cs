using System.Web.UI;

namespace WebApplication.Business.Web.Html.Forms
{
	public class TextArea : BasicInput, ITextArea
	{
		#region Constructors

		public TextArea(IHttpEncoder httpEncoder) : base(httpEncoder, HtmlTextWriterTag.Textarea) {}

		#endregion
	}
}