using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shipping_MVC.Models;
using Shipping_MVC.Repository.BranchRepo;
using Shipping_MVC.Repository.CityRepo;
using Shipping_MVC.Repository.DeliveryRepo;
using Shipping_MVC.Repository.GovernroateRepo;
using Shipping_MVC.Repository.OrderAdminRepo;
using Shipping_MVC.Repository.OrderCustomerRepo;
using Shipping_MVC.Repository.OrderDeliveryRepo;
using Shipping_MVC.Repository.OrderEmployeeRepo;
using Shipping_MVC.ViewModels;
using System.Data;

namespace Shipping_MVC.Controllers
{
    public class OrderController : Controller
    {
        IOrderAdminRepositorycs orderAdminRepo;
        IOrderCustomerRepository orderCustomerRepo;
        IOrderDeliveryRepository orderDeliverRepo;
        IOrderEmployeeRepository orderEmployeeRepo;
        IDeliveryRepository deliveryRepo;
        IBranchRepository BranchRepo;
        IGovernroateRepository GovernroateRepo;
        ICityRepository CityRepo;

        public OrderController(IOrderAdminRepositorycs _orderAdmin,IOrderCustomerRepository _orderCustomer,IOrderDeliveryRepository _orderDelivery,IOrderEmployeeRepository _orderEmp,IDeliveryRepository _delvRepo,IBranchRepository _branch,IGovernroateRepository _govern, ICityRepository _city)
        {
            this.orderAdminRepo = _orderAdmin;
            this.orderCustomerRepo = _orderCustomer;
            this.orderDeliverRepo= _orderDelivery;
            this.orderEmployeeRepo = _orderEmp;
            this.deliveryRepo = _delvRepo;
            this.BranchRepo = _branch;
            this.GovernroateRepo = _govern;
            this.CityRepo = _city;
        }

        [Authorize(Roles = "Admin")]

        public IActionResult All()
        {
            List<Order> orders = orderAdminRepo.GetOrders();
            List<OrderVM> orderVMs = new List<OrderVM>();
            foreach (var order in orders)
            {
                OrderVM orderVM = new OrderVM()
                {
                    id = order.Id,
                    customer_Name = order.Customer.Name,
                    customer_Phone = order.Customer.Phone,
                    client_Name = order.Client_Name,
                    client_Phone = order.Client_Phone,
                    client_Email = order.Client_Email,
                    delivery_Name = order.Delivery?.Name,
                    delivery_Phone = order.Delivery?.Phone,
                    delivery_Id=order.Delivery_Id,
                    order_Date = order.Order_Date,
                    order_Type = order.Order_Type,
                    charge_Type = order.Charge_Type,
                    payment_Type = order.Payment_Type,
                    total_Price = order.Total_Price,
                    total_Weight = order.Total_Weight,
                    status = order.Status,
                    branch_Name = order.Branch.Name,
                    governorate_Name = order.Governorate.Name,
                    city_Name = order.City.Name,
                    client_Village = order.Client_Village,
                    branch_Id = order.Branch.Id,
                    governorate_Id = order.Governorate.Id,
                    city_Id = order.City.Id,
                };
                orderVMs.Add(orderVM);
            }
            return View(orderVMs);
        }

        [Authorize(Roles = "Admin")]

        public IActionResult Report(string status, DateTime startDate,DateTime endDate) {
            List<Order> orders = orderAdminRepo.GetOrdersReport(status, startDate, endDate);
            List<OrderVM> orderVMs = new List<OrderVM>();
            foreach (var order in orders)
            {
                OrderVM orderVM = new OrderVM()
            {
                id = order.Id,
                customer_Name = order.Customer.Name,
                customer_Phone = order.Customer.Phone,
                client_Name = order.Client_Name,
                client_Phone = order.Client_Phone,
                client_Email = order.Client_Email,
                delivery_Name = order.Delivery?.Name,
                delivery_Phone = order.Delivery?.Phone,
                    order_Date = order.Order_Date,
                order_Type = order.Order_Type,
                charge_Type = order.Charge_Type,
                payment_Type = order.Payment_Type,
                total_Price = order.Total_Price,
                total_Weight = order.Total_Weight,
                status = order.Status,
                branch_Name = order.Branch.Name,
                governorate_Name = order.Governorate.Name,
                city_Name = order.City.Name,
                client_Village = order.Client_Village,
                branch_Id = order.Branch.Id,
                governorate_Id = order.Governorate.Id,
                city_Id = order.City.Id,
            };
                orderVMs.Add(orderVM);
        }
            return View("All",orderVMs);
        }

        [Authorize(Roles = "Customer")]

        public IActionResult OrderCustomer()
        {
            int customer_Id = 1;
            List<Order> orders = orderCustomerRepo.GetOrdersByCustomer(customer_Id);
            List<OrderVM> orderVMs = new List<OrderVM>();
            foreach (var order in orders)
            {
                OrderVM orderVM = new OrderVM()
                {
                    id = order.Id,
                    customer_Name = order.Customer.Name,
                    customer_Phone = order.Customer.Phone,
                    client_Name = order.Client_Name,
                    client_Phone = order.Client_Phone,
                    client_Email = order.Client_Email,
                    delivery_Id = order.Delivery_Id,
                    delivery_Name = order.Delivery?.Name,
                    delivery_Phone = order.Delivery?.Phone,
                    order_Date = order.Order_Date,
                    order_Type = order.Order_Type,
                    charge_Type = order.Charge_Type,
                    payment_Type = order.Payment_Type,
                    total_Price = order.Total_Price,
                    total_Weight = order.Total_Weight,
                    status = order.Status,
                    branch_Name = order.Branch.Name,
                    governorate_Name = order.Governorate.Name,
                    city_Name = order.City.Name,
                    client_Village = order.Client_Village,
                    branch_Id = order.Branch.Id,
                    governorate_Id = order.Governorate.Id,
                    city_Id = order.City.Id,
                };
                orderVMs.Add(orderVM);
            }
            return View(orderVMs);
        }

        [Authorize(Roles = "Delivery")]

        public IActionResult OrderDelivery()
        {
            int delivery_Id = 1;
            List<Order> orders = orderDeliverRepo.GetOrdersByDelivery(delivery_Id);
            List<OrderVM> orderVMs = new List<OrderVM>();
            foreach (var order in orders)
            {
                OrderVM orderVM = new OrderVM()
                {
                    id = order.Id,
                    customer_Name = order.Customer.Name,
                    customer_Phone = order.Customer.Phone,
                    client_Name = order.Client_Name,
                    client_Phone = order.Client_Phone,
                    delivery_Id = order.Delivery_Id,
                    client_Email = order.Client_Email,
                    delivery_Name = order.Delivery?.Name,
                    delivery_Phone = order.Delivery?.Phone,
                    order_Date = order.Order_Date,
                    order_Type = order.Order_Type,
                    charge_Type = order.Charge_Type,
                    payment_Type = order.Payment_Type,
                    total_Price = order.Total_Price,
                    total_Weight = order.Total_Weight,
                    status = order.Status,
                    branch_Name = order.Branch.Name,
                    governorate_Name = order.Governorate.Name,
                    city_Name = order.City.Name,
                    client_Village = order.Client_Village,
                    branch_Id = order.Branch.Id,
                    governorate_Id = order.Governorate.Id,
                    city_Id = order.City.Id,
                };
                orderVMs.Add(orderVM);
            }
            return View(orderVMs);
        }
    
        public IActionResult UpdateStatus(int id, string status)
        {
            Order order = orderDeliverRepo.GetOrderById(id);
            orderDeliverRepo.UpdateOrderStatus(order, status);
            orderDeliverRepo.Save();
            return RedirectToAction("OrderDelivery");
        }

        [Authorize(Roles = "Employee")]

        public IActionResult OrderEmployee()
        {
            int branch_Id = 1;
            List<Delivery> deliveries = deliveryRepo.GetDeliveriesAvailableByBranch(branch_Id);
            ViewBag.deliveries = deliveries.ToList();
            List<Order> orders = orderEmployeeRepo.GetOrdersByCustomerBranch(branch_Id);
            List<OrderVM> orderVMs = new List<OrderVM>();
            foreach (var order in orders)
            {
                OrderVM orderVM = new OrderVM()
                {
                    id = order.Id,
                    customer_Name = order.Customer.Name,
                    customer_Phone = order.Customer.Phone,
                    client_Name = order.Client_Name,
                    client_Phone = order.Client_Phone,
                    delivery_Id = order.Delivery_Id,
                    client_Email = order.Client_Email,
                    delivery_Name = order.Delivery?.Name,
                    delivery_Phone = order.Delivery?.Phone,
                    order_Date = order.Order_Date,
                    order_Type = order.Order_Type,
                    charge_Type = order.Charge_Type,
                    payment_Type = order.Payment_Type,
                    total_Price = order.Total_Price,
                    total_Weight = order.Total_Weight,
                    status = order.Status,
                    branch_Name = order.Branch.Name,
                    governorate_Name = order.Governorate.Name,
                    city_Name = order.City.Name,
                    client_Village = order.Client_Village,
                    branch_Id = order.Branch.Id,
                    governorate_Id = order.Governorate.Id,
                    city_Id = order.City.Id,
                };
                orderVMs.Add(orderVM);
            }
            return View(orderVMs);
        }
    

        public IActionResult UpdateDelivery(int id,int delivery_Id)
        {
            Order order = orderEmployeeRepo.GetOrderById(id);
            orderEmployeeRepo.UpdateOrderDeliveryId(order, delivery_Id);
            orderEmployeeRepo.Save();

            return RedirectToAction("OrderEmployee");
        }

        public IActionResult UpdateStatusEmp(int id, string status)
        {
            Order order = orderDeliverRepo.GetOrderById(id);
            orderDeliverRepo.UpdateOrderStatus(order, status);
            orderDeliverRepo.Save();
            return RedirectToAction("OrderEmployee");
        }

        public IActionResult AddOrder()
        {
            List<Branch> Branchs = BranchRepo.GetBranchesAvailable();
            List<City> Cities = CityRepo.GetCitiesAvailable();
            List<Governroate> Governorates = GovernroateRepo.GetGovernoratesAvailable();
            AddOrderCustomerViewModel OrederVM = new AddOrderCustomerViewModel();

            OrederVM.branches_List = Branchs.ToList();
            OrederVM.cities_List = Cities.ToList();
            OrederVM.governorates_List = Governorates.ToList();
            return View(OrederVM);
        }


        [HttpPost]
        public ActionResult AddOrder(AddOrderCustomerViewModel orderDO)
        {
            Order NewOrder = new Order()
            {
                Client_Name = orderDO.client_Name,
                Client_Email = orderDO.client_Email,
                Client_Phone = orderDO.client_Phone,
                Client_Village = orderDO.client_Village,
                Branch_Id = orderDO.branch_Id,
                Customer_Id = 1,
                Order_Date = DateTime.Now,
                Order_Type = orderDO.order_Type,
                Charge_Type = orderDO.charge_Type,
                Payment_Type = orderDO.payment_Type,
                Governorate_Id = orderDO.governorate_Id,
                City_Id = orderDO.city_Id,
                Status = "New",
                Total_Weight = orderDO.total_Weight,
                Total_Price = orderDO.total_Price,
            };
            orderCustomerRepo.AddOrder(NewOrder);
            orderCustomerRepo.Save();
            return RedirectToAction("OrderCustomer");


        }
    }
}
