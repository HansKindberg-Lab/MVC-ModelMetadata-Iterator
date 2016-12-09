using System.Collections.Generic;

namespace WebApplication.Business
{
	public interface ISystemInformationFactory
	{
		#region Methods

		ISystemInformation CreateConfirmation(string heading, string information);
		ISystemInformation CreateException(string heading, string information, IEnumerable<string> detailedInformation);

		#endregion
	}
}