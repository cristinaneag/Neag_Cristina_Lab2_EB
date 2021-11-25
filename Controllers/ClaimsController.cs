using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Neag_Cristina_Lab2_EB.Controllers
{
    [Authorize]
    public class ClaimsController : Controller
    {
        public ViewResult Index() => View(User?.Claims);
    }
}
