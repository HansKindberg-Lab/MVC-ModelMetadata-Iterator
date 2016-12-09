namespace WebApplication.Business.Web.Html.Forms
{
	public class TextAreaComponent : FormComponent<ITextArea>
	{
		#region Constructors

		public TextAreaComponent(IHttpEncoder httpEncoder, string id, string name, bool required, string value) : base(httpEncoder, id, name, required, value) {}

		#endregion

		#region Methods

		protected internal override ITextArea CreateInputInternal()
		{
			return new TextArea(this.HttpEncoder);
		}

		#endregion
	}
}