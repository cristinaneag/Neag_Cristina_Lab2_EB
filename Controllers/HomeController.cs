using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Neag_Cristina_Lab2_EB.Data;
using Neag_Cristina_Lab2_EB.Models.LibraryViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Neag_Cristina_Lab2_EB.Models;
using System.Threading.Tasks;
using System.Linq;

namespace Neag_Cristina_Lab2_EB.Controllers
{
    public class HomeController : Controller
    {
        private readonly LibraryContext _context;
        public HomeController(LibraryContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<ActionResult> Statistics()
        {
            IQueryable<OrderGroup> data =
                from order in _context.Orders
                group order by order.OrderDate into dateGroup
                select new OrderGroup()
                {
                    OrderDate = dateGroup.Key,
                    BookCount = dateGroup.Count()
                };
            return View(await data.AsNoTracking().ToListAsync());
        }

        public async Task<ActionResult> CustomersStatistics()
        {
            IQueryable<CustomerOrderGroup> data =
                from order in _context.Orders
                group order by order.Customer.Name into groupName
                select new CustomerOrderGroup()
                {
                    CustomerName = groupName.Key,
                    OrderCount = groupName.Count()
                };
            return View(await data.AsNoTracking().ToListAsync());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Chat()
        {
            return View();
        }
    }
}
