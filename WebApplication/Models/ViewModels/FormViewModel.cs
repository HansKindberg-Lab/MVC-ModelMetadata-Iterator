using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using WebApplication.Business;
using WebApplication.Business.Web;
using WebApplication.Business.Web.Html;
using WebApplication.Business.Web.Html.Forms;
using WebApplication.Business.Web.Mvc;
using WebApplication.Models.Forms;

namespace WebApplication.Models.ViewModels
{
	public class FormViewModel : IFormViewModel
	{
		#region Fields

		private IEnumerable<IHtmlNode> _htmlNodes;
		private Lazy<IModelMetadata> _modelMetadata;
		private Lazy<ISystemInformation> _systemInformation;

		#endregion

		#region Constructors

		public FormViewModel(Form form, IHtmlIdFactory htmlIdFactory, IHttpEncoder httpEncoder, IModelMetadataProvider modelMetadataProvider, bool posted, ISystemInformationFactory systemInformationFactory, IDictionary<string, IEnumerable<string>> validationErrors)
		{
			if(form == null)
				throw new ArgumentNullException(nameof(form));

			if(htmlIdFactory == null)
				throw new ArgumentNullException(nameof(htmlIdFactory));

			if(httpEncoder == null)
				throw new ArgumentNullException(nameof(httpEncoder));

			if(modelMetadataProvider == null)
				throw new ArgumentNullException(nameof(modelMetadataProvider));

			if(systemInformationFactory == null)
				throw new ArgumentNullException(nameof(systemInformationFactory));

			if(validationErrors == null)
				throw new ArgumentNullException(nameof(validationErrors));

			this.Form = form;
			this.HtmlIdFactory = htmlIdFactory;
			this.HttpEncoder = httpEncoder;
			this.ModelMetadataProvider = modelMetadataProvider;
			this.Posted = posted;
			this.SystemInformationFactory = systemInformationFactory;
			this.ValidationErrors = validationErrors;
		}

		#endregion

		#region Properties

		public virtual Form Form { get; }
		protected internal virtual IHtmlIdFactory HtmlIdFactory { get; }

		public virtual IEnumerable<IHtmlNode> HtmlNodes
		{
			get
			{
				// ReSharper disable InvertIf
				if(this._htmlNodes == null)
				{
					var htmlNodes = new List<IHtmlNode>();

					foreach(var property in this.ModelMetadata.Properties.OrderBy(property => property.Order))
					{
						switch(property.PropertyName)
						{
							case "Name":
								htmlNodes.Add(this.CreateTextInputComponent(this.Form.Name, property));
								break;
							case "SwedishCharactersInput":
								htmlNodes.Add(this.CreateTextInputComponent(this.Form.SwedishCharactersInput, property));
								break;
							case "SwedishCharactersTextArea":
								htmlNodes.Add(this.CreateTextInputComponent(this.Form.SwedishCharactersTextArea, property));
								break;
							default:
								break;
						}
					}

					this._htmlNodes = htmlNodes.ToArray();
				}
				// ReSharper restore InvertIf

				return this._htmlNodes;
			}
		}

		protected internal virtual IHttpEncoder HttpEncoder { get; }

		public virtual IModelMetadata ModelMetadata
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
						if(!this.Posted)
							return null;

						// ReSharper disable ConvertIfStatementToReturnStatement
						if(this.ValidationErrors.Any())
							return this.SystemInformationFactory.CreateException("Fel", "Inmatningsfel", this.ValidationErrors.Values.SelectMany(error => error));
						// ReSharper restore ConvertIfStatementToReturnStatement

						return this.SystemInformationFactory.CreateConfirmation("Bekräftelse", "Formuläret är postat.");
					});
				}

				return this._systemInformation.Value;
			}
		}

		protected internal virtual ISystemInformationFactory SystemInformationFactory { get; }
		protected internal virtual IDictionary<string, IEnumerable<string>> ValidationErrors { get; }

		#endregion

		#region Methods

		protected internal virtual IFormComponent<IFormComponentInput> CreateTextInputComponent(string text, IModelMetadata modelMetadata)
		{
			if(modelMetadata == null)
				throw new ArgumentNullException(nameof(modelMetadata));

			IFormComponent<IFormComponentInput> textInputComponent;

			var id = this.HtmlIdFactory.Create(modelMetadata.ContainerType.Name + modelMetadata.PropertyName);

			var dataType = modelMetadata.GetDataType();

			if(dataType != null && dataType.Value == DataType.MultilineText)
				textInputComponent = new TextAreaComponent(modelMetadata.GetDisplayName(), this.HttpEncoder, id, modelMetadata.PropertyName, modelMetadata.IsRequired, text);
			else
				textInputComponent = new InputComponent(modelMetadata.GetDisplayName(), this.HttpEncoder, id, modelMetadata.PropertyName, modelMetadata.IsRequired, InputType.Text, text);

			return textInputComponent;
		}

		#endregion
	}
}