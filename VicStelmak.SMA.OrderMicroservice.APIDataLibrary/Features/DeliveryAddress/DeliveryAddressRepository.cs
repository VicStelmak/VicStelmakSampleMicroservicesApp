using VicStelmak.SMA.OrderMicroservice.APIDataLibrary.Domain.Models;
using VicStelmak.SMA.OrderMicroservice.APIDataLibrary.Infrastructure.DataAccess;

namespace VicStelmak.SMA.OrderMicroservice.APIDataLibrary.Features.DeliveryAddress
{
    internal class DeliveryAddressRepository : IDeliveryAddressRepository
    {
        private readonly ISqlDbAccess _dbAccess;

        public DeliveryAddressRepository(ISqlDbAccess dbAccess)
        {
            _dbAccess = dbAccess;
        }

        public async Task<DeliveryAddressModel> GetDeliveryAddressByOrderIdAsync(int orderId)
        {
            var getAddressResult = await _dbAccess.LoadDataAsync<DeliveryAddressModel, dynamic>("SELECT * FROM funcdeliveryaddresses_getaddressbyid(:arg_id)",
                new { arg_id = orderId });

            return getAddressResult.FirstOrDefault();
        }

        public Task UpdateDeliveryAddress(DeliveryAddressModel address) => _dbAccess.SaveDataAsync("spdeliveryaddresses_updateaddress", new
        {
            arg_id = address.OrderId,
            arg_apartment = address.Apartment,
            arg_building = address.Building,
            arg_city = address.City,
            arg_postal_code = address.PostalCode,
            arg_street = address.Street
        });
    }
}
