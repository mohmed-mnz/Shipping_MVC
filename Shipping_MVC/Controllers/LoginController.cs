using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Shipping_MVC.Models;
using Shipping_MVC.Repository.AdminRepo;
using Shipping_MVC.Repository.CustomerRepo;
using Shipping_MVC.Repository.DeliveryRepo;
using Shipping_MVC.Repository.EmployeeRepo;
using Shipping_MVC.ViewModels;
using System.Security.Claims;

namespace Shipping_MVC.Controllers
{
    public class LoginController : Controller
    {
        IAdminRepositrory AdminRebo;
        IEmployeeRepository EmpRebo;
        ICustomerRepository CustomerRebo;
        IDeliveryRepository DeliveryRebo;

        public LoginController(IAdminRepositrory _admin, IEmployeeRepository emp, ICustomerRepository cust, IDeliveryRepository delv) {

            this.AdminRebo = _admin;
            this.EmpRebo = emp;
            this.CustomerRebo = cust;
            this.DeliveryRebo = delv;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(UserDataViewModel _AllDto)
        {
            List<Employee> emp = EmpRebo.getAvailableEmployees();
            List<Delivery> deliveries = DeliveryRebo.GetDeleveriesAvailable();
            List<Customer> customer = CustomerRebo.GetAllAvailableCustomer();
            List<Admin> admins = AdminRebo.GetAdmin();
            List<UserDataSentViewModel> ListAll = new List<UserDataSentViewModel>();
            for (int i = 0; i < emp.Count; i++)
            {
                UserDataSentViewModel allViewModel = new UserDataSentViewModel()
                {
                    id = emp[i].Id,
                    name = emp[i].Name,
                    email = emp[i].Email,
                    password = emp[i].Password,
                    role_Name = emp[i].Role.Name,
                    role_Id = emp[i].Role_Id,
                    branch_Id = emp[i].Branch.Id,
                };
                ListAll.Add(allViewModel);
            }

            for (int i = 0; i < deliveries.Count; i++)
            {
                UserDataSentViewModel allViewModel = new UserDataSentViewModel()
                {
                    id = deliveries[i].Id,
                    email = deliveries[i].Email,
                    password = deliveries[i].Password,
                    name = deliveries[i].Name,
                    role_Name = deliveries[i].Role.Name,
                    role_Id = deliveries[i].Role_Id,
                    branch_Id = deliveries[i].Branch.Id,
                };
                ListAll.Add(allViewModel);
            }
            for (int i = 0; i < customer.Count; i++)
            {
                UserDataSentViewModel allDTO = new UserDataSentViewModel()
                {
                    email = customer[i].Email,
                    password = customer[i].Password,
                    id = customer[i].Id,
                    role_Name = customer[i].Role.Name,
                    role_Id = customer[i].Role_Id,
                    branch_Id = customer[i].Branch.Id,
                    name = customer[i].Name
                };
                ListAll.Add(allDTO);
            }

            for (int i = 0; i < admins.Count; i++)
            {
                UserDataSentViewModel allDTO = new UserDataSentViewModel()
                {
                    email = admins[i].Email,
                    password = admins[i].Password,
                    role_Id = admins[i].Role_Id,
                    id = admins[i].Id,
                    role_Name = admins[i].Role.Name,
                    branch_Id = null,
                    name = admins[i].Name
                };
                ListAll.Add(allDTO);
            }

            for (int i = 0; i < ListAll.Count; i++)
            {
                if (ListAll[i].email == _AllDto.email && ListAll[i].password == _AllDto.password)
                {
                    ClaimsIdentity claims =
                    new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                    claims.AddClaim(
                        new Claim(ClaimTypes.NameIdentifier, ListAll[i].id.ToString()));
                    claims.AddClaim(
                        new Claim(ClaimTypes.Name, ListAll[i].name.ToString()));
                    claims.AddClaim(
                        new Claim(ClaimTypes.Role, ListAll[i].role_Name.ToString()));
                    claims.AddClaim(
                        new Claim("branch_Id", ListAll[i].branch_Id.ToString()));

                    ClaimsPrincipal principle =
                        new ClaimsPrincipal(claims);
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principle);


                    return RedirectToAction("Index", "Home");


                }
            }
            return Unauthorized();
        }


        public IActionResult logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index","home");
        }
    }


}
