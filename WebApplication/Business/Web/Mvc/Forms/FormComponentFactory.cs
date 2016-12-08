using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using WebApplication.Business.Web.Html;
using WebApplication.Business.Web.Html.Extensions;
using WebApplication.Business.Web.Mvc.Html;

namespace WebApplication.Business.Web.Mvc.Forms
{
	public class FormComponentFactory : IFormComponentFactory
	{
		#region Fields

		private const string _idAttributeName = "id";

		#endregion

		#region Constructors

		public FormComponentFactory(IHtmlIdFactory htmlIdFactory, IParser<IEnumerable<IHtmlComponent>> htmlParser, IHttpEncoder httpEncoder)
		{
			if(htmlIdFactory == null)
				throw new ArgumentNullException(nameof(htmlIdFactory));

			if(htmlParser == null)
				throw new ArgumentNullException(nameof(htmlParser));

			if(httpEncoder == null)
				throw new ArgumentNullException(nameof(httpEncoder));

			this.HtmlIdFactory = htmlIdFactory;
			this.HtmlParser = htmlParser;
			this.HttpEncoder = httpEncoder;
		}

		#endregion

		#region Properties

		protected internal virtual IHtmlIdFactory HtmlIdFactory { get; }
		protected internal virtual IParser<IEnumerable<IHtmlComponent>> HtmlParser { get; }
		protected internal virtual IHttpEncoder HttpEncoder { get; }
		protected internal virtual string IdAttributeName => _idAttributeName;

		#endregion

		#region Methods

		protected internal virtual IHtmlTag CreateFieldSet(string fieldSetId, string groupName)
		{
			var legend = new HtmlTag(HtmlTextWriterTag.Legend);
			legend.Children.Add(new HtmlText(this.HttpEncoder)
			{
				Encode = false,
				Value = groupName
			});

			var filedset = new HtmlTag(HtmlTextWriterTag.Fieldset);
			filedset.Attributes.Add(this.IdAttributeName, fieldSetId);
			filedset.Children.Add(legend);

			return filedset;
		}

		public virtual IFormComponent CreateFormComponent(IHtmlString html, IModelMetadata modelMetadata)
		{
			if(modelMetadata == null)
				throw new ArgumentNullException(nameof(modelMetadata));

			return new FormComponent(this.HtmlIdFactory, this.HttpEncoder, modelMetadata);
		}

		public virtual IFormComponent CreateFormComponent(IHtmlTag originalHtmlTag, IModelMetadata modelMetadata)
		{
			if(modelMetadata == null)
				throw new ArgumentNullException(nameof(modelMetadata));

			return new FormComponent(this.HtmlIdFactory, this.HttpEncoder, modelMetadata);
		}

		public virtual IHtmlContainer CreateFormComponents(IHtmlString html, IModelMetadata modelMetadata)
		{
			if(modelMetadata == null)
				throw new ArgumentNullException(nameof(modelMetadata));

			var container = new HtmlContainer();

			var htmlValue = html?.ToHtmlString();

			// ReSharper disable InvertIf
			if(!string.IsNullOrEmpty(htmlValue))
			{
				var htmlComponents = this.HtmlParser.Parse(htmlValue).ToArray();

				foreach(var group in modelMetadata.Properties.GroupBy(item => item.GroupName))
				{
					IHtmlContainer componentContainer = container;

					if(!string.IsNullOrWhiteSpace(group.Key))
					{
						var fieldSetId = this.HtmlIdFactory.Create(modelMetadata.ModelType.Name, group.Key, "fieldset");

						var fieldSet = this.GetFieldSet(container, fieldSetId);

						if(fieldSet == null)
						{
							fieldSet = this.CreateFieldSet(fieldSetId, group.Key);
							container.Children.Add(fieldSet);
						}

						componentContainer = fieldSet;
					}

					foreach(var property in group)
					{
						if(property == null)
							throw new InvalidOperationException();

						var inputs = htmlComponents.OfType<IHtmlTag>().SelectMany(htmlTag => htmlTag.Children).OfType<IHtmlTag>();
						var input = inputs.FirstOrDefault(htmlTag => htmlTag.TagType == HtmlTextWriterTag.Input || htmlTag.TagType == HtmlTextWriterTag.Textarea);

						componentContainer.Children.Add(this.CreateFormComponent(input, property));
					}
				}
			}
			// ReSharper restore InvertIf

			return container;
		}

		protected internal virtual IHtmlTag GetFieldSet(IHtmlContainer container, string fieldSetId)
		{
			if(container == null)
				throw new ArgumentNullException(nameof(container));

			return container.Children.OfType<IHtmlTag>().FirstOrDefault(child => child.TagType == HtmlTextWriterTag.Fieldset && child.Attributes.ContainsKey(this.IdAttributeName) && string.Equals(fieldSetId, child.Attributes[this.IdAttributeName], StringComparison.OrdinalIgnoreCase));
		}

		#endregion
	}
}