using System;
using System.Collections.Generic;
using System.Web.UI;

namespace WebApplication.Business.Web.Html.Forms
{
	public abstract class FormComponent<T> : HtmlTag, IFormComponent<T> where T : IFormComponentInput
	{
		#region Fields

		private IList<IHtmlNode> _children;
		private T _input;
		private IHtmlTag _label;

		#endregion

		#region Constructors

		protected FormComponent(string displayText, IHttpEncoder httpEncoder, string id, string name, bool required, string value) : base(httpEncoder, HtmlTextWriterTag.Div)
		{
			if(string.IsNullOrWhiteSpace(id))
				throw new ArgumentException("The id cannot be null, empty or whitespace.", nameof(id));

			if(string.IsNullOrWhiteSpace(name))
				throw new ArgumentException("The name cannot be null, empty or whitespace.", nameof(name));

			this.DisplayText = displayText;
			this.Id = id;
			this.Name = name;
			this.Required = required;
			this.Value = value;
		}

		#endregion

		#region Properties

		public override IList<IHtmlNode> Children
		{
			get
			{
				// ReSharper disable InvertIf
				if(this._children == null)
				{
					var children = new HtmlComponentCollection<IHtmlNode>(this);

					var inputInput = this.Input as IInput;

					if(inputInput != null && (inputInput.Type == InputType.CheckBox || inputInput.Type == InputType.Radio))
					{
						children.Add(this.Input);
						children.Add(this.Label);
					}
					else
					{
						children.Add(this.Label);
						children.Add(this.Input);
					}

					this._children = children;
				}
				// ReSharper restore InvertIf

				return this._children;
			}
		}

		protected internal virtual string DisplayText { get; }

		//		return this._component;
		//	}
		//}
		protected internal virtual string Id { get; }
		IFormComponentInput IFormComponent.Input => this.Input;

		public virtual T Input
		{
			get
			{
				// ReSharper disable ConvertIfStatementToNullCoalescingExpression
				if(this._input == null)
					this._input = this.CreateInput();
				// ReSharper restore ConvertIfStatementToNullCoalescingExpression

				return this._input;
			}
		}

		public virtual IHtmlTag Label
		{
			get
			{
				// ReSharper disable InvertIf
				if(this._label == null)
				{
					var label = new HtmlTag(this.HttpEncoder, HtmlTextWriterTag.Label);

					label.Attributes.Add("for", this.Id);

					if(!string.IsNullOrEmpty(this.DisplayText))
						label.Children.Add(new HtmlText(this.HttpEncoder) {Value = this.DisplayText});

					this._label = label;
				}
				// ReSharper restore InvertIf

				return this._label;
			}
		}

		protected internal virtual string Name { get; }
		protected internal virtual bool Required { get; }
		protected internal virtual string Value { get; }

		#endregion

		#region Methods

		protected internal virtual T CreateInput()
		{
			var input = this.CreateInputInternal();

			input.SetId(this.Id);
			input.SetName(this.Name);
			input.SetRequired(this.Required);

			return input;
		}

		protected internal abstract T CreateInputInternal();

		#endregion
	}
}