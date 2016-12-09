using System.Collections.Generic;

namespace WebApplication.Models
{
	public interface IModelFactory
	{
		#region Methods

		T Create<T>();
		T Create<T>(string parameterName, object value);
		T Create<T>(IDictionary<string, object> parameters);

		#endregion
	}
}