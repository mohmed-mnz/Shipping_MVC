using Microsoft.EntityFrameworkCore;
using Shipping_MVC.Models;

namespace Shipping_MVC.Repository.OrderAdminRepo
{
    public class OrderAdminRepository:IOrderAdminRepositorycs
    {
        ShippingContext db;
        public OrderAdminRepository(ShippingContext _db)
        {
            this.db = _db;
        }

        public List<Order> GetOrders()
        {
            return db.orders.Include(o => o.Customer).Include(o => o.Delivery).Include(o => o.Branch).Include(o => o.Governorate).Include(o => o.City).ToList();
        }

        public List<Order> GetOrdersByStatus(string status)
        {
            return db.orders.Include(o => o.Customer).Include(o => o.Delivery).Include(o => o.Branch).Include(o => o.Governorate).Include(o => o.City).Where(o => o.Status == status).ToList();
        }

        public List<Order> GetOrdersReport(string status, DateTime startDate, DateTime endDate)
        {
            return db.orders.Include(o => o.Customer).Include(o => o.Delivery).Include(o => o.Branch).Include(o => o.Governorate).Include(o => o.City).Where(o => o.Status == status && o.Order_Date.Date >= startDate.Date && o.Order_Date.Date <= endDate.Date).ToList();
        }

        public Order GetOrderById(int id)
        {
            return db.orders.Include(o => o.Customer).Include(o => o.Delivery).Include(o => o.Branch).Include(o => o.Governorate).Include(o => o.City).SingleOrDefault(o => o.Id == id);
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}
