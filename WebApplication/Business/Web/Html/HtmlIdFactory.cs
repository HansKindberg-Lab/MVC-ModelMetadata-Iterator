using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace WebApplication.Business.Web.Html
{
	public class HtmlIdFactory : IHtmlIdFactory
	{
		#region Fields

		private const char _uppercaseLettersSeparator = '-';

		#endregion

		#region Properties

		protected internal virtual char UppercaseLettersSeparator => _uppercaseLettersSeparator;

		#endregion

		#region Methods

		public virtual string Create(string value)
		{
			return this.SeparateOnUppercaseLetters(value);
		}

		[SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase")]
		protected internal virtual string SeparateOnUppercaseLetters(string value)
		{
			if(value == null)
				return null;

			var separatedValue = string.Empty;

			foreach(var character in value)
			{
				if(char.IsUpper(character) && !string.IsNullOrEmpty(separatedValue) && separatedValue.Last() != this.UppercaseLettersSeparator)
					separatedValue += this.UppercaseLettersSeparator.ToString();

				separatedValue += character;
			}

			return separatedValue.ToLowerInvariant();
		}

		#endregion
	}
}