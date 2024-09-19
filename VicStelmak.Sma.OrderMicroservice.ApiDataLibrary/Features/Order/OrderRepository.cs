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

        public async Task CreateLineItemAsync(LineItemModel item) => await _dbAccess.SaveDataAsync("sp_createlineitem", new
        {
            arg_line_item_total_price = item.TotalPrice,
            arg_order_id = item.OrderId,
            arg_product_id = item.ProductId,
            arg_quantity = item.Quantity
        });

        public async Task CreateOrderAsync(OrderModel order, int productId, decimal lineItemTotalPrice) => 
            await _dbAccess.SaveDataAsync("sp_createorder", new
        {
            arg_apartment = order.Apartment,
            arg_building = order.Building,
            arg_city = order.City,
            arg_created_by = order.CreatedBy,
            arg_line_item_total_price = lineItemTotalPrice,  
            arg_order_code = order.OrderCode,
            arg_postal_code = order.PostalCode,
            arg_product_id = productId,
            arg_quantity_of_products = order.QuantityOfProducts,
            arg_status = order.Status,
            arg_street = order.Street,
            arg_total = order.Total
        });

        public async Task DeleteOrderAsync(int orderId) => await _dbAccess.SaveDataAsync("sp_deleteorder", new { arg_id = orderId });

        public async Task<List<OrderModel>> FindOrdersByUserEmailAsync(string userEmail)
        {
            return await _dbAccess.LoadDataAsync<OrderModel, dynamic>(
                "SELECT * FROM func_findordersbyuseremail(:arg_email)", new { arg_email = userEmail });
        }

        public async Task<OrderModel> FindPendingOrderByUserEmailAsync(string userEmail)
        {
            var orders = await _dbAccess.LoadDataAsync<OrderModel, dynamic>(
                "SELECT * FROM func_findpendingorderbyuseremail(:arg_email)", new { arg_email = userEmail });

            return orders.FirstOrDefault();
        }

        public async Task<List<LineItemModel>> GetLineItemsByOrderIdAsync(int orderId) =>
            await _dbAccess.LoadDataAsync<LineItemModel, dynamic>("SELECT * FROM func_getlineitemsbyorderid(:arg_order_id)",
                new { arg_order_id = orderId });

        public async Task<OrderModel> GetOrderByIdAsync(int orderId)
        {
            var getOrderResult = await _dbAccess.LoadDataAsync<OrderModel, dynamic>("SELECT * FROM func_getorderbyid(:arg_id)",
                new { arg_id = orderId });

            return getOrderResult.FirstOrDefault();
        }

        public async Task<List<OrderModel>> GetOrdersAsync() =>
            await _dbAccess.LoadDataAsync<OrderModel, dynamic>("SELECT * FROM func_getorders()", new { });

        public async Task UpdateOrderAsync(OrderModel order) => await _dbAccess.SaveDataAsync("sp_updateorder", new
        {
            arg_id = order.Id,
            arg_apartment = order.Apartment,
            arg_building = order.Building,
            arg_city = order.City,
            arg_postal_code = order.PostalCode,
            arg_quantity_of_products = order.QuantityOfProducts,
            arg_status = order.Status,
            arg_street = order.Street,
            arg_total = order.Total,
            arg_updated_by = order.UpdatedBy
        });
    }
}
