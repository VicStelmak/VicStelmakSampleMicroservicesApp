namespace VicStelmak.Sma.WebUiDataLibrary.Product.Dtos
{
    public record UpdateProductDto(
     int AmountInStock,
     int AmountSold,
     string UpdatedBy,
     string Description,
     string ImageUri,
     string Name,
     decimal Price);
}
