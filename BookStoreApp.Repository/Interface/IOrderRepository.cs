using BookStoreApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreApp.Repository.Interface
{
    public interface IOrderRepository
    {
        public List<Order> getOrders();
        public Order getOrderById(BaseEntity model);
        public Order deleteOrder(Order order);
    }
}
