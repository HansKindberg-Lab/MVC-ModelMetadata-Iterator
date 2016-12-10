namespace WebApplication.Business.Web.Html.Forms
{
	public class TextAreaComponent : FormComponent<ITextArea>
	{
		#region Constructors

		public TextAreaComponent(string displayText, IHttpEncoder httpEncoder, string id, string name, bool required, string value) : base(displayText, httpEncoder, id, name, required, value) {}

		#endregion

		#region Methods

		protected internal override ITextArea CreateInputInternal()
		{
			var textArea = new TextArea(this.HttpEncoder);

			if(!string.IsNullOrEmpty(this.Value))
				textArea.Children.Add(new HtmlText(this.HttpEncoder) {Value = this.Value});

			return textArea;
		}

		#endregion
	}
}