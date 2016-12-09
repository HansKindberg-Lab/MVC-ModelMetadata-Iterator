using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication.Business;
using WebApplication.Business.Web.Html;
using WebApplication.Business.Web.Html.Forms;
using WebApplication.Business.Web.Mvc;
using WebApplication.Models.Forms;

namespace WebApplication.Models.ViewModels
{
	public class FormViewModel : IFormViewModel
	{
		#region Fields

		private IEnumerable<IFormComponent> _components;
		private Lazy<IModelMetadata> _modelMetadata;
		private Lazy<ISystemInformation> _systemInformation;

		#endregion

		#region Constructors

		public FormViewModel(Form form, IHtmlIdFactory htmlIdFactory, IModelMetadataProvider modelMetadataProvider, bool posted, ISystemInformationFactory systemInformationFactory, IDictionary<string, IEnumerable<string>> validationErrors)
		{
			if(form == null)
				throw new ArgumentNullException(nameof(form));

			if(htmlIdFactory == null)
				throw new ArgumentNullException(nameof(htmlIdFactory));

			if(modelMetadataProvider == null)
				throw new ArgumentNullException(nameof(modelMetadataProvider));

			if(systemInformationFactory == null)
				throw new ArgumentNullException(nameof(systemInformationFactory));

			if(validationErrors == null)
				throw new ArgumentNullException(nameof(validationErrors));

			this.Form = form;
			this.HtmlIdFactory = htmlIdFactory;
			this.ModelMetadataProvider = modelMetadataProvider;
			this.Posted = posted;
			this.SystemInformationFactory = systemInformationFactory;
			this.ValidationErrors = validationErrors;
		}

		#endregion

		#region Properties

		public virtual IEnumerable<IFormComponent> Components
		{
			get
			{
				// ReSharper disable InvertIf
				if(this._components == null)
				{
					var components = new List<IFormComponent>();

					foreach(var property in this.ModelMetadata.Properties.OrderBy(property => property.Order))
					{
						if(property == null)
							throw new InvalidOperationException();
					}

					this._components = components.ToArray();
				}
				// ReSharper restore InvertIf

				return this._components;
			}
		}

		protected internal virtual Form Form { get; }
		protected internal virtual IHtmlIdFactory HtmlIdFactory { get; }

		protected internal virtual IModelMetadata ModelMetadata
		{
			get
			{
				if(this._modelMetadata == null)
				{
					this._modelMetadata = new Lazy<IModelMetadata>(() =>
					{
						var modelMetadata = this.ModelMetadataProvider.GetMetadataForType(() => this.Form, this.Form.GetType());

						return modelMetadata;
					});
				}

				return this._modelMetadata.Value;
			}
		}

		protected internal virtual IModelMetadataProvider ModelMetadataProvider { get; }
		protected internal virtual bool Posted { get; }

		public virtual ISystemInformation SystemInformation
		{
			get
			{
				if(this._systemInformation == null)
				{
					this._systemInformation = new Lazy<ISystemInformation>(() =>
					{
						if(!this.Posted || !this.ValidationErrors.Any())
							return null;

						var systemInformation = this.SystemInformationFactory.CreateException("Fel", "Inmatningsfel", Enumerable.Empty<string>());

						return systemInformation;
					});
				}

				return this._systemInformation.Value;
			}
		}

		protected internal virtual ISystemInformationFactory SystemInformationFactory { get; }
		protected internal virtual IDictionary<string, IEnumerable<string>> ValidationErrors { get; }

		#endregion

		#region Methods

		protected internal virtual IFormComponent CreateComponent()
		{
			return null;
		}

		#endregion
	}
}