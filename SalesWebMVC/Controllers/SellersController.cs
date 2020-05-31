using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesWebMVC.Models;
using SalesWebMVC.Services;

namespace SalesWebMVC.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerServices _sel;

        public SellersController(SellerServices sel)
        {
            _sel = sel;
        }

        public IActionResult Index()
        {
            var list = _sel.ListSeller();
            return View(list);
        }

        public IActionResult Create()
        {
            return View();
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