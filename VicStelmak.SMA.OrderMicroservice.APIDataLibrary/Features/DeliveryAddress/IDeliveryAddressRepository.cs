using VicStelmak.SMA.OrderMicroservice.APIDataLibrary.Domain.Models;

namespace VicStelmak.SMA.OrderMicroservice.APIDataLibrary.Features.DeliveryAddress
{
    internal interface IDeliveryAddressRepository
    {
        Task<DeliveryAddressModel> GetDeliveryAddressByOrderIdAsync(int orderId);
        Task UpdateDeliveryAddress(DeliveryAddressModel address);
    }
}