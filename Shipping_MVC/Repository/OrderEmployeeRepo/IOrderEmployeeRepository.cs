using Shipping_MVC.Models;

namespace Shipping_MVC.Repository.OrderEmployeeRepo
{
    public interface IOrderEmployeeRepository
    {
        List<Order> GetOrdersByCustomerBranch(int branch_Id);

        List<Order> GetOrdersByCustomerBranchStatus(int branch_Id, string status);

        List<Order> GetOrderSearch(int id, int branch_Id);

        Order GetOrderById(int id);

        void EditOrder(Order order);

        void UpdateOrderStatus(Order order, string status);

        void UpdateOrderDeliveryId(Order order, int delivery_Id);

        void DeleteOrder(int id);

        void Save();
    }
}
