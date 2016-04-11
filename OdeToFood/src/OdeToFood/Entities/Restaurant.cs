using System.ComponentModel.DataAnnotations;

namespace OdeToFood.Entities
{
    public enum CuisineType
    {
        None, 
        Italian,
        French,
        American
    }
    public class Restaurant
    {
        public int Id { get; set; }

        [Required, Display(Name="Restaurant Name:"), MaxLength(80)]
        public string Name { get; set; }
        [Display(Name="Cuisine Type:")]
        public CuisineType Cuisine { get; set; }
    }
}
