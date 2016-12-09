using System;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using System.Web.Mvc;
using StructureMap;
using WebApplication.Business.Configuration;
using WebApplication.Business.InversionOfControl;
using WebApplication.Business.Web.Mvc;
using Registry = StructureMap.Registry;
using StructureMapConfigurationSection = WebApplication.Business.StructureMap.Configuration.ConfigurationSection;
using StructureMapDependencyResolver = WebApplication.Business.StructureMap.DependencyResolver;
using StructureMapServiceLocator = WebApplication.Business.StructureMap.ServiceLocator;

namespace WebApplication.Business.Initialization
{
	public class Initializer
	{
		#region Constructors

		public Initializer(IContainer container, StructureMapConfigurationSection structureMapConfigurationSection)
		{
			if(container == null)
				throw new ArgumentNullException(nameof(container));

			if(structureMapConfigurationSection == null)
				throw new ArgumentNullException(nameof(structureMapConfigurationSection));

			this.Container = container;
			this.StructureMapConfigurationSection = structureMapConfigurationSection;
		}

		#endregion

		#region Properties

		protected internal virtual IContainer Container { get; }
		protected internal virtual StructureMapConfigurationSection StructureMapConfigurationSection { get; }

		#endregion

		#region Methods

		public virtual void Initialize()
		{
			foreach(var registryElement in this.StructureMapConfigurationSection.Registries)
			{
				this.Container.Configure(configurationExpression => { configurationExpression.AddRegistry((Registry) Activator.CreateInstance(Type.GetType(registryElement.Type, true))); });
			}

			ServiceLocator.Instance = new StructureMapServiceLocator(this.Container);

			DependencyResolver.SetResolver(new StructureMapDependencyResolver(this.Container));

			ModelMetadataProviders.Current = (ModelMetadataProvider) this.Container.GetInstance<IModelMetadataProvider>();

			foreach(var applicationConfiguration in this.Container.GetAllInstances<IApplicationConfiguration>())
			{
				applicationConfiguration.Configure();
			}
		}

		[SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
		public static void Start()
		{
			new Initializer(new Container(), (StructureMapConfigurationSection) ConfigurationManager.GetSection("structureMap")).Initialize();
		}

		#endregion
	}
}