using VicStelmak.SMA.OrderMicroservice.APIDataLibrary.Domain.Models;
using VicStelmak.SMA.OrderMicroservice.APIDataLibrary.Features.Order;

namespace VicStelmak.SMA.OrderMicroservice.APIDataLibrary.Features.DeliveryAddress
{
    internal static class DeliveryAddressMapper
    {
        internal static DeliveryAddressModel MapToDeliveryAddress(this CreateOrderRequest request)
        {
            return new DeliveryAddressModel()
            {
                Apartment = request.Apartment,
                Building = request.Building,
                City = request.City,
                PostalCode = request.PostalCode,
                Street = request.Street
            };
        }

        internal static DeliveryAddressModel MapToDeliveryAddress(this UpdateDeliveryAddressRequest request)
        {
            return new DeliveryAddressModel()
            {
                Apartment = request.Apartment,
                Building = request.Building,
                City = request.City,
                PostalCode = request.PostalCode,
                Street = request.Street
            };
        }

        internal static GetDeliveryAddressResponse MapToGetDeliveryAddressResponse(this DeliveryAddressModel address)
        {
            return new GetDeliveryAddressResponse(address.Apartment, address.Building, address.City, address.OrderId, address.PostalCode, address.Street);
        }
    }
}
