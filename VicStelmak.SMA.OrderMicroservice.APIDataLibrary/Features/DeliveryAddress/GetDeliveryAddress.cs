using MediatR;

namespace VicStelmak.Sma.OrderMicroservice.APIDataLibrary.Features.DeliveryAddress
{
    public record GetDeliveryAddressByOrderIdQuery(int orderId) : IRequest<GetDeliveryAddressResponse>;

    internal class GetDeliveryAddressByOrderIdHandler : IRequestHandler<GetDeliveryAddressByOrderIdQuery, GetDeliveryAddressResponse>
    {
        private readonly IDeliveryAddressRepository _addressRepository;

        public GetDeliveryAddressByOrderIdHandler(IDeliveryAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }

        public async Task<GetDeliveryAddressResponse> Handle(GetDeliveryAddressByOrderIdQuery query, CancellationToken cancellationToken)
        {
            var address = await _addressRepository.GetDeliveryAddressByOrderIdAsync(query.orderId);

            if (address != null) return address.MapToGetDeliveryAddressResponse();

            // Unfortunately I had to return null because I require it if I want DeliveryAddressEndpointsConfigurator to give a 404 response instead of exception
            // thrown by DeliveryAddresSmapper because of an attempt to pass null object to DeliveryAddresSmapper. I know it's a bad design practice but that is
            // what Mapster also do in such cases.
            else return null;
        }
    }

    public record GetDeliveryAddressResponse(int Apartment, string Building, string City,  int OrderId, string PostalCode, string Street);
}
