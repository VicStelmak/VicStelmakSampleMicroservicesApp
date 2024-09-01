using VicStelmak.Sma.WebUiDataLibrary.Product.Dtos;

namespace VicStelmak.Sma.WebUiDataLibrary.ViewModels
{
    public class ProductDetailsInTableViewModel
    {
        public ProductDetailsInTableViewModel()
        {
            
        }

        public ProductDetailsInTableViewModel(ProductDto product, int sequenceNumber)
        {
            AmountInStock = product.AmountInStock;
            AmountSold = product.AmountSold;
            CreatedAt = product.CreatedAt;
            CreatedBy = product.CreatedBy;
            Description = product.Description;
            ImageUri = product.ImageUri;
            Name = product.Name;
            Price = product.Price;
            SequenceNumber = sequenceNumber;
            UpdatedAt = product.UpdatedAt;
            UpdatedBy = product.UpdatedBy;
        }

        public int AmountInStock { get; set; }

        public int AmountSold { get; set; }

        public DateTime CreatedAt { get; set; }

        public string CreatedBy { get; set; }

        public string Description { get; set; }

        public string ImageUri { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public int SequenceNumber { get; set; }

        public DateTime UpdatedAt { get; set; }

        public string UpdatedBy { get; set; }
    }
}
