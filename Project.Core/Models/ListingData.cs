using System;
using System.Collections.Generic;
using System.Linq;
using AutoLinq.Core.DTOs;
using AutoLinq.Core.Utilities;

namespace AutoLinq.Core.Models
{
    public class ListingData: BaseModel
    {
        public int Id { get; set; }
        public DateTime PostDate { get; set; }
        public string PostContent { get; set; }
        public string PostTitle { get; set; }
        public string PostExerpt { get; set; }
	    public string PostStatus { get; set; }
	    public string Postname { get; set; }
	    public DateTime PostModified { get; set; }
	    public string Guid { get; set; }
	    public int CarSold { get; set; }
	    public int HeaderImage { get; set; }
	    public string ListingOptions { get; set; }
	    public string GalleryImages { get; set; }
	    public string MultiOptions { get; set; }
	    public string SecondaryTitle { get; set; }
	    public string Year { get; set; }
	    public string Make { get; set; }
	    public string Model { get; set; }
	    public string BodyStyle { get; set; }
	    public string Mileage { get; set; }
	    public string Transmission { get; set; }
	    public string Condition { get; set; }
	    public string Location { get; set; }
	    public int Price { get; set; }
	    public string Drivetrain { get; set; }
	    public string Engine { get; set; }
	    public string ExteriorColor { get; set; }
	    public string InteriorColor { get; set; }
	    public string Mpg { get; set; }

	    //public BackgroundDisplayDTO DisplayDto =>
		//	new BackgroundDisplayDTO
		//    {
		//	    Id = this.Id,
		//	    Name = this.Name,
		//	    Description = this.Description,
		//	    ImageUrl = this.ImageUrl + "?random=" + new Random().Next(),
		//	    ThumbnailUrl = this.ThumbnailUrl + "?random=" + new Random().Next(),
		//	    ModifiedBy = this.ModifiedBy,
		//	    ModifiedOn = this.ModifiedOn,
		//    };

	 //   public SelectListDTO SelectListItem =>
		//    new SelectListDTO
		//	{
		//	    Value = Id.ToString(),
		//	    Text = Name
		//    };
	}
}
