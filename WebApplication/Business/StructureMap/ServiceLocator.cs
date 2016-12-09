using System;
using System.Collections.Generic;
using System.Linq;
using StructureMap;
using WebApplication.Business.InversionOfControl;

namespace WebApplication.Business.StructureMap
{
	public class ServiceLocator : IServiceLocator
	{
		#region Constructors

		public ServiceLocator(IContainer container)
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

		public virtual T GetService<T>()
		{
			return this.Container.GetInstance<T>();
		}

		public virtual T GetService<T>(string key)
		{
			return this.Container.GetInstance<T>(key);
		}

		public virtual object GetService(Type serviceType)
		{
			return this.Container.GetInstance(serviceType);
		}

		public virtual object GetService(Type serviceType, string key)
		{
			return this.Container.GetInstance(serviceType, key);
		}

		public virtual IEnumerable<T> GetServices<T>()
		{
			return this.Container.GetAllInstances<T>();
		}

		public virtual IEnumerable<object> GetServices(Type serviceType)
		{
			return this.Container.GetAllInstances(serviceType).Cast<object>();
		}

		#endregion
	}
}