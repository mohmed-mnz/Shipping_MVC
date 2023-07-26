using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Shipping_MVC.Models
{
    public class Governroate
    {
        public int Id { get; set; }

        [RegularExpression(@"^[a-zA-Z_ ]{3,25}$", ErrorMessage = "Name Must Be Between 3 to 25 charchters")]
        public string Name { get; set; }
        public bool Available { get; set; } = true;

        [ForeignKey("Branch")]
        public int? Branch_Id { get; set; }
        public virtual Branch? Branch { get; set; }

        public virtual List<City>? Cities { get; set; }
        public virtual List<Order>? Orders { get; set; }
    }
}
