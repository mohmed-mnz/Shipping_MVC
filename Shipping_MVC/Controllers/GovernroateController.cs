using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shipping_MVC.Models;
using Shipping_MVC.Repository.BranchRepo;
using Shipping_MVC.Repository.CityRepo;
using Shipping_MVC.Repository.GovernroateRepo;
using Shipping_MVC.ViewModels;
using System.Data;

namespace Shipping_MVC.Controllers
{
    [Authorize(Roles = "Admin")]

    public class GovernroateController : Controller
    {
        IGovernroateRepository governRepo;
        IBranchRepository branchRepo;
        ICityRepository cityRepo;

        public GovernroateController(IGovernroateRepository _governRepo, IBranchRepository _branchRepo, ICityRepository _cityRepo)
        {
            this.governRepo = _governRepo;
            this.branchRepo = _branchRepo;
            this.cityRepo = _cityRepo;
        }

        public IActionResult All()
        {
            List<Governroate> governroates = governRepo.GetGovernorates();
            List<GovernroateVM> governroateVMs = new List<GovernroateVM>();
            foreach (var govern in governroates)
            {
                GovernroateVM governroateVM = new GovernroateVM()
                {
                    id = govern.Id,
                    name = govern.Name,
                    branch_Name = govern.Branch.Name,
                    available = govern.Branch.Available == false ? false : govern.Available,
                    branch_Id = govern.Branch_Id
                };
                governroateVMs.Add(governroateVM);
            }

            return View(governroateVMs);
        }

        [HttpPost]
        public IActionResult Add(GovernroateAddVM governroateAddVM)
        {
            if (ModelState.IsValid == true)
            {
                Governroate governroate = new Governroate()
                {
                    Id = governroateAddVM.id,
                    Name = governroateAddVM.name,
                    Available = true,
                    Branch_Id=governroateAddVM.branch_Id
                };
                governRepo.Add(governroate);
                governRepo.Save();
                return RedirectToAction("All");
            }
            return View(governroateAddVM);
        }

        [HttpGet]
        public IActionResult Add()
        {
            List<Branch> Branchs = branchRepo.GetBranchesAvailable();
            GovernroateAddVM governroateAddVM = new GovernroateAddVM();
            governroateAddVM.branches_List = Branchs.ToList();
            return View(governroateAddVM);
        }
        
        public IActionResult SoftDelete(int id)
        {
            Governroate governroate = governRepo.GetGovernorateById(id);
            governRepo.SoftDelete(governroate);
            governRepo.Save();
            List<City> cities = cityRepo.GetCitiesByGovernroate(id).ToList();
            foreach (var city in cities)
            {
                if (governroate.Available == false)
                {
                    city.Available = false;
                    cityRepo.Save();
                }
            }

            return RedirectToAction("All");
        }

        [HttpPost]
        public IActionResult Edit(GovernroateAddVM governroateAddVM, int id)
        {
            Governroate governroate = governRepo.GetGovernorateById(id);
            if (ModelState.IsValid == true)
            {
                governroate.Name = governroateAddVM.name;
                governroate.Branch_Id = governroateAddVM.branch_Id;
                governroate.Available = governroateAddVM.available;
                governRepo.Update(governroate);
                governRepo.Save();
                return RedirectToAction("All");
            } else
            {
                List<Branch> Branchs = branchRepo.GetBranchesAvailable();
                governroateAddVM.branches_List = Branchs.ToList();
                return View(governroateAddVM);
            }
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Governroate governroate = governRepo.GetGovernorateById(id);
            List<Branch> Branchs = branchRepo.GetBranchesAvailable();
            GovernroateAddVM governroateAddVM = new GovernroateAddVM()
            {
                name = governroate.Name,
                available = governroate.Available,
                branches_List = Branchs.ToList(),
                branch_Id = governroate.Branch_Id
            };
            return View(governroateAddVM);
        }
    
        public IActionResult Search(string name)
        {
            if (name != "")
            {
                List<Governroate> governroates = governRepo.GetGovernoratesByName(name);
                List<GovernroateVM> governroateVMs = new List<GovernroateVM>();
                foreach (var govern in governroates)
                {
                    GovernroateVM governroateVM = new GovernroateVM()
                    {
                        id = govern.Id,
                        name = govern.Name,
                        branch_Name = govern.Branch.Name,
                        available = govern.Branch.Available == false ? false : govern.Available,
                        branch_Id = govern.Branch_Id
                    };
                    governroateVMs.Add(governroateVM);
                }

                return View("All",governroateVMs);
            }
            else
            {
                List<Governroate> governroates = governRepo.GetGovernorates();
                List<GovernroateVM> governroateVMs = new List<GovernroateVM>();
                foreach (var govern in governroates)
                {
                    GovernroateVM governroateVM = new GovernroateVM()
                    {
                        id = govern.Id,
                        name = govern.Name,
                        branch_Name = govern.Branch.Name,
                        available = govern.Branch.Available == false ? false : govern.Available,
                        branch_Id = govern.Branch_Id
                    };
                    governroateVMs.Add(governroateVM);
                }

                return View("All", governroateVMs);
            }
        }
    }
}
