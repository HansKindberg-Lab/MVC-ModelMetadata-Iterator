using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace WebApplication.Business
{
	public class SystemInformation : ISystemInformation
	{
		#region Properties

		public virtual IList<string> DetailedInformation { get; } = new List<string>();
		public virtual string Heading { get; set; }
		public virtual IList<string> Information { get; } = new List<string>();

		[SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods")]
		public virtual SystemInformationType Type { get; set; }

		#endregion
	}
}