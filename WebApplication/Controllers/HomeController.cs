using System.Web.Mvc;
using WebApplication.Models.Forms;
using WebApplication.Models.ViewModels;

namespace WebApplication.Controllers
{
	public class HomeController : Controller
	{
		#region Methods

		protected internal virtual ActionResult CreateView()
		{
			var model = new HomeViewModel();

			return this.View(model);
		}

		public virtual ActionResult Index()
		{
			return this.CreateView();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public virtual ActionResult Index(Form form)
		{
			var view = (ViewResult) this.CreateView();

			var model = (HomeViewModel) view.Model;

			model.Form = form;

			return view;
		}

		#endregion
	}
}