using VicStelmak.Sma.OrderMicroservice.APIDataLibrary.Domain.Models;

namespace VicStelmak.Sma.OrderMicroservice.APIDataLibrary.Features.DeliveryAddress
{
    internal interface IDeliveryAddressRepository
    {
        Task<DeliveryAddressModel> GetDeliveryAddressByOrderIdAsync(int orderId);
        Task UpdateDeliveryAddress(DeliveryAddressModel address);
    }
}