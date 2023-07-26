using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shipping_MVC.Models;
using Shipping_MVC.Repository.CityRepo;
using Shipping_MVC.Repository.GovernroateRepo;
using Shipping_MVC.ViewModels;
using System.Data;

namespace Shipping_MVC.Controllers
{
    [Authorize(Roles = "Admin")]

    public class CityController : Controller
    {
        ICityRepository cityRepo;
        IGovernroateRepository goverRepo;

        public CityController(ICityRepository _cityRepo, IGovernroateRepository _goverRepo)
        {
            this.cityRepo = _cityRepo;
            this.goverRepo = _goverRepo;
        }

        public IActionResult All()
        {
            List<City> cities = cityRepo.GetCities();
            List<CityVM> cityVMs = new List<CityVM>();
            foreach (var city in cities)
            {
                CityVM cityVM = new CityVM()
                {
                    id = city.Id,
                    name = city.Name,
                    charge_Regular = city.Charge_Regular,
                    charge_24Hour = city.Charge_24Hour,
                    charge_15Days = city.Charge_15Days,
                    charge_89Days = city.Charge_89Days,
                    governorate_Name = city.Governorate.Name,
                    governorate_Id = city.Governorate.Id,
                    branch_Name = city.Governorate.Branch.Name,
                    available = city.Governorate.Available == false ? false : city.Available
                };
                cityVMs.Add(cityVM);
            }

            return View(cityVMs);
        }

        [HttpPost]
        public IActionResult Add(CityAddVM cityAddVM)
        {
            if (ModelState.IsValid == true)
            {
                City city = new City()
                {
                    Name = cityAddVM.name,
                    Charge_Regular = cityAddVM.charge_Regular,
                    Charge_24Hour = cityAddVM.charge_24Hour,
                    Charge_15Days = cityAddVM.charge_15Days,
                    Charge_89Days = cityAddVM.charge_89Days,
                    Governorate_Id = cityAddVM.governorate_Id,
                    Available = true,
                };
                cityRepo.Add(city);
                cityRepo.Save();
                return RedirectToAction("All");
            }
            return View(cityAddVM);
        }

        [HttpGet]
        public IActionResult Add()
        {
            List<Governroate> governroates = goverRepo.GetGovernoratesAvailable();
            CityAddVM cityAddVM = new CityAddVM();
            cityAddVM.governroates = governroates.ToList();
            return View(cityAddVM);
        }
    
        public IActionResult SoftDelete(int id)
        {
            City city = cityRepo.GetCityById(id);
            if (city.Governorate.Available == true)
            {
                cityRepo.SoftDelete(city);
                cityRepo.Save();
            }
            return RedirectToAction("All");
        }
    
        public IActionResult Search(string name)
        {
            if (name != "")
            {
                List<City> cities = cityRepo.GetCitiesByName(name);
                List<CityVM> cityVMs = new List<CityVM>();
                foreach (var city in cities)
                {
                    CityVM cityVM = new CityVM()
                    {
                        id = city.Id,
                        name = city.Name,
                        charge_Regular = city.Charge_Regular,
                        charge_24Hour = city.Charge_24Hour,
                        charge_15Days = city.Charge_15Days,
                        charge_89Days = city.Charge_89Days,
                        governorate_Name = city.Governorate.Name,
                        governorate_Id = city.Governorate.Id,
                        branch_Name = city.Governorate.Branch.Name,
                        available = city.Governorate.Available == false ? false : city.Available
                    };
                    cityVMs.Add(cityVM);
                }

                return View("All", cityVMs);
            }
            else
            {
                List<City> cities = cityRepo.GetCities();
                List<CityVM> cityVMs = new List<CityVM>();
                foreach (var city in cities)
                {
                    CityVM cityVM = new CityVM()
                    {
                        id = city.Id,
                        name = city.Name,
                        charge_Regular = city.Charge_Regular,
                        charge_24Hour = city.Charge_24Hour,
                        charge_15Days = city.Charge_15Days,
                        charge_89Days = city.Charge_89Days,
                        governorate_Name = city.Governorate.Name,
                        governorate_Id = city.Governorate.Id,
                        branch_Name = city.Governorate.Branch.Name,
                        available = city.Governorate.Available == false ? false : city.Available
                    };
                    cityVMs.Add(cityVM);
                }

                return View("All", cityVMs);
            }
        }

        [HttpPost]
        public IActionResult Edit(CityAddVM cityAddVM,int id)
        {
            City city = cityRepo.GetCityById(id);

            if (ModelState.IsValid == true)
            {
                city.Name = cityAddVM.name;
                city.Governorate_Id = cityAddVM.governorate_Id;
                city.Available = cityAddVM.available;
                city.Charge_Regular = cityAddVM.charge_Regular;
                city.Charge_24Hour = cityAddVM.charge_24Hour;
                city.Charge_15Days = cityAddVM.charge_15Days;
                city.Charge_89Days = cityAddVM.charge_89Days;
                cityRepo.Update(city);
                cityRepo.Save();
                return RedirectToAction("All");
            }
            else
            {
                List<Governroate> governroates = goverRepo.GetGovernoratesAvailable();
                cityAddVM.governroates = governroates.ToList();
                return View(cityAddVM);
            }
        }
        
        
        [HttpGet]
        public IActionResult Edit(int id)
        {
            City city = cityRepo.GetCityById(id);
            List<Governroate> governroates = goverRepo.GetGovernoratesAvailable();
            CityAddVM cityAddVM = new CityAddVM()
            {
                name = city.Name,
                governorate_Id = city.Governorate_Id,
                available = city.Available,
                charge_Regular = city.Charge_Regular,
                charge_15Days = city.Charge_15Days,
                charge_24Hour = city.Charge_24Hour,
                charge_89Days = city.Charge_89Days,
                governroates = governroates.ToList()
            };
            return View(cityAddVM);

        }

    }
}
