using System;
using System.Web.Mvc;
using WebApplication.Models;

namespace WebApplication.Controllers
{
	public abstract class SiteController : Controller
	{
		#region Constructors

		protected SiteController(IModelFactory modelFactory)
		{
			if(modelFactory == null)
				throw new ArgumentNullException(nameof(modelFactory));

			this.ModelFactory = modelFactory;
		}

		#endregion

		#region Properties

		protected internal virtual IModelFactory ModelFactory { get; }

		#endregion
	}
}