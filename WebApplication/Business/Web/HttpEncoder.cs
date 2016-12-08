using System.Web;

namespace WebApplication.Business.Web
{
	public class HttpEncoder : IHttpEncoder
	{
		#region Methods

		public virtual string HtmlDecode(string value)
		{
			return HttpUtility.HtmlDecode(value);
		}

		public virtual string HtmlEncode(string value)
		{
			return HttpUtility.HtmlEncode(value);
		}

		public virtual string UrlDecode(string value)
		{
			return HttpUtility.UrlDecode(value);
		}

		public virtual string UrlEncode(string value)
		{
			return HttpUtility.UrlEncode(value);
		}

		#endregion
	}
}