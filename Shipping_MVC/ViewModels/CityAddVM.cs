using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Shipping_MVC.Models;

namespace Shipping_MVC.ViewModels
{
    public class CityAddVM
    {
        public int id { get; set; }

        [RegularExpression(@"^[a-zA-Z_ ]{3,25}$", ErrorMessage = "Name Must Be Between 3 to 25 charchters")]

        public string name { get; set; }

        public double charge_Regular { get; set; }

        public double charge_24Hour { get; set; }

        public double charge_15Days { get; set; }

        public double charge_89Days { get; set; }

        public int? governorate_Id { get; set; }

        [DefaultValue(true)]
        public bool available { get; set; }

        public List<Governroate>? governroates { get; set; }
    }
}
