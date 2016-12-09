using System;
using System.Web;

namespace WebApplication.Business.Web.Html
{
	public abstract class BasicHtmlNode : IHtmlChild, IHtmlNode
	{
		#region Properties

		public virtual IHtmlContainer Parent => this.ParentInternal;
		protected internal virtual IHtmlContainer ParentInternal { get; set; }

		IHtmlContainer IHtmlChild.ParentInternal
		{
			get { return this.ParentInternal; }
			set { this.ParentInternal = value; }
		}

		#endregion

		#region Methods

		protected internal virtual string GetTabs(int indentLevel)
		{
			if(indentLevel < 0)
				throw new ArgumentOutOfRangeException(nameof(indentLevel), "The indent-level cannot be less than zero.");

			var tabs = string.Empty;

			for(var i = 0; i < indentLevel; i++)
			{
				tabs += "\t";
			}

			return tabs;
		}

		public virtual IHtmlString ToHtmlString()
		{
			return this.ToHtmlString(0);
		}

		public abstract IHtmlString ToHtmlString(int indentLevel);

		public override string ToString()
		{
			return this.ToHtmlString().ToHtmlString();
		}

		#endregion
	}
}