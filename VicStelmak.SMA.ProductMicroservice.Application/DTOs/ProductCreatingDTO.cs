namespace VicStelmak.SMA.ProductMicroservice.Application.DTOs
{
    public record ProductCreatingDTO(
        int AmountInStock, 
        int AmountSold,
        string CreatedBy, 
        string Description,
         string ImageUri,
        string Name,
        decimal Price
    );
}
