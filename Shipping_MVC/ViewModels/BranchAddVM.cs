using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Shipping_MVC.ViewModels
{
    public class BranchAddVM
    {
        public int id { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9_ ]{3,25}$", ErrorMessage = "Name Must Be Between 3 to 25 charchters")]

        public string name { get; set; }

        [DefaultValue(true)]
        public bool available { get; set; }
    }
}
