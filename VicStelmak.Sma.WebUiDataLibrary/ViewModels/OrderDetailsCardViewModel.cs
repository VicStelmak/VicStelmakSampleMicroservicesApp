using VicStelmak.Sma.WebUiDataLibrary.Order.Responses;
using VicStelmak.Sma.WebUiDataLibrary.Product.Dtos;

namespace VicStelmak.Sma.WebUiDataLibrary.ViewModels
{
    public class OrderDetailsCardViewModel
    {
        public OrderDetailsCardViewModel()
        {
            
        }
        public OrderDetailsCardViewModel(GetOrderResponse order)
        {
            Apartment = order.Apartment;
            Building = order.Building;
            City = order.City;
            CreatedAt = order.CreatedAt;
            CreatedBy = order.CreatedBy;
            OrderCode = order.OrderCode;
            PostalCode = order.PostalCode;
            QuantityOfProducts = order.QuantityOfProducts;
            Status = order.Status;
            Street = order.Street;
            Total = order.Total;
            UpdatedAt = order.UpdatedAt;
            UpdatedBy = order.UpdatedBy;
        }

        public int Apartment { get; set; }

        public string Building { get; set; }

        public string City { get; set; }

        public DateTime CreatedAt { get; set; }

        public string CreatedBy { get; set; }

        public List<GetLineItemsResponse> LineItems { get; set; } = new();

        public string OrderCode { get; set; }

        public string PostalCode { get; set; }

        public List<ProductDto> Products { get; set; } = new();

        public int QuantityOfProducts { get; set; }

        public string Status { get; set; }

        public string Street { get; set; }

        public decimal Total { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public string? UpdatedBy { get; set; }
    }
}
