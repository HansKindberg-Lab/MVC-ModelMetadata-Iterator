﻿using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using WebApplication.Business.Web.Mvc;

namespace WebApplication
{
	public class Global : HttpApplication
	{
		#region Methods

		protected void Application_Start(object sender, EventArgs e)
		{
			ModelMetadataProviders.Current = new ExtendedCachedDataAnnotationsModelMetadataProvider();

			AreaRegistration.RegisterAllAreas();

			this.RegisterRoutes(RouteTable.Routes);
		}

		protected internal virtual void RegisterRoutes(RouteCollection routes)
		{
			//routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute("Default", "{controller}/{action}/{id}", new {controller = "Home", action = "Index", id = UrlParameter.Optional});
		}

		#endregion
	}
}