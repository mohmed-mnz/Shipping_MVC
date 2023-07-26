using Shipping_MVC.Models;

namespace Shipping_MVC.Repository.OrderCustomerRepo
{
    public interface IOrderCustomerRepository
    {
        List<Order> GetOrdersByCustomer(int customer_Id);

        List<Order> GetOrdersByCustomerStatus(int customer_Id, string status);

        Order GetOrderById(int id);

        void AddOrder(Order order);

        void Save();
    }
}
