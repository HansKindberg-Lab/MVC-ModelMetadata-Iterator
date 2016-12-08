using System;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using WebApplication.Business.InversionOfControl;
using WebApplication.Business.Web.Mvc.Forms;

namespace WebApplication.Business.Web.Mvc.Extensions
{
	public static class HtmlHelperExtension
	{
		#region Fields

		private static volatile IFormComponentFactory _formComponentFactory;
		private static readonly object _lockObject = new object();

		#endregion

		#region Properties

		public static IFormComponentFactory FormComponentFactory
		{
			get
			{
				if(_formComponentFactory == null)
				{
					lock(_lockObject)
					{
						if(_formComponentFactory == null)
							_formComponentFactory = ServiceLocator.Instance.GetService<IFormComponentFactory>();
					}
				}

				return _formComponentFactory;
			}
			set
			{
				if(value == _formComponentFactory)
					return;

				lock(_lockObject)
				{
					_formComponentFactory = value;
				}
			}
		}

		#endregion

		#region Methods

		private static IHtmlString EditorInternal(this HtmlHelper htmlHelper, IHtmlString html)
		{
			if(htmlHelper == null)
				throw new ArgumentNullException(nameof(htmlHelper));

			var container = FormComponentFactory.CreateFormComponents(html, (IModelMetadata) htmlHelper.ViewData.ModelMetadata);

			return container.ToHtmlString();
		}

		//public static IHtmlString CreateFormComponents(this HtmlHelper htmlHelper)
		//{
		//	if(htmlHelper == null)
		//		throw new ArgumentNullException(nameof(htmlHelper));

		//	return FormComponentFactory.CreateFormComponents((IModelMetadata) htmlHelper.ViewData.ModelMetadata).ToHtmlString();
		//}
		public static MvcHtmlString ExtendedEditor(this HtmlHelper html, string expression)
		{
			var value = html.Editor(expression);

			return value;
		}

		public static MvcHtmlString ExtendedEditor(this HtmlHelper html, string expression, object additionalViewData)
		{
			var value = html.Editor(expression, additionalViewData);

			return value;
		}

		public static MvcHtmlString ExtendedEditor(this HtmlHelper html, string expression, string templateName)
		{
			var value = html.Editor(expression, templateName);

			return value;
		}

		public static MvcHtmlString ExtendedEditor(this HtmlHelper html, string expression, string templateName, object additionalViewData)
		{
			var value = html.Editor(expression, templateName, additionalViewData);

			return value;
		}

		public static MvcHtmlString ExtendedEditor(this HtmlHelper html, string expression, string templateName, string htmlFieldName)
		{
			var value = html.Editor(expression, templateName, htmlFieldName);

			return value;
		}

		public static MvcHtmlString ExtendedEditor(this HtmlHelper html, string expression, string templateName, string htmlFieldName, object additionalViewData)
		{
			var value = html.Editor(expression, templateName, htmlFieldName, additionalViewData);

			return value;
		}

		public static IHtmlString ExtendedEditorFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression)
		{
			return EditorInternal(htmlHelper, htmlHelper.EditorFor(expression));
		}

		public static IHtmlString ExtendedEditorFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression, object additionalViewData)
		{
			return EditorInternal(htmlHelper, htmlHelper.EditorFor(expression, additionalViewData));
		}

		public static MvcHtmlString ExtendedEditorFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, string templateName)
		{
			var value = html.EditorFor(expression, templateName);

			return value;
		}

		public static MvcHtmlString ExtendedEditorFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, string templateName, object additionalViewData)
		{
			var value = html.EditorFor(expression, templateName, additionalViewData);

			return value;
		}

		public static MvcHtmlString ExtendedEditorFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, string templateName, string htmlFieldName)
		{
			var value = html.EditorFor(expression, templateName, htmlFieldName);

			return value;
		}

		public static MvcHtmlString ExtendedEditorFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, string templateName, string htmlFieldName, object additionalViewData)
		{
			var value = html.EditorFor(expression, templateName, htmlFieldName, additionalViewData);

			return value;
		}

		public static MvcHtmlString ExtendedEditorForModel(this HtmlHelper html)
		{
			var value = html.EditorForModel();

			return value;
		}

		public static MvcHtmlString ExtendedEditorForModel(this HtmlHelper html, object additionalViewData)
		{
			var value = html.EditorForModel(additionalViewData);

			return value;
		}

		public static MvcHtmlString ExtendedEditorForModel(this HtmlHelper html, string templateName)
		{
			var value = html.EditorForModel(templateName);

			return value;
		}

		public static MvcHtmlString ExtendedEditorForModel(this HtmlHelper html, string templateName, object additionalViewData)
		{
			var value = html.EditorForModel(templateName, additionalViewData);

			return value;
		}

		public static MvcHtmlString ExtendedEditorForModel(this HtmlHelper html, string templateName, string htmlFieldName)
		{
			var value = html.EditorForModel(templateName, htmlFieldName);

			return value;
		}

		public static MvcHtmlString ExtendedEditorForModel(this HtmlHelper html, string templateName, string htmlFieldName, object additionalViewData)
		{
			var value = html.EditorForModel(templateName, htmlFieldName, additionalViewData);

			return value;
		}

		#endregion
	}
}