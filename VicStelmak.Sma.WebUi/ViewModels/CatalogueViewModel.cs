using VicStelmak.Sma.WebUi.Product.Dtos;

namespace VicStelmak.Sma.WebUi.ViewModels
{
    public class CatalogueViewModel
    {
        public int SequenceNumber { get; set; } 

        public List<ProductDto> Products { get; set; } = new List<ProductDto>();
    }
}
