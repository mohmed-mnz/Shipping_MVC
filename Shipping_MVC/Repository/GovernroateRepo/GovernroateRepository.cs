using Microsoft.EntityFrameworkCore;
using Shipping_MVC.Models;

namespace Shipping_MVC.Repository.GovernroateRepo
{
    public class GovernroateRepository:IGovernroateRepository
    {
        ShippingContext db;
        public GovernroateRepository(ShippingContext _db)
        {
            this.db = _db;
        }

        public List<Governroate> GetGovernorates()
        {
            List<Governroate> governorates = db.governorates.Include(g => g.Branch).ToList();
            return governorates;
        }

        public List<Governroate> GetGovernoratesAvailable()
        {
            List<Governroate> governorates = db.governorates.Include(g => g.Branch).Where(g => g.Available == true).ToList();
            return governorates;
        }

        public Governroate GetGovernorateById(int id)
        {
            Governroate governorate = db.governorates.Include(g => g.Branch).SingleOrDefault(g => g.Id == id);
            return governorate;
        }

        public List<Governroate> GetGovernoratesByName(string name)
        {
            List<Governroate> governorates = db.governorates.Include(g => g.Branch).Where(g => g.Name.Contains(name)).ToList();
            return governorates;
        }

        public List<Governroate> GetGovernoratesByBranch(int branch_Id)
        {
            List<Governroate> governorates = db.governorates.Include(g => g.Branch).Where(g => g.Branch_Id == branch_Id && g.Available == true).ToList();
            return governorates;
        }

        public void Add(Governroate governorate)
        {
            db.Add(governorate);
        }

        public void Update(Governroate governorate)
        {
            db.Entry(governorate).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }

        public void SoftDelete(Governroate governorate)
        {
            governorate.Available = !governorate.Available;
        }

        public void Delete(Governroate governorate)
        {
            db.governorates.Remove(governorate);
        }

        public void Save()
        {
            db.SaveChanges();
        }

    }
}
