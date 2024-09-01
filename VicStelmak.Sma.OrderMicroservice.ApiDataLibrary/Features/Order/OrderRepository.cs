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

        public async Task AddLineItemToOrderAsync(LineItemModel item) => await _dbAccess.SaveDataAsync("spordercontent_addlineitem", new
        {
            arg_order_id = item.OrderId,
            arg_product_id = item.ProductId,
            arg_quantity = item.Quantity
        });

        public async Task CreateOrderAsync(DeliveryAddressModel address, OrderModel order, int productId) => 
            await _dbAccess.SaveDataAsync("sporders_addorder", new
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

        public async Task DeleteOrderAsync(int orderId) => await _dbAccess.SaveDataAsync("sporders_deleteorder", new { arg_id = orderId });

        public async Task<OrderModel> FindPendingOrderByUserEmailAsync(string userEmail)
        {
            var orders = await _dbAccess.LoadDataAsync<OrderModel, dynamic>(
                "SELECT * FROM funcorders_findpendingorderbyuseremail(:arg_email)", new { arg_email = userEmail });

            return orders.FirstOrDefault();
        }

        public async Task<List<LineItemModel>> GetLineItemsByOrderIdAsync(int orderId) =>
            await _dbAccess.LoadDataAsync<LineItemModel, dynamic>("SELECT * FROM funcordercontent_getlineitemsbyorderid(:arg_order_id)",
                new { arg_order_id = orderId });

        public async Task<OrderModel> GetOrderByIdAsync(int orderId)
        {
            var getOrderResult = await _dbAccess.LoadDataAsync<OrderModel, dynamic>("SELECT * FROM funcorders_getorderbyid(:arg_id)",
                new { arg_id = orderId });

            return getOrderResult.FirstOrDefault();
        }

        public async Task<List<OrderModel>> GetOrdersList() =>
            await _dbAccess.LoadDataAsync<OrderModel, dynamic>("SELECT * FROM funcorders_getorders()", new { });

        public async Task UpdateOrderAsync(OrderModel order) => await _dbAccess.SaveDataAsync("sporders_updateorder", new
        {
            arg_id = order.Id,
            arg_quantity_of_products = order.QuantityOfProducts,
            arg_status = order.Status,
            arg_total = order.Total,
            arg_updated_by = order.UpdatedBy
        });
    }
}
