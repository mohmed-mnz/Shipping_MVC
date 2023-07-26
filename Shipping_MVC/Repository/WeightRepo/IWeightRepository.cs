using Shipping_MVC.Models;

namespace Shipping_MVC.Repository.WeightRepo
{
    public interface IWeightRepository
    {
        Weight GetWeight();

        void EditWeight(Weight weight);

        void Save();
    }
}
