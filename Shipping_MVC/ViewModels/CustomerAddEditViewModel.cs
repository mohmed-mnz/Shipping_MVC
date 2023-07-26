using Shipping_MVC.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Shipping_MVC.ViewModels
{
    public class CustomerAddEditViewModel
    {
        [RegularExpression(@"^[a-zA-Z_ ]{5,25}$", ErrorMessage = "Name Must Be Between 5 to 25 charchters")]
        public string name { get; set; }

        [RegularExpression(@"[a-zA-Z0-9]{1,20}@[a-zA-Z]{1,7}.com", ErrorMessage = "Enter valid E-Mail Address")]
        public string email { get; set; }

        [MinLength(8, ErrorMessage = "Minimum Length is 8")]
        [DataType(DataType.Password)]

        public string password { get; set; }

        [RegularExpression(@"^01[0125][0-9]{8}$", ErrorMessage = "Enter Valid Phone Number")]
        public string phone { get; set; }

        [MinLength(5, ErrorMessage = "Minimum Length is 5")]
        [MaxLength(25, ErrorMessage = "Maximum Length is 25")]
        public string address { get; set; }

        [MinLength(5, ErrorMessage = "Minimum Length is 5")]
        [MaxLength(25, ErrorMessage = "Maximum Length is 25")]
        public string store_Name { get; set; }

        [Range(0, 100, ErrorMessage = "Range 0 - 100")]
        public double special_Discount_Perc { get; set; }

        [Range(0, 100, ErrorMessage = "Range 0 - 100")]
        public double refused_Order_Perc { get; set; }

        public int branch_Id { get; set; }

        [DefaultValue(3)]
        public int role_Id { get; set; }
        [DefaultValue(true)]
        public bool available { get; set; }
        public List<Branch>? branches_List { get; set; }
    }
}
