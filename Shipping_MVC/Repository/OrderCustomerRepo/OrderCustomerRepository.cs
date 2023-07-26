using Microsoft.EntityFrameworkCore;
using Shipping_MVC.Models;

namespace Shipping_MVC.Repository.OrderCustomerRepo
{
    public class OrderCustomerRepository:IOrderCustomerRepository
    {
        ShippingContext db;
        public OrderCustomerRepository(ShippingContext _db)
        {
            this.db = _db;
        }

        public List<Order> GetOrdersByCustomer(int customer_Id)
        {
            return db.orders.Include(o => o.Customer).Include(o => o.Delivery).Include(o => o.Branch).Include(o => o.Governorate).Include(o => o.City).Where(o => o.Customer_Id == customer_Id).ToList();
        }

        public List<Order> GetOrdersByCustomerStatus(int customer_Id, string status)
        {
            return db.orders.Include(o => o.Customer).Include(o => o.Delivery).Include(o => o.Branch).Include(o => o.Governorate).Include(o => o.City).Where(o => o.Customer_Id == customer_Id && o.Status == status).ToList();
        }

        public Order GetOrderById(int id)
        {
            return db.orders.Include(o => o.Customer).Include(o => o.Delivery).Include(o => o.Branch).Include(o => o.Governorate).Include(o => o.City).SingleOrDefault(o => o.Id == id);
        }

        public void AddOrder(Order order)
        {
            db.orders.Add(order);
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}
