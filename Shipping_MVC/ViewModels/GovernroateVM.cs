namespace Shipping_MVC.ViewModels
{
    public class GovernroateVM
    {
        public int id { get; set; }
        public string name { get; set; }
        public bool available { get; set; }
        public string branch_Name { get; set; }

        public int? branch_Id { get; set; }
    }
}
