using System.Diagnostics.CodeAnalysis;

namespace WebApplication.Business.Web
{
	public interface IHttpEncoder
	{
		#region Methods

		string HtmlDecode(string value);
		string HtmlEncode(string value);

		[SuppressMessage("Microsoft.Design", "CA1055:UriReturnValuesShouldNotBeStrings")]
		string UrlDecode(string value);

		[SuppressMessage("Microsoft.Design", "CA1055:UriReturnValuesShouldNotBeStrings")]
		string UrlEncode(string value);

		#endregion
	}
}