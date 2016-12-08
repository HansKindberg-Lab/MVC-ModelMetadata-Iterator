using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Web.Mvc;

namespace WebApplication.Business.Web.Mvc
{
	public interface IModelMetadata
	{
		#region Properties

		IDictionary<string, object> AdditionalValues { get; }
		object Container { get; set; }
		Type ContainerType { get; }
		bool ConvertEmptyStringToNull { get; set; }
		string DataTypeName { get; set; }
		string Description { get; set; }
		string DisplayFormatString { get; set; }

		[SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods")]
		string DisplayName { get; set; }

		string EditFormatString { get; set; }
		string GroupName { get; set; }
		bool HideSurroundingHtml { get; set; }
		bool HtmlEncode { get; set; }
		bool IsComplexType { get; }
		bool IsNullableValueType { get; }
		bool IsReadOnly { get; set; }
		bool IsRequired { get; set; }
		object Model { get; set; }
		Type ModelType { get; }
		string NullDisplayText { get; set; }
		int Order { get; set; }
		IEnumerable<IModelMetadata> Properties { get; }
		string PropertyName { get; }
		bool RequestValidationEnabled { get; set; }
		string ShortDisplayName { get; set; }
		bool ShowForDisplay { get; set; }
		bool ShowForEdit { get; set; }
		string SimpleDisplayText { get; set; }
		string TemplateHint { get; set; }
		string Watermark { get; set; }

		#endregion

		#region Methods

		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		string GetDisplayName();

		IEnumerable<ModelValidator> GetValidators(ControllerContext context);

		#endregion
	}
}