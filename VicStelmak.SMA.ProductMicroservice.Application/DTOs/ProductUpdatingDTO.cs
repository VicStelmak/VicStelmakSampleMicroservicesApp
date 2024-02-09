namespace VicStelmak.SMA.ProductMicroservice.Application.DTOs
{
    public record ProductUpdatingDTO(
        int AmountInStock,
        int AmountSold,
        string UpdatedBy,
        string Description,
        string ImageUri,
        string Name,
        decimal Price
    );
}
