using System.Diagnostics.CodeAnalysis;

namespace WebApplication.Business.Web.Html.Forms
{
	public class InputComponent : FormComponent<IInput>
	{
		#region Constructors

		public InputComponent(IHttpEncoder httpEncoder, string id, string name, bool required, InputType type, string value) : base(httpEncoder, id, name, required, value)
		{
			this.Type = type;
		}

		#endregion

		#region Properties

		[SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods")]
		protected internal virtual InputType Type { get; }

		#endregion

		#region Methods

		protected internal override IInput CreateInputInternal()
		{
			return new Input(this.HttpEncoder, this.Type);
		}

		#endregion
	}
}