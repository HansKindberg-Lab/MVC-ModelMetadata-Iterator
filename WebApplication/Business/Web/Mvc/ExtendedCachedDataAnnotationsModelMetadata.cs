using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace WebApplication.Business.Web.Mvc
{
	public class ExtendedCachedDataAnnotationsModelMetadata : CachedDataAnnotationsModelMetadata, IModelMetadata
	{
		#region Fields

		private Lazy<string> _groupName;

		#endregion

		#region Constructors

		public ExtendedCachedDataAnnotationsModelMetadata(CachedDataAnnotationsModelMetadata prototype, Func<object> modelAccessor) : base(prototype, modelAccessor) {}
		public ExtendedCachedDataAnnotationsModelMetadata(CachedDataAnnotationsModelMetadataProvider provider, Type containerType, Type modelType, string propertyName, IEnumerable<Attribute> attributes) : base(provider, containerType, modelType, propertyName, attributes) {}

		#endregion

		#region Properties

		IDictionary<string, object> IModelMetadata.AdditionalValues => this.AdditionalValues;

		public virtual string GroupName
		{
			get
			{
				if(this._groupName == null)
					this._groupName = new Lazy<string>(() => this.PrototypeCache?.Display?.GetGroupName());

				return this._groupName.Value;
			}
			set { this._groupName = new Lazy<string>(() => value); }
		}

		IEnumerable<IModelMetadata> IModelMetadata.Properties => this.Properties.Cast<ExtendedCachedDataAnnotationsModelMetadata>();

		#endregion

		#region Methods

		public virtual DataType? GetDataType()
		{
			DataType dataType;

			if(Enum.TryParse(this.DataTypeName, true, out dataType))
				return dataType;

			return null;
		}

		#endregion
	}
}