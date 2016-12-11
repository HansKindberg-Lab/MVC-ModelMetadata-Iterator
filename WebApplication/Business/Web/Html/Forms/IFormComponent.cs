namespace WebApplication.Business.Web.Html.Forms
{
	public interface IFormComponent : IHtmlTag
	{
		#region Properties

		string Id { get; }
		IFormComponentInput Input { get; }
		IHtmlTag Label { get; }
		string Name { get; }
		string Value { get; }

		#endregion
	}

	public interface IFormComponent<out T> : IFormComponent where T : IFormComponentInput
	{
		#region Properties

		new T Input { get; }

		#endregion
	}
}