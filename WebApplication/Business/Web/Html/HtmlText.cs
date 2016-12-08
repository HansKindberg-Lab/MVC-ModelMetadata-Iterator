using System;
using System.Web;

namespace WebApplication.Business.Web.Html
{
	public class HtmlText : IHtmlComponent
	{
		#region Fields

		private const bool _defaultEncode = true;
		private bool? _encode;

		#endregion

		#region Constructors

		public HtmlText(IHttpEncoder httpEncoder)
		{
			if(httpEncoder == null)
				throw new ArgumentNullException(nameof(httpEncoder));

			this.HttpEncoder = httpEncoder;
		}

		#endregion

		#region Properties

		protected internal virtual bool DefaultEncode => _defaultEncode;

		public virtual bool Encode
		{
			get
			{
				if(this._encode == null)
					this._encode = this.DefaultEncode;

				return this._encode.Value;
			}
			set { this._encode = value; }
		}

		protected internal virtual IHttpEncoder HttpEncoder { get; }
		public virtual IHtmlContainer Parent { get; set; }
		public virtual string Value { get; set; }

		#endregion

		#region Methods

		public virtual IHtmlString ToHtmlString()
		{
			var value = this.Value ?? string.Empty;

			return new HtmlString(this.Encode ? this.HttpEncoder.HtmlEncode(value) : value);
		}

		public override string ToString()
		{
			return this.ToHtmlString().ToHtmlString();
		}

		#endregion
	}
}