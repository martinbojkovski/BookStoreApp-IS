using BookStoreApp.Domain.Entities;
using BookStoreApp.Service.Interface;
using BookStoreApp.Domain.DTO;
using BookStoreApp.Domain.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Eshop.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly UserManager<BookStoreUser> _userManager;
        public AdminController(IOrderService orderService, UserManager<BookStoreUser> userManager)
        {
            _orderService = orderService;
            _userManager = userManager;
        }
        [HttpGet("[action]")]
        public List<Order> GetAllOrders()
        {
            return this._orderService.GetOrders();
        }
        [HttpPost("[action]")]
        public Order GetDetails(BaseEntity id)
        {
            return this._orderService.GetDetailsForOrder(id);
        }

        [HttpPost("[action]")]
        public bool ImportAllUsers(List<UserRegistrationDTO> model)
        {
            bool status = true;

            foreach (var item in model)
            {
                var userCheck = _userManager.FindByEmailAsync(item.Email).Result;

                if (userCheck == null)
                {
                    var user = new BookStoreUser
                    {
                        UserName = item.Email,
                        NormalizedUserName = item.Email,
                        Email = item.Email,
                        EmailConfirmed = true,
                        UserCart = new ShoppingCart()
                    };

                    var result = _userManager.CreateAsync(user, item.Password).Result;
                    status = status && result.Succeeded;
                }
                else
                {
                    continue;
                }
            }
            return status;
        }

    }
}