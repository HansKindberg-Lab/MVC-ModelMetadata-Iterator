using System;
using System.Collections.Generic;

namespace WebApplication.Business.Web.Mvc
{
	public interface IModelMetadataProvider
	{
		#region Methods

		IEnumerable<IModelMetadata> GetMetadataForProperties(object container, Type containerType);
		IModelMetadata GetMetadataForProperty(Func<object> modelAccessor, Type containerType, string propertyName);
		IModelMetadata GetMetadataForType(Func<object> modelAccessor, Type modelType);

		#endregion
	}
}