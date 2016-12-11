namespace WebApplication.Models.ViewModels
{
	public interface IFormViewModelSettings
	{
		#region Properties

		string CheckBoxClass { get; set; }
		string ComponentClass { get; set; }
		string TextInputClass { get; set; }
		string ValidationClass { get; set; }

		#endregion
	}
}