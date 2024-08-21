namespace VicStelmak.Sma.WebUiDataLibrary.ViewModels
{
    public class ProductPurchasingViewModel
    {
        public ProductPurchasingViewModel(string email, string productName)
        {
            Email = email;
            ProductName = productName;
        }

        public int AmountToPurchase { get; set; }

        public int Apartment { get; set; }

        public string Building { get; set; }

        public string City { get; set; }

        public string Email { get; set; }

        public string PostalCode { get; set; }

        public string ProductName { get; set; }

        public string Street { get; set; }
    }
}

