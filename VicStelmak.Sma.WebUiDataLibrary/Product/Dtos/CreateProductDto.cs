namespace VicStelmak.Sma.WebUiDataLibrary.Product.Dtos
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
