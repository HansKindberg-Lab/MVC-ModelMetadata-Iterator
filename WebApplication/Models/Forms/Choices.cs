using System.ComponentModel.DataAnnotations;

namespace WebApplication.Models.Forms
{
	public class Choices
	{
		#region Properties

		[Display(Name = "Blå", Order = 2)]
		public virtual bool Blue { get; set; }

		[Display(Name = "Grön", Order = 3)]
		public virtual bool Green { get; set; }

		[Display(Name = "Röd", Order = 1)]
		public virtual bool Red { get; set; }

		#endregion
	}
}