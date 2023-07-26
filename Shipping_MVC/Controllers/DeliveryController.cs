using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shipping_MVC.Models;
using Shipping_MVC.Repository.BranchRepo;
using Shipping_MVC.Repository.DeliveryRepo;
using Shipping_MVC.ViewModels;

namespace Shipping_MVC.Controllers
{
    [Authorize(Roles = "Admin")]

    public class DeliveryController : Controller
    {
        IDeliveryRepository deliveryrepo;
        IBranchRepository branchRepo;
        public DeliveryController(IDeliveryRepository deliveryrepo, IBranchRepository branchRepo)
        {
            this.deliveryrepo = deliveryrepo;
            this.branchRepo = branchRepo;
        }
        public IActionResult GetAllDelivries(string searchName)
        {
            List<Delivery> deliveries = string.IsNullOrEmpty(searchName) ? deliveryrepo.GetAllDeleveries() : deliveryrepo.GetByName(searchName);
            if (deliveries == null)
            {
                return Content("There are no deliveries");
            }

            List<DeliveryViewModel> deliveryViewModels = new List<DeliveryViewModel>();
            for (int i = 0; i < deliveries.Count; i++)
            {
                DeliveryViewModel delivery = new DeliveryViewModel()
                {
                    id = deliveries[i].Id,
                    name = deliveries[i].Name,
                    address = deliveries[i].Address,
                    phone = deliveries[i].Phone,
                    email = deliveries[i].Email,
                    branch_name = deliveries[i].Branch.Name,
                    available = deliveries[i].Available,
                };
                deliveryViewModels.Add(delivery);
            }
            return View(deliveryViewModels);
        }

        public ActionResult GetById(int id)
        {
            var delivery = deliveryrepo.GetById(id);
            DeliveryViewModel deliveryVM = new DeliveryViewModel()
            {
                id = delivery.Id,
                name = delivery.Name,
                address = delivery.Address,
                email = delivery.Email,
                phone = delivery.Phone,
                password = delivery.Password,
                branch_Id = delivery.Branch_Id,
                branch_name = delivery.Branch.Name,
                available = delivery.Available,
                companyPercentage = delivery.Company_Perc,
                role_Name = delivery.Role.Name
            };
            return View(deliveryVM);
        }
        public IActionResult getDeliveryName(string name)
        {
            List<Delivery> deliveries = deliveryrepo.GetByName(name);
            List<DeliveryViewModel> deliveryViews = new List<DeliveryViewModel>();
            for (int i = 0; i < deliveries.Count; i++)
            {
                DeliveryViewModel deliveryViewModel = new DeliveryViewModel()
                {
                    id = deliveries[i].Id,
                    name = deliveries[i].Name,
                    address = deliveries[i].Address,
                    password = deliveries[i].Password,
                    email = deliveries[i].Email,
                    branch_name = deliveries[i].Branch.Name,
                    role_Name = deliveries[i].Role.Name,
                    available = deliveries[i].Available
                };
                deliveryViews.Add(deliveryViewModel);
            }
            return View("GetAllDelivries",deliveryViews);
        }
        public ActionResult AddDelivery()

        {
            List<Branch> branches = branchRepo.GetBranchesAvailable();
            DeliveryAddEditViewModel addEditViewModel = new DeliveryAddEditViewModel();
            addEditViewModel.branches_List = branches.ToList();
            return View(addEditViewModel);
        }

        [HttpPost]
        public ActionResult AddDelivery(DeliveryAddEditViewModel deliveryDTO)
        {
            if (ModelState.IsValid == true)
            {
                var delivery = new Delivery
                {
                    Name = deliveryDTO.name,
                    Email = deliveryDTO.email,
                    Password = deliveryDTO.password,
                    Phone = deliveryDTO.phone,
                    Address = deliveryDTO.address,
                    Company_Perc = deliveryDTO.company_Perc,
                    Branch_Id = deliveryDTO.branch_Id,
                    Available = true,
                    Role_Id = 4,
                };
                deliveryrepo.Add(delivery);
                deliveryrepo.save();
                return RedirectToAction("GetAllDelivries");
            }
            else
            {
                List<Branch> Branchs = branchRepo.GetBranchesAvailable();
                DeliveryAddEditViewModel DeliveryVModel = new DeliveryAddEditViewModel();
                DeliveryVModel.branches_List = Branchs.ToList();
                DeliveryVModel.name = deliveryDTO.name;
                DeliveryVModel.email = deliveryDTO.email;
                DeliveryVModel.phone = deliveryDTO.phone;
                DeliveryVModel.address = deliveryDTO.address;
                DeliveryVModel.branch_Id = deliveryDTO.branch_Id;
                DeliveryVModel.available = true;
                DeliveryVModel.role_Id = 3;
                return View(DeliveryVModel);
            }

        }


        public IActionResult EditDelivery(int Id)
        {
            Delivery delivery = deliveryrepo.GetById(Id);
            List<Branch> Branchs = branchRepo.GetBranchesAvailable();
            DeliveryAddEditViewModel DevVModel = new DeliveryAddEditViewModel();
            DevVModel.branches_List = Branchs.ToList();
            DevVModel.name = delivery.Name;
            DevVModel.email = delivery.Email;
            DevVModel.phone = delivery.Phone;
            DevVModel.company_Perc = delivery.Company_Perc;
            DevVModel.address = delivery.Address;
            DevVModel.branch_Id = delivery.Branch_Id;
            return View(DevVModel);
        }

        [HttpPost]
        public ActionResult EditDelivery(DeliveryAddEditViewModel DeliveryDto, int Id)
        {
            Delivery delivery = deliveryrepo.GetById(Id);
            if (ModelState.IsValid == true)
            {
                delivery.Name = DeliveryDto.name;
                delivery.Address = DeliveryDto.address;
                delivery.Password = DeliveryDto.password;
                delivery.Phone = DeliveryDto.phone;
                delivery.Email = DeliveryDto.email;
                delivery.Branch_Id = DeliveryDto.branch_Id;
                delivery.Role_Id = 3;
                deliveryrepo.Edit(delivery);
                deliveryrepo.save();
                return RedirectToAction("GetAllDelivries");
            }
            else
            {
                List<Branch> Branchs = branchRepo.GetBranchesAvailable();
                DeliveryAddEditViewModel DeliveryVModel = new DeliveryAddEditViewModel();
                DeliveryVModel.branches_List = Branchs.ToList();
                DeliveryVModel.name = DeliveryDto.name;
                DeliveryVModel.email = DeliveryDto.email;
                DeliveryVModel.phone = DeliveryDto.phone;
                DeliveryVModel.address = DeliveryDto.address;
                DeliveryVModel.company_Perc = DeliveryDto.company_Perc;
                DeliveryVModel.branch_Id = DeliveryDto.branch_Id;
                return View(DeliveryVModel);
            }
        }

        public IActionResult SoftDelete(int id)
        {
            Delivery Delivery = deliveryrepo.GetById(id);
            deliveryrepo.SoftDelete(Delivery);
            deliveryrepo.save();
            return RedirectToAction("GetAllDelivries");
        }
    }
}
