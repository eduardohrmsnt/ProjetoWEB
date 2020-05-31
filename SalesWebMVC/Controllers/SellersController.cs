using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesWebMVC.Models;
using SalesWebMVC.Services;
using SalesWebMVC.Models.ViewModels;

namespace SalesWebMVC.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerServices _sel;
        private readonly DepartamentService _depservice;
        public SellersController(SellerServices sel, DepartamentService depservice)
        {
            _sel = sel;
            _depservice = depservice;
        }

        public IActionResult Index()
        {
            var list = _sel.ListSeller();
            return View(list);
        }

        public IActionResult Create()
        {
            var departament = _depservice.FindAll();
            var viewmodel = new SellerFormViewModel { Departaments = departament}
            return View(viewmodel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Seller seller)
        {
            _sel.Insert(seller);
            return RedirectToAction(nameof(Index));
        }
    }
}