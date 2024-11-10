using BookStoreApp.Domain.DTO;
using BookStoreApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreApp.Service.Interface
{
    public interface IShoppingCartService
    {
        ShoppingCart AddBookToShoppingCart(string userId, AddToCartDTO model);
        AddToCartDTO getBookInfo(Guid Id);
        ShoppingCartDTO getShoppingCartDetails(string userId);
        Boolean deleteFromShoppingCart(string userId, Guid? Id);
        Boolean orderBooks(string userId);
    }
}
