using MediatR;

namespace VicStelmak.Sma.OrderMicroservice.APIDataLibrary.Features.DeliveryAddress
{
    public record UpdateDeliveryAddressCommand(int orderId, UpdateDeliveryAddressRequest request) : IRequest;

    internal class UpdateDeliveryAddressHandler : IRequestHandler<UpdateDeliveryAddressCommand>
    {
        private readonly IDeliveryAddressRepository _addressRepository;

        public UpdateDeliveryAddressHandler(IDeliveryAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }

        public Task Handle(UpdateDeliveryAddressCommand command, CancellationToken cancellationToken)
        {
            var address = command.request.MapToDeliveryAddress();
            address.OrderId = command.orderId;

            var addressUpdatingResult = _addressRepository.UpdateDeliveryAddress(address);

            return Task.CompletedTask;
        }
    }

    public record UpdateDeliveryAddressRequest(int Apartment, string Building, string City, string PostalCode, string Street);
}
