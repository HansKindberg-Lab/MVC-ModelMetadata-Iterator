using System;
using System.Configuration;
using WebApplication.Business.Configuration;

namespace WebApplication.Business.StructureMap.Configuration
{
	public class RegistryElementCollection : ConfigurationElementCollection<RegistryElement>
	{
		#region Methods

		protected override object GetElementKey(ConfigurationElement element)
		{
			if(element == null)
				throw new ArgumentNullException(nameof(element));

			return ((RegistryElement) element).Type;
		}

		#endregion
	}
}