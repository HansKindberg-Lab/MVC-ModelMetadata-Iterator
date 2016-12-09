using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;

namespace WebApplication.Business.Configuration
{
	public class ConfigurationManagerWrapper : IConfigurationManager
	{
		#region Properties

		public virtual NameValueCollection AppSettings => ConfigurationManager.AppSettings;
		public virtual IEnumerable<ConnectionStringSettings> ConnectionStrings => ConfigurationManager.ConnectionStrings.Cast<ConnectionStringSettings>();

		#endregion

		#region Methods

		public virtual object GetSection(string sectionName)
		{
			return ConfigurationManager.GetSection(sectionName);
		}

		public virtual void RefreshSection(string sectionName)
		{
			ConfigurationManager.RefreshSection(sectionName);
		}

		#endregion
	}
}