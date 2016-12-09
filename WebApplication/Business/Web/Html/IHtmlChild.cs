namespace WebApplication.Business.Web.Html
{
	public interface IHtmlChild
	{
		#region Properties

		IHtmlContainer ParentInternal { get; set; }

		#endregion
	}
}