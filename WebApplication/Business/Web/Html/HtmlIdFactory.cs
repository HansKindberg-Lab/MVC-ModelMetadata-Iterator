using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace WebApplication.Business.Web.Html
{
	public class HtmlIdFactory : IHtmlIdFactory
	{
		#region Fields

		private const char _idPartSeparator = '-';

		#endregion

		#region Properties

		protected internal virtual char IdPartSeparator => _idPartSeparator;

		#endregion

		#region Methods

		public virtual string Create(IEnumerable<string> parts)
		{
			if(parts == null)
				throw new ArgumentNullException(nameof(parts));

			return this.SeparateOnUppercaseLetters(string.Join(string.Empty, parts));

			//return string.Join(this.IdPartSeparator.ToString(), parts.Select(this.CreateIdPart).Where(part => !string.IsNullOrEmpty(part)));
		}

		[SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase")]
		protected internal virtual string SeparateOnUppercaseLetters(string value)
		{
			if(value == null)
				return null;

			var separatedValue = string.Empty;

			foreach(var character in value)
			{
				if(char.IsUpper(character) && !string.IsNullOrEmpty(separatedValue) && separatedValue.Last() != this.IdPartSeparator)
					separatedValue += this.IdPartSeparator.ToString();

				separatedValue += character;
			}

			return separatedValue.ToLowerInvariant();
		}

		#endregion

		/*
		[SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase")]
		protected internal virtual string CreateIdPart(string name)
		{
			var id = string.Empty;

			foreach (var character in name ?? string.Empty)
			{
				if (char.IsUpper(character) && !string.IsNullOrEmpty(id))
					id += this.IdPartSeparator.ToString();

				id += character;
			}

			return id.ToLowerInvariant();
		}
		*/
	}
}