using VicStelmak.Sma.OrderMicroservice.ApiDataLibrary.Domain.Models;
using VicStelmak.Sma.OrderMicroservice.ApiDataLibrary.Infrastructure.DataAccess;

namespace VicStelmak.Sma.OrderMicroservice.ApiDataLibrary.Features.Order
{
    internal class OrderRepository : IOrderRepository
    {
        private readonly ISqlDbAccess _dbAccess;

        public OrderRepository(ISqlDbAccess dbAccess)
        {
            _dbAccess = dbAccess;
        }

        public Task CreateOrder(DeliveryAddressModel address, OrderModel order) => _dbAccess.SaveDataAsync("sporders_addorder", new
        {
            arg_apartment = address.Apartment,
            arg_building = address.Building,
            arg_city = address.City,
            arg_created_by = order.CreatedBy,
            arg_order_code = order.OrderCode,
            arg_postal_code = address.PostalCode,
            arg_quantity_of_products = order.QuantityOfProducts,
            arg_status = order.Status,
            arg_street = address.Street,
            arg_total = order.Total
        });

        public Task DeleteOrder(int orderId) => _dbAccess.SaveDataAsync("sporders_deleteorder", new { arg_id = orderId });

        public async Task<OrderModel> GetOrderByIdAsync(int orderId)
        {
            var getOrderResult = await _dbAccess.LoadDataAsync<OrderModel, dynamic>("SELECT * FROM funcorders_getorderbyid(:arg_id)",
                new { arg_id = orderId });

            return getOrderResult.FirstOrDefault();
        }

        public async Task<List<OrderModel>> GetOrdersList() =>
            await _dbAccess.LoadDataAsync<OrderModel, dynamic>("SELECT * FROM funcorders_getorders()", new { });

        public Task UpdateOrder(OrderModel order) => _dbAccess.SaveDataAsync("sporders_updateorder", new
        {
            arg_id = order.Id,
            arg_quantity_of_products = order.QuantityOfProducts,
            arg_status = order.Status,
            arg_total = order.Total,
            arg_updated_by = order.UpdatedBy
        });
    }
}
