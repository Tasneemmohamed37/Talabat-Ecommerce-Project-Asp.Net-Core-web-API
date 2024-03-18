using System.ComponentModel.DataAnnotations;

namespace Talabat.APIs.DTOs.Basket
{
    public class BasketItemDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }


        [Required]
        public string PictureURL { get; set; }



        [Required]
        [Range(0.1, double.MaxValue, ErrorMessage = "Price Must be greater than zero!!")] // use double.maxValue becouse defulte dt of range filter
        public decimal Price { get; set; }


        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity Must be at least one item!!")]
        public int Quantity { get; set; }


        [Required]
        public string Product_Brand { get; set; }


        [Required]
        public string Product_Type { get; set; }
    }
}
