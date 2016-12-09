using System.ComponentModel.DataAnnotations;

namespace WebApplication.Models.Forms
{
	public class Form
	{
		#region Properties

		[Display(Name = "Ditt för- och efternamn", Order = 1, Prompt = "Skriv in ditt för- och efternamn")]
		[Required(ErrorMessage = "\"Ditt för- och efternamn\" är obligatoriskt.")]
		public virtual string Name { get; set; }

		#endregion
	}
}