using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace WebApplication.Models.Forms
{
	public class Form
	{
		#region Fields

		private string _swedishCharactersInput;
		private string _swedishCharactersTextArea;

		#endregion

		#region Properties

		[AdditionalMetadata("First-key", "First-value")]
		[AdditionalMetadata("Second-key", "Second-value")]
		[Display(Name = "Ditt för- och efternamn", Order = 1, Prompt = "Skriv in ditt för- och efternamn")]
		[Required(ErrorMessage = "\"Ditt för- och efternamn\" är obligatoriskt.")]
		[StringLength(255)]
		public virtual string Name { get; set; }

		[Display(Name = "Svenska tecken (input)", Order = 10)]
		public virtual string SwedishCharactersInput
		{
			get { return this._swedishCharactersInput ?? (this._swedishCharactersInput = "å, ä, ö, Å, Ä och Ö"); }
			set { this._swedishCharactersInput = value; }
		}

		[Display(Name = "Svenska tecken (textarea)", Order = 11)]
		[DataType(DataType.MultilineText)]
		public virtual string SwedishCharactersTextArea
		{
			get { return this._swedishCharactersTextArea ?? (this._swedishCharactersTextArea = "å, ä, ö, Å, Ä och Ö"); }
			set { this._swedishCharactersTextArea = value; }
		}

		#endregion
	}
}