using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace WebApplication.Business.Web.Html
{
	public interface IHtmlTag : IHtmlContainer
	{
		#region Properties

		IDictionary<string, string> Attributes { get; }
		string TagName { get; }
		HtmlTextWriterTag TagType { get; }

		#endregion

		#region Methods

		void AddClass(string value);
		void MergeAttribute(string key, string value);
		void MergeAttribute(string key, string value, bool replaceExisting);
		void MergeAttributes<TKey, TValue>(IDictionary<TKey, TValue> attributes);
		void MergeAttributes<TKey, TValue>(IDictionary<TKey, TValue> attributes, bool replaceExisting);
		IHtmlString ToHtmlString(TagRenderMode renderMode);
		string ToString(TagRenderMode renderMode);

		#endregion
	}
}