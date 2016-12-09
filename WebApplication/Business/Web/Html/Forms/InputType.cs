using System.ComponentModel.DataAnnotations;

namespace WebApplication.Business.Web.Html.Forms
{
	public enum InputType
	{
		[Display(Name = "button")] Button,
		[Display(Name = "checkbox")] CheckBox,
		[Display(Name = "color")] Color,
		[Display(Name = "date")] Date,
		[Display(Name = "datetime-local")] DateTimeLocal,
		[Display(Name = "email")] Email,
		[Display(Name = "file")] File,
		[Display(Name = "hidden")] Hidden,
		[Display(Name = "image")] Image,
		[Display(Name = "month")] Month,
		[Display(Name = "number")] Number,
		[Display(Name = "password")] Password,
		[Display(Name = "radio")] Radio,
		[Display(Name = "range")] Range,
		[Display(Name = "reset")] Reset,
		[Display(Name = "search")] Search,
		[Display(Name = "submit")] Submit,
		[Display(Name = "tel")] Telephone,
		[Display(Name = "text")] Text,
		[Display(Name = "time")] Time,
		[Display(Name = "url")] Url,
		[Display(Name = "week")] Week
	}
}