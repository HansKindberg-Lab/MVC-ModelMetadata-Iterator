using WebApplication.Models.Forms;

namespace WebApplication.Models.ViewModels
{
	public class HomeViewModel
	{
		#region Fields

		private Form _form;

		#endregion

		#region Properties

		public virtual Form Form
		{
			get { return this._form ?? (this._form = new Form()); }
			set { this._form = value; }
		}

		#endregion
	}
}