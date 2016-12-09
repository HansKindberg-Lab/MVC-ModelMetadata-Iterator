using System.Collections.Generic;
using WebApplication.Business.Collections.Generic.Extensions;

namespace WebApplication.Business
{
	public class SystemInformationFactory : ISystemInformationFactory
	{
		#region Methods

		protected internal virtual ISystemInformation Create(string heading, string information, SystemInformationType type)
		{
			return this.Create(null, heading, new[] {information}, type);
		}

		protected internal virtual ISystemInformation Create(IEnumerable<string> detailedInformation, string heading, IEnumerable<string> information, SystemInformationType type)
		{
			var systemInformation = new SystemInformation
			{
				Heading = heading,
				Type = type
			};

			systemInformation.DetailedInformation.AddRange(detailedInformation);
			systemInformation.Information.AddRange(information);

			return systemInformation;
		}

		public virtual ISystemInformation CreateConfirmation(string heading, string information)
		{
			return this.Create(heading, information, SystemInformationType.Confirmation);
		}

		public virtual ISystemInformation CreateException(string heading, string information, IEnumerable<string> detailedInformation)
		{
			return this.Create(detailedInformation, heading, new[] {information}, SystemInformationType.Exception);
		}

		#endregion
	}
}