using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Order_Aggregate;
using Talabat.Core.Entities.Product;
using Talabat.Core.Interfaces;
using Talabat.Core.Services;
using Talabat.Core.Specification.Orders;
using Talabat.Reposatory.Repositories;

namespace Talabat.Services
{
    public class OrderService : IOrderService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(
            IBasketRepository basketRepository,
            IUnitOfWork unitOfWork) 
        {
            _basketRepository = basketRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Order?> CreateOrderAsync(string buyerEmail, string basketId, int deliverMethodId, Address shippingAddress)
        {
            // 1. Get Basket From Baskets Repo
            var basket = await _basketRepository.GetBasketAsync(basketId);
            
            // 2. Get Selected Items at Basket From Products Repo
            var orderItems = new List<OrderItem>();

            if(basket?.Items?.Count > 0)
            {
                foreach (var item in basket.Items)
                {
                    var product = await _unitOfWork.Repository<Product>().GetByIdAsync(item.Id);

                    var productItemOrdered = new ProductItemOrdered(product.Id, product.Name, product.PictureURL);

                    var orderItem = new OrderItem(productItemOrdered, product.Price, item.Quantity);

                    orderItems.Add(orderItem);
                }
            }
            
            // 3. Calculate SubTotal
            var subTotal = orderItems.Sum(item => item.Price *  item.Quantity);

            // 4. Get Delivery Method From DeliveryMethods Repo
            var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(deliverMethodId);
            
            // 5. Create Order
            var order = new Order(buyerEmail, shippingAddress, deliveryMethod, orderItems, subTotal);
            await _unitOfWork.Repository<Order>().AddAsync(order);
            
            // 6. Save To Database [TODO]
            var result = await _unitOfWork.CompleteAsync();

            if (result <= 0)
                return null;

            return order;
        }

       
        public async Task<Order> GetOrderByIdForUserAsync(int orderId, string buyerEmail)
        {
            var spec = new OrderWithItemsAndDeliveryMethodSpecifications(orderId, buyerEmail);

            var order = await _unitOfWork.Repository<Order>().GetByIdWithSpecAsync(spec);

            return order;
        }

        public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
        {
            var spec = new OrderWithItemsAndDeliveryMethodSpecifications(buyerEmail);

            var orders = await _unitOfWork.Repository<Order>().GetAllWithSpecAsync(spec);

            return orders;
        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodAsync()
        {
            var delivaryMethods = await _unitOfWork.Repository<DeliveryMethod>().GetAllAsync();
            return delivaryMethods;
        }

    }
}
