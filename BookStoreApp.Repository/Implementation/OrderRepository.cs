using BookStoreApp.Data;
using BookStoreApp.Domain.Entities;
using BookStoreApp.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreApp.Repository.Implementation
{
    public class OrderRepository : IOrderRepository
    {

        private readonly ApplicationDbContext context;
        private DbSet<Order> entities;
        //string errorMessage = string.Empty;

        public OrderRepository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<Order>();
        }

        public Order deleteOrder(Order order)
        {

            if (order == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(order);
            context.SaveChanges();
            return order;
        }

        public Order getOrderById(BaseEntity model)
        {
            return entities
                .Include(i => i.owner)
                .Include(i => i.bookInOrders)
                .Include("bookInOrders.orderedBook")
                .SingleOrDefaultAsync(i => i.Id == model.Id).Result;
        }

        public List<Order> getOrders()
        {
            return entities
                .Include(i => i.owner)
                .Include(i => i.bookInOrders)
                .Include("bookInOrders.orderedBook")
                .ToList();
        }
    }
}
