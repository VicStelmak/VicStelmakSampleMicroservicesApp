namespace VicStelmak.SMA.ProductMicroservice.Domain.Models
{
    public class ProductModel
    {
        public int Id { get; set; }

        public int AmountInStock { get; set; }

        public int AmountSold { get; set; }

        // Date and time when product was added to the database
        public DateTime CreatedAt { get; set; }

        // Person who added product to database
        public string CreatedBy { get; set; }

        public string Description { get; set; }

        public string ImageUri { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        //Date and time when product was updated in the database
        public DateTime UpdatedAt { get; set; }

        public string UpdatedBy { get; set; }


       

    }
}
