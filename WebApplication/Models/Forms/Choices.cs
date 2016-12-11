using System.ComponentModel.DataAnnotations;

namespace WebApplication.Models.Forms
{
	public class Choices
	{
		#region Properties

		[Display(Name = "Bl�", Order = 2)]
		public virtual bool Blue { get; set; }

		[Display(Name = "Gr�n", Order = 3)]
		public virtual bool Green { get; set; }

		[Display(Name = "R�d", Order = 1)]
		public virtual bool Red { get; set; }

		#endregion
	}
}