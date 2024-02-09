namespace VicStelmak.SMA.ProductMicroservice.Application.DTOs
{
    public record ProductReadingDTO(
        int AmountInStock,
        string Description,
        string ImageUri,
        string Name,
        decimal Price
    );
}


