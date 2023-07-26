using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shipping_MVC.Models;
using Shipping_MVC.Repository.BranchRepo;
using Shipping_MVC.Repository.CustomerRepo;
using Shipping_MVC.ViewModels;
using System.Data;

namespace Shipping_MVC.Controllers
{
    [Authorize(Roles = "Admin")]

    public class CustomerController : Controller
    {
        ICustomerRepository CustRepo;
        IBranchRepository BranchRepo;
        public CustomerController(ICustomerRepository _CustRepo, IBranchRepository _BranchRepo)
        {
            this.CustRepo = _CustRepo;
            this.BranchRepo = _BranchRepo;
        }

        public IActionResult GetAll()
        {
            List<Customer> customers = CustRepo.GetAllCustomer();
            if (customers == null)
            {
                return Content("No Customer Here");
            }
            else
            {
                List<CustomerViewModel> customerVMs = new List<CustomerViewModel>();

                foreach (Customer cust in customers)
                {
                    CustomerViewModel customerVM = new CustomerViewModel()
                    {
                        id = cust.Id,
                        name = cust.Name,
                        email = cust.Email,
                        phone = cust.Phone,
                        address = cust.Address,
                        password = cust.Password,
                        store_Name = cust.Store_Name,
                        special_Discount_Perc = cust.Special_Discount_Perc,
                        refused_Order_Perc = cust.Refused_Order_Perc,
                        available = cust.Available,
                        branch = cust.Branch.Name,
                        branch_Id = cust.Branch_Id,
                        role_Name = cust.Role.Name

                    };
                    customerVMs.Add(customerVM);
                }
                return View(customerVMs);
            }
        }

        public IActionResult GetCustomerByName(string name)
        {
            List<Customer> customers = CustRepo.GetByName(name);
            if (customers == null)
            {
                return Content("No Customer Here");
            }
            else
            {
                List<CustomerViewModel> customerVMs = new List<CustomerViewModel>();

                foreach (Customer cust in customers)
                {
                    CustomerViewModel customerVM = new CustomerViewModel()
                    {
                        id = cust.Id,
                        name = cust.Name,
                        email = cust.Email,
                        phone = cust.Phone,
                        address = cust.Address,
                        password = cust.Password,
                        store_Name = cust.Store_Name,
                        special_Discount_Perc = cust.Special_Discount_Perc,
                        refused_Order_Perc = cust.Refused_Order_Perc,
                        available = cust.Available,
                        branch = cust.Branch.Name,
                        branch_Id = cust.Branch_Id,
                        role_Name = cust.Role.Name

                    };
                    customerVMs.Add(customerVM);
                }
                return View(customerVMs);
            }
        }

        public IActionResult AddCustomer()
        {
            List<Branch> Branchs = BranchRepo.GetBranchesAvailable();
            CustomerAddEditViewModel CustVModel = new CustomerAddEditViewModel();
            CustVModel.branches_List = Branchs.ToList();
            return View(CustVModel);
        }


        [HttpPost]
        public ActionResult AddCustomer(CustomerAddEditViewModel CustDTO)
        {
            if (ModelState.IsValid == true)
            {
                Customer NewCust = new Customer()
                {
                    Name = CustDTO.name,
                    Email = CustDTO.email,
                    Password = CustDTO.password,
                    Phone = CustDTO.phone,
                    Address = CustDTO.address,
                    Store_Name = CustDTO.store_Name,
                    Special_Discount_Perc = CustDTO.special_Discount_Perc,
                    Refused_Order_Perc = CustDTO.refused_Order_Perc,
                    Role_Id = 3,
                    Available = true,
                    Branch_Id = CustDTO.branch_Id
                };
                CustRepo.Add(NewCust);
                CustRepo.Save();
                return RedirectToAction("GetAll");
            }
            else
            {
                List<Branch> Branchs = BranchRepo.GetBranchesAvailable();
                CustomerAddEditViewModel CustVModel = new CustomerAddEditViewModel();

                CustVModel.name = CustDTO.name;
                CustVModel.email = CustDTO.email;
                CustVModel.password = CustDTO.password;
                CustVModel.phone = CustDTO.phone;
                CustVModel.address = CustDTO.address;
                CustVModel.store_Name = CustDTO.store_Name;
                CustVModel.special_Discount_Perc = CustDTO.special_Discount_Perc;
                CustVModel.refused_Order_Perc = CustDTO.refused_Order_Perc;
                CustVModel.branch_Id = CustDTO.branch_Id;

                return View(CustVModel);
            }
        }

        public IActionResult EditCustomer(int Id)
        {
            Customer Cust = CustRepo.GetById(Id);
            List<Branch> Branchs = BranchRepo.GetBranchesAvailable();
            CustomerAddEditViewModel CustVModel = new CustomerAddEditViewModel();
            CustVModel.branches_List = Branchs.ToList();
            CustVModel.name = Cust.Name;
            CustVModel.email = Cust.Email;
            CustVModel.password = Cust.Password;
            CustVModel.phone = Cust.Phone;
            CustVModel.address = Cust.Address;
            CustVModel.role_Id = 3;
            CustVModel.store_Name = Cust.Store_Name;
            CustVModel.available = Cust.Available;
            CustVModel.special_Discount_Perc = Cust.Special_Discount_Perc;
            CustVModel.refused_Order_Perc = Cust.Refused_Order_Perc;
            CustVModel.branch_Id = (int)Cust.Branch_Id;
            ViewData["BranchList"] = BranchRepo.GetBranchesAvailable();
            return View(CustVModel);
        }

        [HttpPost]
        public ActionResult EditCustomer(CustomerAddEditViewModel CustDTO, int Id)
        {
            Customer Cust = CustRepo.GetById(Id);
            if (Cust == null)
            {
                return Content("No Customer Here");
            }
            if (ModelState.IsValid == true)
            {
                Cust.Name = CustDTO.name;
                Cust.Email = CustDTO.email;
                Cust.Password = CustDTO.password;
                Cust.Phone = CustDTO.phone;
                Cust.Address = CustDTO.address;
                Cust.Store_Name = CustDTO.store_Name;
                Cust.Special_Discount_Perc = CustDTO.special_Discount_Perc;
                Cust.Refused_Order_Perc = CustDTO.refused_Order_Perc;
                Cust.Role_Id = 3;
                Cust.Branch_Id = CustDTO.branch_Id;
                CustRepo.Edit(Cust);
                CustRepo.Save();
                return RedirectToAction("GetAll");
            }
            else
            {
                List<Branch> Branchs = BranchRepo.GetBranchesAvailable();
                CustomerAddEditViewModel CustVModel = new CustomerAddEditViewModel();
                CustVModel.branches_List = Branchs.ToList();
                CustVModel.name = CustDTO.name;
                CustVModel.email = CustDTO.email;
                CustVModel.password = CustDTO.password;
                CustVModel.phone = CustDTO.phone;
                CustVModel.address = CustDTO.address;
                CustVModel.store_Name = CustDTO.store_Name;
                CustVModel.special_Discount_Perc = CustDTO.special_Discount_Perc;
                CustVModel.refused_Order_Perc = CustDTO.refused_Order_Perc;
                CustVModel.branch_Id = CustDTO.branch_Id;
                ViewData["BranchList"] = BranchRepo.GetBranchesAvailable();
                return View(CustVModel);
            }
        }

        public IActionResult SoftDelete(int id)
        {
            Customer Cust = CustRepo.GetById(id);
            CustRepo.SoftDelete(Cust);
            CustRepo.Save();
            return RedirectToAction("GetAll");
        }

        public IActionResult Search(string name)
        {
            List<Customer> Custs = CustRepo.GetByName(name);
            List<CustomerViewModel> customerVMs = new List<CustomerViewModel>();

            foreach (Customer cust in Custs)
            {
                CustomerViewModel customerVM = new CustomerViewModel()
                {
                    id = cust.Id,
                    name = cust.Name,
                    email = cust.Email,
                    phone = cust.Phone,
                    address = cust.Address,
                    password = cust.Password,
                    store_Name = cust.Store_Name,
                    special_Discount_Perc = cust.Special_Discount_Perc,
                    refused_Order_Perc = cust.Refused_Order_Perc,
                    available = cust.Available,
                    branch = cust.Branch.Name,
                    branch_Id = cust.Branch_Id,
                    role_Name = cust.Role.Name

                };
                customerVMs.Add(customerVM);
            }
            return View("GetAll", customerVMs);
        }
    }
}
