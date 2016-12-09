namespace WebApplication.Business.Web.Html.Forms
{
	public interface IFormComponent : IHtmlTag
	{
		#region Properties

		IFormComponentInput Input { get; }
		IHtmlTag Label { get; }

		#endregion
	}

	public interface IFormComponent<out T> : IFormComponent where T : IFormComponentInput
	{
		#region Properties

		new T Input { get; }

		#endregion
	}
}