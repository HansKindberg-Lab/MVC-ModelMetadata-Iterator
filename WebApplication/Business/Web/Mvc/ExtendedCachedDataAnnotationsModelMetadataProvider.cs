using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace WebApplication.Business.Web.Mvc
{
	public class ExtendedCachedDataAnnotationsModelMetadataProvider : CachedDataAnnotationsModelMetadataProvider, IModelMetadataProvider
	{
		#region Methods

		protected override CachedDataAnnotationsModelMetadata CreateMetadataFromPrototype(CachedDataAnnotationsModelMetadata prototype, Func<object> modelAccessor)
		{
			return new ExtendedCachedDataAnnotationsModelMetadata(prototype, modelAccessor);
		}

		protected override CachedDataAnnotationsModelMetadata CreateMetadataPrototype(IEnumerable<Attribute> attributes, Type containerType, Type modelType, string propertyName)
		{
			return new ExtendedCachedDataAnnotationsModelMetadata(this, containerType, modelType, propertyName, attributes);
		}

		IEnumerable<IModelMetadata> IModelMetadataProvider.GetMetadataForProperties(object container, Type containerType)
		{
			return this.GetMetadataForProperties(container, containerType).Cast<IModelMetadata>();
		}

		IModelMetadata IModelMetadataProvider.GetMetadataForProperty(Func<object> modelAccessor, Type containerType, string propertyName)
		{
			return (IModelMetadata) this.GetMetadataForProperty(modelAccessor, containerType, propertyName);
		}

		IModelMetadata IModelMetadataProvider.GetMetadataForType(Func<object> modelAccessor, Type modelType)
		{
			return (IModelMetadata) this.GetMetadataForType(modelAccessor, modelType);
		}

		#endregion
	}
}