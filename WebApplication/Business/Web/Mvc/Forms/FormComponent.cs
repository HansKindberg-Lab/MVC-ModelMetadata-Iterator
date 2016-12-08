using System;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.UI;
using WebApplication.Business.Web.Html;
using WebApplication.Business.Web.Html.Extensions;
using WebApplication.Business.Web.Mvc.Html;

namespace WebApplication.Business.Web.Mvc.Forms
{
	public class FormComponent : IFormComponent
	{
		#region Fields

		private Lazy<DataType?> _dataType;
		private IHtmlTag _htmlTag;
		private IHtmlTag _input;
		private IHtmlTag _label;
		private Lazy<string> _name;

		#endregion

		#region Constructors

		public FormComponent(IHtmlIdFactory htmlIdFactory, IHttpEncoder httpEncoder, IModelMetadata modelMetadata)
		{
			if(htmlIdFactory == null)
				throw new ArgumentNullException(nameof(htmlIdFactory));

			if(httpEncoder == null)
				throw new ArgumentNullException(nameof(httpEncoder));

			if(modelMetadata == null)
				throw new ArgumentNullException(nameof(modelMetadata));

			this.HtmlIdFactory = htmlIdFactory;
			this.HttpEncoder = httpEncoder;
			this.ModelMetadata = modelMetadata;
		}

		#endregion

		#region Properties

		public virtual DataType? DataType
		{
			get
			{
				if(this._dataType == null)
				{
					this._dataType = new Lazy<DataType?>(() =>
					{
						DataType dataType;

						if(Enum.TryParse(this.ModelMetadata.DataTypeName, true, out dataType))
							return dataType;

						return null;
					});
				}

				return this._dataType.Value;
			}
			set
			{
				this._dataType = new Lazy<DataType?>(() => value);
				this.ClearState();
			}
		}

		protected internal virtual IHtmlIdFactory HtmlIdFactory { get; }

		public virtual IHtmlTag HtmlTag
		{
			get
			{
				// ReSharper disable InvertIf
				if(this._htmlTag == null)
				{
					var htmlTag = new HtmlTag(HtmlTextWriterTag.Div);

					htmlTag.Children.Add(this.Label);
					htmlTag.Children.Add(this.Input);

					this._htmlTag = htmlTag;
				}
				// ReSharper restore InvertIf

				return this._htmlTag;
			}
			protected internal set { this._htmlTag = value; }
		}

		protected internal virtual IHttpEncoder HttpEncoder { get; }

		public virtual string Id
		{
			get
			{
				if(this.IdInternal == null)
					this.IdInternal = new Lazy<string>(() => this.HtmlIdFactory.Create(this.ModelMetadata.ContainerType?.Name, this.Name, "input"));

				return this.IdInternal.Value;
			}
			set
			{
				this.IdIsExplicitlySet = true;
				this.IdInternal = new Lazy<string>(() => value);
				this.ClearState();
			}
		}

		protected internal virtual Lazy<string> IdInternal { get; set; }
		protected internal virtual bool IdIsExplicitlySet { get; set; }

		public virtual IHtmlTag Input
		{
			get
			{
				if(this._input == null)
				{
					//this._htmlTag = new HtmlTag();
				}

				return this._input;
			}
			protected internal set { this._input = value; }
		}

		public virtual IHtmlTag Label
		{
			get
			{
				// ReSharper disable InvertIf
				if(this._label == null)
				{
					var label = new HtmlTag(HtmlTextWriterTag.Label);

					label.Attributes.Add("for", this.Id);

					label.Children.Add(new HtmlText(this.HttpEncoder)
					{
						Encode = false,
						Value = this.ModelMetadata.GetDisplayName()
					});

					this._label = label;
				}
				// ReSharper restore InvertIf

				return this._label;
			}
			protected internal set { this._label = value; }
		}

		protected internal virtual IModelMetadata ModelMetadata { get; }

		public virtual string Name
		{
			get
			{
				if(this._name == null)
					this._name = new Lazy<string>(() => this.ModelMetadata.PropertyName);

				return this._name.Value;
			}
			set
			{
				this._name = new Lazy<string>(() => value);
				this.ClearState();
			}
		}

		public virtual IHtmlContainer Parent { get; set; }

		#endregion

		#region Methods

		protected internal virtual void ClearState()
		{
			this.HtmlTag = null;

			if(!this.IdIsExplicitlySet)
				this.IdInternal = null;

			this.Input = null;
			this.Label = null;
		}

		public virtual IHtmlString ToHtmlString()
		{
			return this.HtmlTag.ToHtmlString();
		}

		public override string ToString()
		{
			return this.HtmlTag.ToString();
		}

		#endregion
	}
}