using System.Collections.Generic;
using System.Web;
using System.Web.UI;

namespace WebApplication.Business.Web.Html
{
	public interface IHtmlTag : IHtmlContainer
	{
		#region Properties

		IDictionary<string, object> Attributes { get; }
		HtmlTextWriterTag Tag { get; }
		string TagName { get; }

		#endregion

		#region Methods

		void AddAttributeIfNotExist(string key, object value);
		void AddAttributesIfNotExist<TKey, TValue>(IDictionary<TKey, TValue> attributes);
		void AddClass(string value);
		void SetAttribute(string key, object value);
		void SetAttributes<TKey, TValue>(IDictionary<TKey, TValue> attributes);
		void SetId(string id);
		void SetIdIfNotExist(string id);
		IHtmlString ToHtmlString(TagRenderMode renderMode);
		IHtmlString ToHtmlString(TagRenderMode renderMode, int indentLevel);

		#endregion
	}
}