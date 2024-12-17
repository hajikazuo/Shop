using Shop.Api.Repositories.Interfaces;
using Shop.Common.Context;
using Shop.Common.Models.Entities;

namespace Shop.Api.Repositories.Implementation
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;

        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Order> CreateOrderAsync(Guid userId, Basket basket, Guid orderId)
        {
            var order = new Order
            {
                OrderId = orderId,
                UserId = userId,
                OrderDate = DateTime.Now,
            };

            foreach (var item in basket.Items)
            {
                var orderItem = new OrderItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = item.Product.Price
                };

                order.OrderItems.Add(orderItem);

                var product = await _context.Products.FindAsync(item.ProductId);
                if (product != null)
                {
                    product.Stock -= item.Quantity;
                    if (product.Stock < 0)
                    {
                        throw new InvalidOperationException($"Product {product.Name} does not have enough stock.");
                    }
                }
            }

            order.TotalAmount = basket.Total;

            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            return order;
        }
    }
}
