using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;

namespace WebApplication.Models.Forms
{
	public class Form
	{
		#region Properties

		//[DataType(DataType.EmailAddress)]
		//[Display(Name = "EmailWithDataTypeAttribute - Name (from DisplayAttribute)", Description = "EmailWithDataTypeAttribute - Description (from DisplayAttribute)", Order = 2, Prompt = "EmailWithDataTypeAttribute - Prompt (from DisplayAttribute)", ShortName = "EmailWithDataTypeAttribute - ShortName (from DisplayAttribute)")]
		//[Required]
		//public virtual string EmailWithDataTypeAttribute { get; set; }

		//[Display(Name = "EmailWithEmailAddressAttribute - Name (from DisplayAttribute)", Description = "EmailWithEmailAddressAttribute - Description (from DisplayAttribute)", Order = 3, Prompt = "EmailWithEmailAddressAttribute - Prompt (from DisplayAttribute)", ShortName = "EmailWithEmailAddressAttribute - ShortName (from DisplayAttribute)")]
		//[EmailAddress]
		//[Required]
		//public virtual string EmailWithEmailAddressAttribute { get; set; }

		//[Display(Name = "Number - Name (from DisplayAttribute)", Description = "Number - Description (from DisplayAttribute)", Order = 4, Prompt = "Number - Prompt (from DisplayAttribute)", ShortName = "Number - ShortName (from DisplayAttribute)")]
		//[Required]
		//public virtual int Number { get; set; }

		//[DataType(DataType.Currency)]
		//[Display(Name = "NumberWithDataTypeAttribute - Name (from DisplayAttribute)", Description = "NumberWithDataTypeAttribute - Description (from DisplayAttribute)", Order = 5, Prompt = "NumberWithDataTypeAttribute - Prompt (from DisplayAttribute)", ShortName = "NumberWithDataTypeAttribute - ShortName (from DisplayAttribute)")]
		//[Required]
		//public virtual int NumberWithDataTypeAttribute { get; set; }

		//[Display(Name = "Text - Name (from DisplayAttribute)", Description = "Text - Description (from DisplayAttribute)", Order = 1, Prompt = "Text - Prompt (from DisplayAttribute)", ShortName = "Text - ShortName (from DisplayAttribute)")]
		//[Required]
		//public virtual string Text { get; set; }



















		




		[Display(GroupName = "first-group", Order = 1)]
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "A")]
		public virtual int A { get; set; }

		[Display(Order = 2)]
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "B")]
		public virtual int B { get; set; }

		[Display(GroupName = "last-group", Order = 3)]
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "C")]
		public virtual int C { get; set; }

		[Display(Order = 4)]
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "D")]
		public virtual int D { get; set; }

	
		[Display(GroupName = "first-group", Order = 5)]
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "E")]
		public virtual int E { get; set; }

		
		[Display(GroupName = "first-group", Order = 6)]
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "F")]
		public virtual int F { get; set; }

		#endregion
	}
}