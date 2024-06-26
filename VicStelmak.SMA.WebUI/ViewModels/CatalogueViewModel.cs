using VicStelmak.SMA.WebUI.Product.Dtos;

namespace VicStelmak.SMA.WebUI.ViewModels
{
    public class CatalogueViewModel
    {
        public int SequenceNumber { get; set; } 

        public List<ProductDto> Products { get; set; } = new List<ProductDto>();
    }
}
