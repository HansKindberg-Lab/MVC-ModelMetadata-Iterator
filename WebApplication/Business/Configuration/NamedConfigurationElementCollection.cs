using System;
using System.Configuration;

namespace WebApplication.Business.Configuration
{
	public abstract class NamedConfigurationElementCollection<T> : ConfigurationElementCollection<T> where T : NamedConfigurationElement, new()
	{
		#region Methods

		protected override object GetElementKey(ConfigurationElement element)
		{
			if(element == null)
				throw new ArgumentNullException(nameof(element));

			return ((NamedConfigurationElement) element).Name;
		}

		#endregion
	}
}