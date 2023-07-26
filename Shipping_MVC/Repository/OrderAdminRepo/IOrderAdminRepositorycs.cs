using Shipping_MVC.Models;

namespace Shipping_MVC.Repository.OrderAdminRepo
{
    public interface IOrderAdminRepositorycs
    {
        List<Order> GetOrders();

        List<Order> GetOrdersByStatus(string status);

        List<Order> GetOrdersReport(string status, DateTime startDate, DateTime endDate);

        Order GetOrderById(int id);

        void Save();
    }
}
