using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shipping_MVC.Models;
using Shipping_MVC.Repository.BranchRepo;
using Shipping_MVC.Repository.EmployeeRepo;
using Shipping_MVC.ViewModels;
using System.Data;

namespace Shipping_MVC.Controllers
{
    [Authorize(Roles = "Admin")]

    public class EmployeesController : Controller
    {
        IEmployeeRepository employeeRepo;
        IBranchRepository branchRepo;

        public EmployeesController(IEmployeeRepository _empRepo, IBranchRepository _branchRepo)
        {
            this.employeeRepo = _empRepo;
            this.branchRepo = _branchRepo;
        }
        public ActionResult GetAllEmployee()
        {
            List<Employee> emps = employeeRepo.getAll();
            List<EmployeeViewModel> employeeDTOs = new List<EmployeeViewModel>();
            for (int i = 0; i < emps.Count; i++)
            {
                EmployeeViewModel empDTO = new EmployeeViewModel()
                {
                    id = emps[i].Id,
                    name = emps[i].Name,
                    age = emps[i].Age,
                    address = emps[i].Address,
                    phone = emps[i].Phone,
                    email = emps[i].Email,
                    password = emps[i].Password,
                    branch_Name = emps[i].Branch.Name,
                    available = emps[i].Available,
                    role_Name = emps[i].Role.Name
                };
                employeeDTOs.Add(empDTO);
            }
            return View(employeeDTOs);
        }

        public IActionResult SoftDelete(int id)
        {
            Employee empl = employeeRepo.GetById(id);
            employeeRepo.SoftDelete(empl);
            employeeRepo.Save();
            return RedirectToAction("GetAllEmployee");
        }

        public IActionResult GetEmployeeByName(string name)
        {
            List<Employee> emps = employeeRepo.GetByName(name);
            List<EmployeeViewModel> employeeDTOs = new List<EmployeeViewModel>();
            for (int i = 0; i < emps.Count; i++)
            {
                EmployeeViewModel empDTO = new EmployeeViewModel()
                {
                    id = emps[i].Id,
                    name = emps[i].Name,
                    age = emps[i].Age,
                    address = emps[i].Address,
                    password = emps[i].Password,
                    email = emps[i].Email,
                    branch_Name = emps[i].Branch.Name,
                    role_Name = emps[i].Role.Name,
                    available = emps[i].Available
                };
                employeeDTOs.Add(empDTO);
            }
            return View("GetAllEmployee",employeeDTOs);
        }

        public IActionResult AddEmployee()
        {
            List<Branch> Branchs = branchRepo.GetBranchesAvailable();
            EmployeeAddEditViewModel EmpVModel = new EmployeeAddEditViewModel();
            EmpVModel.branches_List = Branchs.ToList();
            return View(EmpVModel);
        }

        [HttpPost]
        public ActionResult AddEmployee(EmployeeAddEditViewModel empDto)
        {
            if (ModelState.IsValid == true)
            {
                Employee NewEmp = new Employee()
                {
                    Email = empDto.email,
                    Name = empDto.name,
                    Address = empDto.address,
                    Age = empDto.age,
                    Available = true,
                    Branch_Id = empDto.branch_Id,
                    Role_Id = 2,
                    Phone = empDto.phone,
                    Password = empDto.password
                };
                employeeRepo.Add(NewEmp);
                employeeRepo.Save();
                return RedirectToAction("getAllEmployee");
            }
            else
            {
                List<Branch> Branchs = branchRepo.GetBranchesAvailable();
                EmployeeAddEditViewModel EmpVModel = new EmployeeAddEditViewModel();
                EmpVModel.branches_List = Branchs;
                EmpVModel.name = empDto.name;
                EmpVModel.email = empDto.email;
                EmpVModel.phone = empDto.phone;
                EmpVModel.address = empDto.address;
                EmpVModel.age = empDto.age;
                EmpVModel.branch_Id = empDto.branch_Id;
                return View(EmpVModel);
            }
        }

        public IActionResult EditEmployee(int id)
        {
            Employee emp = employeeRepo.GetById(id);
            List<Branch> Branchs = branchRepo.GetBranchesAvailable();
            EmployeeAddEditViewModel EmpVModel = new EmployeeAddEditViewModel();
            EmpVModel.branches_List = Branchs.ToList();
            EmpVModel.name = emp.Name;
            EmpVModel.email = emp.Email;
            EmpVModel.phone = emp.Phone;
            EmpVModel.address = emp.Address;
            EmpVModel.age = emp.Age;
            EmpVModel.branch_Id = emp.Branch_Id;
            return View(EmpVModel);
        }

        [HttpPost]
        public ActionResult EditEmployee(EmployeeAddEditViewModel empDto, int id)
        {
            Employee empl = employeeRepo.GetById(id);
            if (ModelState.IsValid == true)
            {
                empl.Name = empDto.name;
                empl.Address = empDto.address;
                empl.Password = empDto.password;
                empl.Phone = empDto.phone;
                empl.Email = empDto.email;
                empl.Age = empDto.age;
                empl.Branch_Id = empDto.branch_Id;
                empl.Role_Id = 2;
                employeeRepo.Edit(empl);
                employeeRepo.Save();
                return RedirectToAction("GetAllEmployee");
            }
            else
            {
                List<Branch> Branchs = branchRepo.GetBranchesAvailable();
                EmployeeAddEditViewModel EmpVModel = new EmployeeAddEditViewModel();
                EmpVModel.branches_List = Branchs.ToList();
                EmpVModel.branches_List = Branchs;
                EmpVModel.name = empDto.name;
                EmpVModel.email = empDto.email;
                EmpVModel.phone = empDto.phone;
                EmpVModel.address = empDto.address;
                EmpVModel.age = empDto.age;
                EmpVModel.branch_Id = empDto.branch_Id;
                return View(EmpVModel);
            }
        }
    }
}
