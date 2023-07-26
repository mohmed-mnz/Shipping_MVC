using Shipping_MVC.Models;

namespace Shipping_MVC.Repository.GovernroateRepo
{
    public interface IGovernroateRepository
    {
        List<Governroate> GetGovernorates();

        List<Governroate> GetGovernoratesAvailable();

        Governroate GetGovernorateById(int id);

        List<Governroate> GetGovernoratesByName(string name);

        List<Governroate> GetGovernoratesByBranch(int branch_Id);

        void Add(Governroate governorate);

        void Update(Governroate governorate);

        void SoftDelete(Governroate governorate);
        void Delete(Governroate governorate);
        void Save();
    }
}
