using BookStoreApp.Domain.DTO;
using BookStoreApp.Domain.Entities;
using BookStoreApp.Repository.Implementation;
using BookStoreApp.Repository.Interface;
using BookStoreApp.Service.Interface;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreApp.Service.Implementation
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRepository<ShoppingCart> _shoppingCartRepository;
        private readonly IRepository<Book> _bookRepository;
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<BookInOrder> _bookInOrderRepository;


        public ShoppingCartService(IUserRepository userRepository, IRepository<ShoppingCart> shoppingCartRepository, IRepository<Book> bookRepository, IRepository<Order> orderRepository, IRepository<BookInOrder> bookInOrderRepository)
        {
            _userRepository = userRepository;
            _shoppingCartRepository = shoppingCartRepository;
            _bookRepository = bookRepository;
            _orderRepository = orderRepository;
            _bookInOrderRepository = bookInOrderRepository;
        }

        public ShoppingCart AddBookToShoppingCart(string userId, AddToCartDTO model)
        {
            if (userId != null)
            {
                var loggedInUser = _userRepository.Get(userId);

                var userCart = loggedInUser?.UserCart;

                var selectedBook = _bookRepository.Get(model.selectedBookId);

                if (selectedBook != null && userCart != null)
                {
                    userCart?.bookInShoppingCart?.Add(new BookInShoppingCart
                    {
                        book = selectedBook,
                        bookId = selectedBook.Id,
                        shoppingCart = userCart,
                        shoppingCartId = userCart.Id,
                        quantity = model.quantity
                    });

                    return _shoppingCartRepository.Update(userCart);
                }
            }
            return null;
        }

        public bool deleteFromShoppingCart(string userId, Guid? Id)
        {
            if (userId != null)
            {
                var loggedInUser = _userRepository.Get(userId);


                var product_to_delete = loggedInUser?.UserCart?.bookInShoppingCart.First(z => z.bookId == Id);

                loggedInUser?.UserCart?.bookInShoppingCart?.Remove(product_to_delete);

                _shoppingCartRepository.Update(loggedInUser.UserCart);

                return true;

            }

            return false;
        }

        public AddToCartDTO getBookInfo(Guid Id)
        {
            var selectedBook = _bookRepository.Get(Id);
            if (selectedBook != null)
            {
                var model = new AddToCartDTO
                {
                    selectedBookTitle = selectedBook.title.ToString(),
                    selectedBookId = selectedBook.Id,
                    quantity = 1
                };
                return model;
            }
            return null;
        }

        public ShoppingCartDTO getShoppingCartDetails(string userId)
        {
            if (userId != null && !userId.IsNullOrEmpty())
            {
                var loggedInUser = _userRepository.Get(userId);

                var allProducts = loggedInUser?.UserCart?.bookInShoppingCart?.ToList();
                var totalPrice = 0.0;
                if(allProducts != null)
                {
                    foreach (var item in allProducts)
                    {
                        totalPrice += Double.Round((double)(item.quantity * item.book.price), 2);
                    }

                    var model = new ShoppingCartDTO
                    {
                        allBooks = allProducts,
                        totalPrice = totalPrice
                    };

                    return model;
                }
            }

            return new ShoppingCartDTO
            {
                allBooks = new List<BookInShoppingCart>(),
                totalPrice = 0.0
            };
        }

        public bool orderBooks(string userId)
        {
            if (userId != null)
            {
                var loggedInUser = _userRepository.Get(userId);

                var userShoppingCart = loggedInUser.UserCart;

                Order order = new Order
                {
                    Id = Guid.NewGuid(),
                    ownerId = userId,
                    owner = loggedInUser
                };

                _orderRepository.Insert(order);

                List<BookInOrder> productInOrder = new List<BookInOrder>();

                var lista = userShoppingCart.bookInShoppingCart.Select(
                    x => new BookInOrder
                    {
                        Id = Guid.NewGuid(),
                        bookId = x.bookId,
                        orderedBook = x.book,
                        orderId = order.Id,
                        order = order,
                        quantity = x.quantity
                    }
                    ).ToList();

                productInOrder.AddRange(lista);

                foreach (var product in productInOrder)
                {
                    _bookInOrderRepository.Insert(product);
                }

                loggedInUser.UserCart.bookInShoppingCart.Clear();
                _userRepository.Update(loggedInUser);
                return true;
            }
            return false;
        }
    }
}
