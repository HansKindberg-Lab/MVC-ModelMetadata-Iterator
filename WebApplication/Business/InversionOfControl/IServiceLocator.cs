namespace WebApplication.Business.InversionOfControl
{
	public interface IServiceLocator
	{
		#region Methods

		T GetService<T>();

		#endregion
	}
}