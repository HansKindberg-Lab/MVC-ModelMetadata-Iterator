namespace WebApplication.Business.Web.Html.Forms
{
	public interface IFormComponentInput : IHtmlTag
	{
		#region Methods

		void SetName(string name);
		void SetNameIfNotExist(string name);
		void SetReadOnly(bool value);
		void SetRequired(bool value);

		#endregion
	}
}