using System.Web;
using WebApplication.Business.Web.Html;

namespace WebApplication.Business.Web.Mvc.Forms
{
	public interface IFormComponentFactory
	{
		#region Methods

		IFormComponent CreateFormComponent(IHtmlString html, IModelMetadata modelMetadata);
		IHtmlContainer CreateFormComponents(IHtmlString html, IModelMetadata modelMetadata);

		#endregion
	}
}