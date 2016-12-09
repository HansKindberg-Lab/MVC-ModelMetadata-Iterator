using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Web.UI;

namespace WebApplication.Business.Web.Html.Forms
{
	public class Input : BasicInput, IInput
	{
		#region Fields

		private IDictionary<string, object> _attributes;
		private const bool _defaultSelfClosingWhenNoChildren = true;
		private static readonly IDictionary<InputType, string> _inputTypeCache = new Dictionary<InputType, string>();
		private static readonly object _inputTypeCacheLock = new object();
		private string _typeAttributeValue;

		#endregion

		#region Constructors

		public Input(IHttpEncoder httpEncoder, InputType type) : base(httpEncoder, HtmlTextWriterTag.Input)
		{
			this.Type = type;
		}

		#endregion

		#region Properties

		public override IDictionary<string, object> Attributes => this._attributes ?? (this._attributes = new SortedDictionary<string, object>(StringComparer.OrdinalIgnoreCase) {{this.TypeAttributeName, this.TypeAttributeValue}});
		protected internal override bool DefaultSelfClosingWhenNoChildren => _defaultSelfClosingWhenNoChildren;
		protected internal virtual IDictionary<InputType, string> InputTypeCache => _inputTypeCache;
		protected internal virtual object InputTypeCacheLock => _inputTypeCacheLock;
		public virtual InputType Type { get; }
		protected internal virtual string TypeAttributeName => HtmlAttributeKey.Type;

		[SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods")]
		protected internal virtual string TypeAttributeValue => this._typeAttributeValue ?? (this._typeAttributeValue = this.GetTypeAttributeValue(this.Type));

		#endregion

		#region Methods

		protected internal virtual string GetTypeAttributeValue(InputType inputType)
		{
			string typeAttributeValue;

			// ReSharper disable InvertIf
			if(!this.InputTypeCache.TryGetValue(inputType, out typeAttributeValue))
			{
				lock(this.InputTypeCacheLock)
				{
					if(!this.InputTypeCache.TryGetValue(inputType, out typeAttributeValue))
					{
						var type = typeof(InputType);
						var member = type.GetMember(inputType.ToString());
						var attributes = member.First().GetCustomAttributes(typeof(DisplayAttribute), false);
						typeAttributeValue = ((DisplayAttribute) attributes.First()).Name;

						this.InputTypeCache[inputType] = typeAttributeValue;
					}
				}
			}
			// ReSharper restore InvertIf

			return typeAttributeValue;
		}

		#endregion
	}
}