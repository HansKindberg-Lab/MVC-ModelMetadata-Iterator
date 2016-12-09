using System.Collections.Generic;

namespace WebApplication.Business
{
	public interface ISystemInformation
	{
		#region Properties

		IList<string> DetailedInformation { get; }
		string Heading { get; set; }
		IList<string> Information { get; }
		SystemInformationType Type { get; set; }

		#endregion
	}
}