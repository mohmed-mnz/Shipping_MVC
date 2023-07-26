using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Shipping_MVC.Models;

namespace Shipping_MVC.ViewModels
{
    public class GovernroateAddVM
    {
        public int id { get; set; }

        [RegularExpression(@"^[a-zA-Z_ ]{3,25}$", ErrorMessage = "Name Must Be Between 3 to 25 charchters")]
        public string name { get; set; }

        [DefaultValue(true)]
        public bool available { get; set; } = true;
        public int? branch_Id { get; set; }

        public List<Branch>? branches_List { get; set; }
    }
}
