namespace VicStelmak.Sma.ProductMicroservice.Application.Dtos
{
    public record UpdateProductDto(
    int AmountInStock,
    string UpdatedBy,
    string Description,
    string ImageUri,
    string Name,
    decimal Price);
}

