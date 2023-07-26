using Shipping_MVC.Models;

namespace Shipping_MVC.Repository.OrderDeliveryRepo
{
    public interface IOrderDeliveryRepository
    {
        List<Order> GetOrdersByDelivery(int delivery_Id);

        List<Order> GetOrdersByDeliveryStatus(int delivery_Id, string status);

        List<Order> GetSearch(int id, int delivery_Id);
        Order GetOrderById(int id);

        void UpdateOrderStatus(Order order, string status);

        void Save();
    }
}
