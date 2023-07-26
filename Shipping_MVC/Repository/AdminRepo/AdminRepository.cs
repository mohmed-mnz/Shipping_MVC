using Microsoft.EntityFrameworkCore;
using Shipping_MVC.Models;

namespace Shipping_MVC.Repository.AdminRepo
{
    public class AdminRepository:IAdminRepositrory
    {
        ShippingContext db;

        public AdminRepository(ShippingContext _db)
        {
            this.db = _db;
        }
        public List<Admin> GetAdmin()
        {
            return db.admins.Include(a => a.Role).ToList();
        }
    }
}
