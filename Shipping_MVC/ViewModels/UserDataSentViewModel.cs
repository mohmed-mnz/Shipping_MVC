namespace Shipping_MVC.ViewModels
{
    public class UserDataSentViewModel
    {
        public int? id { get; set; }
        public string email { get; set; }

        public string? name { get; set; }
        public int? branch_Id { get; set; }

        public string password { get; set; }
        public int? role_Id { get; set; }

        public string role_Name { get; set; }
    }
}
