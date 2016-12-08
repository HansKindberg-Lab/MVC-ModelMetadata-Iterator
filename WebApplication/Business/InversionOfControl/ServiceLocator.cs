using System;
using System.Collections.Generic;
using WebApplication.Business.Web;
using WebApplication.Business.Web.Html;
using WebApplication.Business.Web.Mvc.Forms;
using WebApplication.Business.Web.Mvc.Html;

namespace WebApplication.Business.InversionOfControl
{
	[CLSCompliant(false)]
	public class ServiceLocator : IServiceLocator
	{
		#region Fields

		private IFormComponentFactory _formComponentFactory;
		private IParser<IEnumerable<IHtmlComponent>> _htmlParser;
		private static volatile IServiceLocator _instance;
		private static readonly object _lockObject = new object();

		#endregion

		#region Properties

		protected internal virtual IFormComponentFactory FormComponentFactory => this._formComponentFactory ?? (this._formComponentFactory = new FormComponentFactory(this.HtmlIdFactory, this.HtmlParser, this.HttpEncoder));
		protected internal virtual IHtmlDocumentFactory HtmlDocumentFactory { get; } = new HtmlDocumentFactory();
		protected internal virtual IHtmlIdFactory HtmlIdFactory { get; } = new HtmlIdFactory();
		protected internal virtual IParser<IEnumerable<IHtmlComponent>> HtmlParser => this._htmlParser ?? (this._htmlParser = new HtmlParser(this.HtmlDocumentFactory, this.HttpEncoder));
		protected internal virtual IHttpEncoder HttpEncoder { get; } = new HttpEncoder();

		public static IServiceLocator Instance
		{
			get
			{
				// ReSharper disable InvertIf
				if(_instance == null)
				{
					lock(_lockObject)
					{
						if(_instance == null)
							_instance = new ServiceLocator();
					}
				}
				// ReSharper restore InvertIf

				return _instance;
			}
			set
			{
				if(value == _instance)
					return;

				lock(_lockObject)
				{
					_instance = value;
				}
			}
		}

		#endregion

		#region Methods

		public virtual T GetService<T>()
		{
			if(typeof(T) == typeof(IFormComponentFactory))
				return (T) this.FormComponentFactory;

			return default(T);
		}

		#endregion
	}
}