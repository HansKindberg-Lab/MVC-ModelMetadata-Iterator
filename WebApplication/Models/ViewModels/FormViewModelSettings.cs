namespace WebApplication.Models.ViewModels
{
	public class FormViewModelSettings : IFormViewModelSettings
	{
		#region Properties

		public virtual string CheckBoxClass { get; set; }
		public virtual string ComponentClass { get; set; }
		public virtual string TextInputClass { get; set; }
		public virtual string ValidationClass { get; set; }

		#endregion
	}
}