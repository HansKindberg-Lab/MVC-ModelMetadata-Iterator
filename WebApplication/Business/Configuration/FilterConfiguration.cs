using System.Web.Mvc;

namespace WebApplication.Business.Configuration
{
	public class FilterConfiguration : IApplicationConfiguration
	{
		#region Methods

		public void Configure()
		{
			this.RegisterFilters(GlobalFilters.Filters);
		}

		protected internal virtual void RegisterFilters(GlobalFilterCollection filters) {}

		#endregion
	}
}