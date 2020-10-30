using RaalProjectPreview.DAL.Models.DBModels;
using System;
using System.Linq;

namespace RaalProjectPreview.DAL.Repository
{
    public class OrderItemRepository : RepositoryBase<OrderItem>
    {
        public OrderItem GetOrderItemById(int id)
        {
            return _DbSet.Where(x => x.Id == id).FirstOrDefault();
        }
        public OrderItem UpdateOrderItem(OrderItem orderItem)
        {
            OrderItem _updatedOrder = GetOrderItemById(orderItem.Id);
            _updatedOrder.ItemId = orderItem.ItemId;
            _updatedOrder.ItemPrice = orderItem.ItemPrice;
            _updatedOrder.ItemsCount = orderItem.ItemsCount;
            _updatedOrder.OrderId = orderItem.OrderId;
            _APPContext.SaveChanges();
            return _updatedOrder;
        }
    }
}