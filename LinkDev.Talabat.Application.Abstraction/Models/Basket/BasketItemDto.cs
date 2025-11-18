using System.ComponentModel.DataAnnotations;

namespace LinkDev.Talabat.Application.Abstraction.Models.Basket
{
    public record BasketItemDto
    {
        [Required]
        public int Id { get; set; }


        [Required]
        public required string ProductName { get; set; }
        public string? PictureUrl { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Quantity Must Be At Least 1")]
        public int Quantity { get; set; }


        [Range(.0, double.MaxValue , ErrorMessage ="Price Must Be Greater Than 0" )]
        public decimal Price { get; set; }
        public string? Brand { get; set; }
        public string? Category { get; set; }
    }
}
