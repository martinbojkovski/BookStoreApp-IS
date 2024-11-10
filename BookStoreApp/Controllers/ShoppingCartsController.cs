using BookStoreApp.Service.Interface;
using EShop.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Stripe;
using System.Security.Claims;

namespace BookStoreApp.Web.Controllers
{
    public class ShoppingCartsController : Controller
    {
        private readonly IShoppingCartService _shoppingCartService;
        private readonly StripeSettings _stripeSettings;


        public ShoppingCartsController(IShoppingCartService shoppingCartService, IOptions<StripeSettings> stripeSettings)
        {
            _shoppingCartService = shoppingCartService;
            _stripeSettings = stripeSettings.Value;
            StripeConfiguration.ApiKey = _stripeSettings.SecretKey;
        }

        // GET: ShoppingCarts
        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            return View(_shoppingCartService.getShoppingCartDetails(userId));
        }

        // GET: ShoppingCarts/Delete/5
        public IActionResult DeleteProductFromShoppingCart(Guid? productId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = _shoppingCartService.deleteFromShoppingCart(userId, productId);

            return RedirectToAction("Index", "ShoppingCarts");
        }

        public IActionResult Order()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = _shoppingCartService.orderBooks(userId);

            return RedirectToAction("Index", "ShoppingCarts");
        }

        public IActionResult SuccessPayment()
        {
            return View();
        }

        public IActionResult PayOrder(string stripeEmail, string stripeToken)
        {
            StripeConfiguration.ApiKey = "sk_test_51QJivo033d3HqTnkPcVdVGb7co8qy3TJtA2odbcnfd2vJPHxCjGfo6fWanJuHJR135vup8VU78qcPv8cHSlO8CfG00KJSHDk5j";
            var customerService = new CustomerService();
            var chargeService = new ChargeService();
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var order = this._shoppingCartService.getShoppingCartDetails(userId);

            var customer = customerService.Create(new CustomerCreateOptions
            {
                Email = stripeEmail,
                Source = stripeToken
            });

            var charge = chargeService.Create(new ChargeCreateOptions
            {
                Amount = (Convert.ToInt32(order.totalPrice) * 100),
                Description = "BookStore Application Payment",
                Currency = "eur",
                Customer = customer.Id
            });

            if (charge.Status == "succeeded")
            {
                this.Order();
                return RedirectToAction("SuccessPayment");

            }
            else
            {
                return RedirectToAction("NotsuccessPayment");
            }
        }

    }
}
