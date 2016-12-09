using System.Configuration;

namespace WebApplication.Business.StructureMap.Configuration
{
	public class ConfigurationSection : System.Configuration.ConfigurationSection
	{
		#region Fields

		private const string _registriesPropertyName = "registries";

		#endregion

		#region Properties

		[ConfigurationProperty(_registriesPropertyName)]
		public virtual RegistryElementCollection Registries => (RegistryElementCollection) this[_registriesPropertyName];

		#endregion
	}
}