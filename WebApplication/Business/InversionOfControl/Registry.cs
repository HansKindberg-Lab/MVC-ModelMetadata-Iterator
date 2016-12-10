using System;
using System.Diagnostics.CodeAnalysis;
using System.Web;
using StructureMap;
using StructureMap.Web;
using WebApplication.Business.Configuration;
using WebApplication.Business.Web;
using WebApplication.Business.Web.Html;
using WebApplication.Business.Web.Mvc;
using WebApplication.Models;
using WebApplication.Models.ViewModels;

namespace WebApplication.Business.InversionOfControl
{
	public class Registry : global::StructureMap.Registry
	{
		#region Constructors

		public Registry()
		{
			Register(this);
		}

		#endregion

		#region Methods

		[SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling")]
		public static void Register(IProfileRegistry registry)
		{
			if(registry == null)
				throw new ArgumentNullException(nameof(registry));

			// Web
			registry.For<HttpRequest>().HybridHttpOrThreadLocalScoped().Use(() => HttpContext.Current.Request);
			registry.For<HttpRequestBase>().HybridHttpOrThreadLocalScoped().Use<HttpRequestWrapper>();

			registry.For<IApplicationConfiguration>().Singleton().Use<BundleConfiguration>();
			registry.For<IApplicationConfiguration>().Singleton().Use<FilterConfiguration>();
			registry.For<IFormViewModel>().HybridHttpOrThreadLocalScoped().Use<FormViewModel>();
			registry.For<IHomeViewModel>().HybridHttpOrThreadLocalScoped().Use<HomeViewModel>();
			registry.For<IHtmlIdFactory>().Singleton().Use<HtmlIdFactory>();
			registry.For<IHttpEncoder>().Singleton().Use<HttpEncoder>();
			registry.For<IModelFactory>().Singleton().Use<ModelFactory>();
			registry.For<IModelMetadataProvider>().Singleton().Use<ExtendedCachedDataAnnotationsModelMetadataProvider>();
			registry.For<ISystemInformationFactory>().Singleton().Use<SystemInformationFactory>();
		}

		#endregion
	}
}