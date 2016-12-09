using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApplication.Business.Web.Html;

namespace Tests.Business.Web.Html
{
	[TestClass]
	public class HtmlIdFactoryTest
	{
		#region Methods

		[TestMethod]
		public void CreateTest()
		{
			Assert.AreEqual("access-violation-exception", new HtmlIdFactory().SeparateOnUppercaseLetters(typeof(AccessViolationException).Name));

			Assert.AreEqual("access-violation-exception-test-property-name", new HtmlIdFactory().SeparateOnUppercaseLetters(typeof(AccessViolationException).Name + "TestPropertyName"));

			Assert.AreEqual("access-violation-exception-test-property-name", new HtmlIdFactory().SeparateOnUppercaseLetters(typeof(AccessViolationException).Name + "-Test-Property-Name"));

			Assert.AreEqual("access-violation-exception-test-property-na-me", new HtmlIdFactory().SeparateOnUppercaseLetters(typeof(AccessViolationException).Name + "-Test-Property-Na-me"));
		}

		#endregion
	}
}