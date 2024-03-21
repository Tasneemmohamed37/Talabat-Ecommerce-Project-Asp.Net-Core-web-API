using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Order_Aggregate;
using Talabat.Core.Entities.Product;
using Talabat.Core.Services;
using Talabat.Reposatory.Repositories;

namespace Talabat.Services
{
    public class OrderService : IOrderService
    {
        private readonly BasketRepository _basketRepository;
        private readonly GenericRepository<Product> _productRepository;
        private readonly GenericRepository<DeliveryMethod> _deliveryRepository;
        private readonly GenericRepository<Order> _orderRepository;

        public OrderService(
            BasketRepository basketRepository,
            GenericRepository<Product> productRepository,
            GenericRepository<DeliveryMethod> deliveryRepository,
            GenericRepository<Order> orderRepository) 
        {
            _basketRepository = basketRepository;
            _productRepository = productRepository;
            _deliveryRepository = deliveryRepository;
            _orderRepository = orderRepository;
        }

        public async Task<Order> CreateOrderAsync(string buyerEmail, string basketId, int deliverMethodId, Address shippingAddress)
        {
            // 1. Get Basket From Baskets Repo
            var basket = await _basketRepository.GetBasketAsync(basketId);
            
            // 2. Get Selected Items at Basket From Products Repo
            var orderItems = new List<OrderItem>();

            if(basket?.Items?.Count > 0)
            {
                foreach (var item in orderItems)
                {
                    var product = await _productRepository.GetByIdAsync(item.Id);

                    var productItemOrdered = new ProductItemOrdered(product.Id, product.Name, product.PictureURL);

                    var orderItem = new OrderItem(productItemOrdered, product.Price, item.Quantity);

                    orderItems.Add(orderItem);
                }
            }
            
            // 3. Calculate SubTotal
            var subTotal = orderItems.Sum(item => item.Price *  item.Quantity);

            // 4. Get Delivery Method From DeliveryMethods Repo
            var deliveryMethod = await _deliveryRepository.GetByIdAsync(deliverMethodId);
            
            // 5. Create Order
            var order = new Order(buyerEmail, shippingAddress, deliveryMethod, orderItems, subTotal);
            await _orderRepository.AddAsync(order);
            
            // 6. Save To Database [TODO]

            return order;
        }

       
        public Task<Order> GetOrderByIdForUserAsync(int orderId, string buyerEmail)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodAsync()
        {
            throw new NotImplementedException();
        }

    }
}
