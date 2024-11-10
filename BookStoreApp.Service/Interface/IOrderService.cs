using BookStoreApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreApp.Service.Interface
{
    public interface IOrderService
    {
        public List<Order> GetOrders();
        public Order GetDetailsForOrder(BaseEntity model);
        public Order DeleteOrder(BaseEntity model);
    }
}
