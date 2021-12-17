using Grpc.Net.Client;
using GrpcCustomersService;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Neag_Cristina_Lab2_EB.Controllers
{
    public class CustomersGrpcController : Controller
    {
        private readonly GrpcChannel channel;
        public CustomersGrpcController()
        {
            channel = GrpcChannel.ForAddress("https://localhost:5001");
        }
        [HttpGet]
        public IActionResult Index()
        {
            var client = new CustomerService.CustomerServiceClient(channel);
            CustomerList cust = client.GetAll(new Empty());
            return View(cust);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                var client = new
                CustomerService.CustomerServiceClient(channel);
                var createdCustomer = client.Insert(customer);
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        public IActionResult Edit(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var client = new
               CustomerService.CustomerServiceClient(channel);

            CustomerId idC = new CustomerId
            {
                Id = (int)id
            };
            Customer customer = client.Get(idC);

            if (customer == null)
            {
                return NotFound();
            }

            return View(new Neag_Cristina_Lab2_EB.Models.Customer()
            {
                CustomerID = customer.CustomerId,
                Name = customer.Name,
                Adress = customer.Adress,
                BirthDate = DateTime.Parse(customer.Birthdate)
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int? id, Customer customer)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = new
               CustomerService.CustomerServiceClient(channel);

            CustomerId idC = new CustomerId
            {
                Id = (int)id
            };
            Customer customer1 = client.Get(idC);

            if (customer1 == null)
            {
                return NotFound();
            }

            client.Update(customer);

            return RedirectToAction(nameof(Index));
        }

        public  IActionResult Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = new
               CustomerService.CustomerServiceClient(channel);

            CustomerId idC = new CustomerId
            {
                Id = (int)id
            };
            Customer customer = client.Get(idC);

            if (customer == null)
            {
                return NotFound();
            }

            return View(new Neag_Cristina_Lab2_EB.Models.Customer()
            {
                CustomerID = customer.CustomerId,
                Name = customer.Name,
                Adress = customer.Adress,
                BirthDate = DateTime.Parse(customer.Birthdate)
            });
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = new
               CustomerService.CustomerServiceClient(channel);

            CustomerId idC = new CustomerId
            {
                Id = (int)id
            };
            Customer customer1 = client.Get(idC);

            if (customer1 == null)
            {
                return NotFound();
            }

            client.Delete(idC);

            return RedirectToAction(nameof(Index));
        }
    }
}
