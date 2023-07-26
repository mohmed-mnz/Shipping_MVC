using Microsoft.EntityFrameworkCore;
using Shipping_MVC.Models;

namespace Shipping_MVC.Repository.OrderEmployeeRepo
{
    public class OrderEmployeeRepository:IOrderEmployeeRepository
    {
        ShippingContext db;

        public OrderEmployeeRepository(ShippingContext _db)
        {
            this.db = _db;
        }

        public List<Order> GetOrdersByCustomerBranch(int branch_Id)
        {
            return db.orders.Include(o => o.Customer).Include(o => o.Delivery).Include(o => o.Branch).Include(o => o.Governorate).Include(o => o.City).Where(o => o.Customer.Branch_Id == branch_Id).ToList();
        }

        public List<Order> GetOrdersByCustomerBranchStatus(int branch_Id, string status)
        {
            return db.orders.Include(o => o.Customer).Include(o => o.Delivery).Include(o => o.Branch).Include(o => o.Governorate).Include(o => o.City).Where(o => o.Customer.Branch_Id == branch_Id && o.Status == status).ToList();
        }

        public List<Order> GetOrderSearch(int id, int branch_Id)
        {
            return db.orders.Include(o => o.Customer).Include(o => o.Delivery).Include(o => o.Branch).Include(o => o.Governorate).Include(o => o.City).Where(o => o.Customer.Branch_Id == branch_Id && o.Id == id).ToList();

        }

        public Order GetOrderById(int id)
        {
            return db.orders.Include(o => o.Customer).Include(o => o.Delivery).Include(o => o.Branch).Include(o => o.Governorate).Include(o => o.City).SingleOrDefault(o => o.Id == id);
        }

        public void EditOrder(Order order)
        {
            db.Entry(order).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }

        public void UpdateOrderStatus(Order order, string status)
        {
            order.Status = status;
        }

        public void UpdateOrderDeliveryId(Order order, int delivery_Id)
        {
            order.Delivery_Id = delivery_Id;
        }

        public void DeleteOrder(int id)
        {
            Order order = GetOrderById(id);
            db.orders.Remove(order);
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}
