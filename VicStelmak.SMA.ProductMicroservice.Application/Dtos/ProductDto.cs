namespace VicStelmak.SMA.ProductMicroservice.Application.Dtos
{
    public record ProductDto(
        int Id,
        int AmountInStock,
        int AmountSold,
        DateTime CreatedAt,
        string CreatedBy,
        string Description,
        string ImageUri,
        string Name,
        decimal Price,
        DateTime UpdatedAt,
        string UpdatedBy
    );
}
