using RaalProjectPreview.DAL.Models.DBModels;
using System;
using System.Linq;

namespace RaalProjectPreview.DAL.Repository
{
    public class OrderRepository : RepositoryBase<Order>
    {
        public Order GetOrderById(int id)
        {
            return _DbSet.Where(x => x.Id == id).FirstOrDefault();
        }
        public Order UpdateOrder(Order order)
        {
            Order _updatedOrder = GetOrderById(order.Id);
            _updatedOrder.OrderDate = order.OrderDate;
            _updatedOrder.OrderNumber = order.OrderNumber;
            _updatedOrder.ShipmentDate = order.ShipmentDate;
            _updatedOrder.Status = order.Status;
            _APPContext.SaveChanges();
            return _updatedOrder;
        }
    }
}