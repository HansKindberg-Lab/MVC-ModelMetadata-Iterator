using System.Configuration;
using System.Diagnostics.CodeAnalysis;

namespace WebApplication.Business.StructureMap.Configuration
{
	public class RegistryElement : ConfigurationElement
	{
		#region Fields

		private const string _typePropertyName = "type";

		#endregion

		#region Properties

		[ConfigurationProperty(_typePropertyName, IsKey = true, IsRequired = true)]
		[SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods")]
		public virtual string Type
		{
			get { return (string) this[_typePropertyName]; }
			set { this[_typePropertyName] = value; }
		}

		#endregion
	}
}