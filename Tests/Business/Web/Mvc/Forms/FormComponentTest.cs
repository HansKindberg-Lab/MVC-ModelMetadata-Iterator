using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebApplication.Business.Web;
using WebApplication.Business.Web.Html;
using WebApplication.Business.Web.Mvc;
using WebApplication.Business.Web.Mvc.Forms;

namespace Tests.Business.Web.Mvc.Forms
{
	[TestClass]
	public class FormComponentTest
	{
		#region Methods

		protected internal virtual Mock<IModelMetadata> CreateModelMetadataMock()
		{
			return new Mock<IModelMetadata>();
		}

		[TestMethod]
		public void Name_Test()
		{
			const string propertyName = "TestPropertyName";

			var modelMetadataMock = this.CreateModelMetadataMock();
			modelMetadataMock.Setup(modelMetadata => modelMetadata.PropertyName).Returns(propertyName);

			Assert.AreEqual(propertyName, new FormComponent(Mock.Of<IHtmlIdFactory>(), Mock.Of<IHttpEncoder>(), modelMetadataMock.Object).Name);
		}

		#endregion
	}
}