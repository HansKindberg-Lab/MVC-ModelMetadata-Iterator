using System;
using System.Web.UI;

namespace WebApplication.Business.Web.Html.Forms
{
	public abstract class BasicInput : HtmlTag, IFormComponentInput
	{
		#region Constructors

		protected BasicInput(IHttpEncoder httpEncoder, HtmlTextWriterTag tag) : base(httpEncoder, tag) {}

		#endregion

		#region Properties

		protected internal virtual string NameAttributeName => HtmlAttributeKey.Name;
		protected internal virtual string ReadOnlyAttributeName => HtmlAttributeKey.ReadOnly;
		protected internal virtual string RequiredAttributeName => HtmlAttributeKey.Required;

		#endregion

		#region Methods

		protected internal virtual void SetBooleanAttribute(string name, bool value)
		{
			if(string.IsNullOrWhiteSpace(name))
				throw new ArgumentException("The name cannot be null, empty or whitespace.", nameof(name));

			if(value)
				this.SetAttribute(name, name);
			else if(this.Attributes.ContainsKey(name))
				this.Attributes.Remove(name);
		}

		public virtual void SetName(string name)
		{
			this.SetName(name, true);
		}

		protected internal virtual void SetName(string name, bool replaceExisting)
		{
			this.SetAttribute(this.NameAttributeName, name, replaceExisting);
		}

		public virtual void SetNameIfNotExist(string name)
		{
			this.SetId(name, false);
		}

		public virtual void SetReadOnly(bool value)
		{
			this.SetBooleanAttribute(this.ReadOnlyAttributeName, value);
		}

		public virtual void SetRequired(bool value)
		{
			this.SetBooleanAttribute(this.RequiredAttributeName, value);
		}

		#endregion
	}
}