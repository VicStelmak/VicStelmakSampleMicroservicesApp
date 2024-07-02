namespace VicStelmak.Sma.ProductMicroservice.Application.Dtos
{
    public record CreateProductDto(
    int AmountInStock,
    int AmountSold,
    string CreatedBy,
    string Description,
     string ImageUri,
    string Name,
    decimal Price
    );
}


