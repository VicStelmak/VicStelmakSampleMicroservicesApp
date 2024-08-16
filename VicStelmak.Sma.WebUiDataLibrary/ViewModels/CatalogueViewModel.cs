using VicStelmak.Sma.WebUiDataLibrary.Product.Dtos;

namespace VicStelmak.Sma.WebUiDataLibrary.ViewModels
{
    public class CatalogueViewModel
    {
        public int SequenceNumber { get; set; } 

        public List<ProductDto> Products { get; set; } = new List<ProductDto>();
    }
}
