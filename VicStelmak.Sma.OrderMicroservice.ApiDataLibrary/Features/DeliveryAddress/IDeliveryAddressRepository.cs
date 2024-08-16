using VicStelmak.Sma.OrderMicroservice.ApiDataLibrary.Domain.Models;

namespace VicStelmak.Sma.OrderMicroservice.ApiDataLibrary.Features.DeliveryAddress
{
    internal interface IDeliveryAddressRepository
    {
        Task<DeliveryAddressModel> GetDeliveryAddressByOrderIdAsync(int orderId);
        Task UpdateDeliveryAddress(DeliveryAddressModel address);
    }
}