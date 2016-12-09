using System;
using System.Collections.Generic;
using StructureMap;
using StructureMap.Pipeline;

namespace WebApplication.Models
{
	public class ModelFactory : IModelFactory
	{
		#region Constructors

		public ModelFactory(IContainer container)
		{
			if(container == null)
				throw new ArgumentNullException(nameof(container));

			this.Container = container;
		}

		#endregion

		#region Properties

		protected internal virtual IContainer Container { get; }

		#endregion

		#region Methods

		public virtual T Create<T>()
		{
			return this.Container.GetInstance<T>();
		}

		public virtual T Create<T>(string parameterName, object value)
		{
			return this.Create<T>(new Dictionary<string, object>
			{
				{parameterName, value}
			});
		}

		public virtual T Create<T>(IDictionary<string, object> parameters)
		{
			return this.Container.GetInstance<T>(new ExplicitArguments(parameters));
		}

		#endregion
	}
}