namespace VicStelmak.Sma.WebUiDataLibrary.Product.Dtos
{
    public record UpdateProductDto(
     int AmountInStock,
     string UpdatedBy,
     string Description,
     string ImageUri,
     string Name,
     decimal Price);
}
