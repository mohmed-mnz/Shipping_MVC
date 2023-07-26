using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shipping_MVC.Models;
using Shipping_MVC.Repository.BranchRepo;
using Shipping_MVC.Repository.GovernroateRepo;
using Shipping_MVC.ViewModels;

namespace Shipping_MVC.Controllers
{
    [Authorize(Roles ="Admin")]
    public class BranchController : Controller
    {

         IBranchRepository branchRepo;
        IGovernroateRepository governRepo;
        public BranchController(IBranchRepository _branchRepo, IGovernroateRepository _governRepo)
        {
            this.branchRepo = _branchRepo;
            this.governRepo = _governRepo;
        }

        [HttpGet]
        public IActionResult All()
        {
            List<Branch> branches = branchRepo.GetBranches();
            List<BranchVM> branchVMs = new List<BranchVM>();
            foreach (var branch in branches)
            {
                BranchVM branchVM = new BranchVM()
                {
                    id = branch.Id,
                    name = branch.Name,
                    available = branch.Available,
                };
                branchVMs.Add(branchVM);
            }

            return View(branchVMs);
        }
    
        public IActionResult Available()
        {
            List<Branch> branches = branchRepo.GetBranchesAvailable();
            List<BranchVM> branchVMs = new List<BranchVM>();
            foreach (var branch in branches)
            {
                BranchVM branchVM = new BranchVM()
                {
                    id = branch.Id,
                    name = branch.Name,
                    available = branch.Available,
                };
                branchVMs.Add(branchVM);
            }

            return View(branchVMs);
        }
        public IActionResult Search(string name)
        {
            if (name != "")
            {
                List<Branch> branches = branchRepo.GetBranchByName(name);
                List<BranchVM> branchVMs = new List<BranchVM>();
                foreach (var branch in branches)
                {
                    BranchVM branchVM = new BranchVM()
                    {
                        id = branch.Id,
                        name = branch.Name,
                        available = branch.Available,
                    };
                    branchVMs.Add(branchVM);
                }

                return View("All",branchVMs);
            } else
            {
                List<Branch> branches = branchRepo.GetBranches();
                List<BranchVM> branchVMs = new List<BranchVM>();
                foreach (var branch in branches)
                {
                    BranchVM branchVM = new BranchVM()
                    {
                        id = branch.Id,
                        name = branch.Name,
                        available = branch.Available,
                    };
                    branchVMs.Add(branchVM);
                }

                return View("All", branchVMs);
            }
        }

        [HttpPost]
        public IActionResult Add(BranchAddVM branchAddVM)
        {
            if (ModelState.IsValid == true) {

                Branch branch = new Branch()
                {
                    Id = branchAddVM.id,
                    Name = branchAddVM.name,
                    Available = true
                };
                branchRepo.Add(branch);
                branchRepo.Save();
                return RedirectToAction("All");
            }
            return View(branchAddVM);
        }

        [HttpGet]
        public IActionResult Add()
        {
            BranchAddVM branchAddVM = new BranchAddVM();
            return View(branchAddVM);
        }

        [HttpPost]
        public IActionResult Edit(BranchAddVM branchAddVM, int id)
        {
                Branch branch = branchRepo.GetBranchById(id);
            if (ModelState.IsValid == true) {
                branch.Name= branchAddVM.name;
                branchRepo.Update(branch);
                branchRepo.Save();
                return RedirectToAction("All");
            } else
            {
                return View(branchAddVM);
            }
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Branch branch = branchRepo.GetBranchById(id);
            BranchAddVM branchAddVM = new BranchAddVM()
            {
                name= branch.Name,
                available=branch.Available
            };
            return View(branchAddVM);
        }
    
        public IActionResult SoftDelete(int id)
        {
            Branch branch = branchRepo.GetBranchById(id);
            branchRepo.SoftDelete(branch);
            branchRepo.Save();
            List<Governroate> governorates = governRepo.GetGovernoratesByBranch(id).ToList();
            foreach (var govern in governorates)
            {
                if (branch.Available == false)
                {
                    govern.Available = false;
                    governRepo.Save();
                }
            }
            return RedirectToAction("All");
        }
    
    
        
    }
}
