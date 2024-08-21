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

        public Task AddLineItemToOrder(LineItemModel item) => _dbAccess.SaveDataAsync("spordercontent_addlineitem", new
        {
            arg_order_id = item.OrderId,
            arg_product_id = item.ProductId,
            arg_quantity = item.Quantity
        });

        public Task CreateOrder(DeliveryAddressModel address, OrderModel order, int productId) => _dbAccess.SaveDataAsync("sporders_addorder", new
        {
            arg_apartment = address.Apartment,
            arg_building = address.Building,
            arg_city = address.City,
            arg_created_by = order.CreatedBy,
            arg_order_code = order.OrderCode,
            arg_postal_code = address.PostalCode,
            arg_product_id = productId,
            arg_quantity_of_products = order.QuantityOfProducts,
            arg_status = order.Status,
            arg_street = address.Street,
            arg_total = order.Total
        });

        public Task DeleteOrder(int orderId) => _dbAccess.SaveDataAsync("sporders_deleteorder", new { arg_id = orderId });

        public async Task<OrderModel> FindOrderByUserEmailAsync(string orderStatus, string userEmail)
        {
            var orders = await _dbAccess.LoadDataAsync<OrderModel, dynamic>("SELECT * FROM funcorders_findorderbyuseremail(:arg_email,:arg_status)",
                new { arg_email = userEmail, arg_status = orderStatus });

            return orders.FirstOrDefault();
        }

        public async Task<List<LineItemModel>> GetLineItemsByOrderIdAsync(int orderId) =>
            await _dbAccess.LoadDataAsync<LineItemModel, dynamic>("SELECT * FROM funcordercontent_getlineitemsbyorderid(:arg_order_id)",
                new { arg_order_id = orderId });

        public async Task<OrderModel> GetOrderByIdAsync(int orderId, string orderStatus)
        {
            var getOrderResult = await _dbAccess.LoadDataAsync<OrderModel, dynamic>("SELECT * FROM funcorders_getorderbyid(:arg_id, :arg_status)",
                new { arg_id = orderId, arg_status = orderStatus });

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
