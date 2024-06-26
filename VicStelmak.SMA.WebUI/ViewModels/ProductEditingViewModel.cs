using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using VicStelmak.SMA.WebUI.Product.Dtos;

namespace VicStelmak.SMA.WebUI.ViewModels
{
    public class ProductEditingViewModel
    {
        public ProductEditingViewModel(ProductDto product)
        {
            AmountInStock = product.AmountInStock;
            Description = product.Description;
            ImageUri = product.ImageUri;
            Name = product.Name;
            Price = product.Price;
        }

        [DisplayName("Amount")]
        [Range(1, int.MaxValue, ErrorMessage = "Amount can't be equal to zero.")]
        public int AmountInStock { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }

        [StringLength(70)]
        public string ImageUri { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        [Range(typeof(decimal), "0,50", "79228162514264337593543950335", ErrorMessage = "Price can't be equal to zero.")]
        public decimal Price { get; set; }
    }
}
