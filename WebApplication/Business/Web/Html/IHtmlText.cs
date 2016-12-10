namespace WebApplication.Business.Web.Html
{
	public interface IHtmlText : IHtmlNode
	{
		#region Properties

		bool Encode { get; set; }
		string Value { get; set; }

		#endregion
	}
}