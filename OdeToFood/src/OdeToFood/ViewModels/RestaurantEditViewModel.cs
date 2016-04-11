using OdeToFood.Entities;
using System.ComponentModel.DataAnnotations;

namespace OdeToFood.ViewModels
{
    public class RestaurantEditViewModel
    {
        [Required, Display(Name = "Restaurant Name:"), MaxLength(80)]
        public string Name { get; set; }
        [Display(Name = "Cuisine Type:")]
        public CuisineType Cuisine { get; set; }
    }
}
