using BookStoreApp.Domain.Entities;
using BookStoreApp.Repository.Interface;
using BookStoreApp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreApp.Service.Implementation
{
    public class OrderSevice : IOrderService
    {

        private readonly IOrderRepository _orderRepository;

        public OrderSevice(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public Order DeleteOrder(BaseEntity model)
        {
            Order order = this.GetDetailsForOrder(model);
            return this._orderRepository.deleteOrder(order);
        }

        public Order GetDetailsForOrder(BaseEntity model)
        {
            return this._orderRepository.getOrderById(model);
        }

        public List<Order> GetOrders()
        {
            return this._orderRepository.getOrders();
        }
    }
}
