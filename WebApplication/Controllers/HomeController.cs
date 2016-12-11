using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebApplication.Models;
using WebApplication.Models.Forms;
using WebApplication.Models.ViewModels;

namespace WebApplication.Controllers
{
	public class HomeController : SiteController
	{
		#region Fields

		private Form _form;
		private IFormViewModel _formViewModel;
		private IHomeViewModel _homeViewModel;
		private bool? _posted;
		private IDictionary<string, IEnumerable<string>> _validationErrors;

		#endregion

		#region Constructors

		public HomeController(IModelFactory modelFactory) : base(modelFactory) {}

		#endregion

		#region Properties

		protected internal virtual Form Form
		{
			get { return this._form ?? (this._form = new Form()); }
			set { this._form = value; }
		}

		protected internal virtual IFormViewModel FormViewModel
		{
			get
			{
				// ReSharper disable ConvertIfStatementToNullCoalescingExpression
				if(this._formViewModel == null)
				{
					this._formViewModel = this.ModelFactory.Create<IFormViewModel>(new Dictionary<string, object>
					{
						{"form", this.Form},
						{"posted", this.Posted},
						{"validationErrors", this.ValidationErrors}
					});
				}
				// ReSharper restore ConvertIfStatementToNullCoalescingExpression

				return this._formViewModel;
			}
		}

		protected internal virtual IHomeViewModel HomeViewModel => this._homeViewModel ?? (this._homeViewModel = this.ModelFactory.Create<IHomeViewModel>("formViewModel", this.FormViewModel));

		protected internal virtual bool Posted
		{
			get
			{
				if(this._posted == null)
					this._posted = false;

				return this._posted.Value;
			}
			set { this._posted = value; }
		}

		protected internal virtual IDictionary<string, IEnumerable<string>> ValidationErrors
		{
			get
			{
				// ReSharper disable InvertIf
				if(this._validationErrors == null)
				{
					var validationErrors = new Dictionary<string, IEnumerable<string>>(StringComparer.OrdinalIgnoreCase);

					if(!this.ModelState.IsValid)
					{
						foreach(var key in this.ModelState.Keys)
						{
							var errors = this.ModelState[key].Errors.Select(error => error.ErrorMessage).ToArray();

							if(errors.Any())
								validationErrors.Add(key, errors);
						}
					}

					this._validationErrors = validationErrors;
				}
				// ReSharper restore InvertIf

				return this._validationErrors;
			}
		}

		#endregion

		#region Methods

		protected internal virtual ActionResult CreateView()
		{
			return this.View(this.HomeViewModel);
		}

		public virtual ActionResult Index()
		{
			return this.CreateView();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public virtual ActionResult Index(Form form)
		{
			this.Form = form;
			this.Posted = true;

			return this.CreateView();
		}

		#endregion
	}
}