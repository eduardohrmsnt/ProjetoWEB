using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesWebMVC.Models;
using SalesWebMVC.Services;
using SalesWebMVC.Models.ViewModels;
using SalesWebMVC.Services.Exceptions;
using System.Diagnostics;

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

        public async Task<IActionResult> Index()
        {
            var list = _sel.ListSellerAsync();
            return View(list);
        }

        public async Task<IActionResult> Create()
        {
            var departament = await _depservice.FindAllAsync();
            var viewmodel = new SellerFormViewModel { Departaments = departament };
            return View(viewmodel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Seller seller)
        {
            if (!ModelState.IsValid)
            {
                var departament = await _depservice.FindAllAsync();
                SellerFormViewModel seller1 = new SellerFormViewModel { Departaments = departament, Seller = seller };
                return View(seller);
            }
            await _sel.InsertAsync(seller);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if(id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }
            var obj =  await _sel.FindByIdAsync(id.Value);
            if(obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            return View(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _sel.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }
            var obj = await _sel.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            return View(obj);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if(id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            var obj = await _sel.FindByIdAsync(id.Value);
            if(obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            List<Departament> departaments = await _depservice.FindAllAsync();
            SellerFormViewModel sellerFormView = new SellerFormViewModel { Seller = obj, Departaments = departaments };

            return View(sellerFormView);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Seller sel)
        {
            if (!ModelState.IsValid)
            {
                var departament = await _depservice.FindAllAsync();
                SellerFormViewModel seller = new SellerFormViewModel { Departaments = departament, Seller = sel };
                return View(seller);
            }
            if(id != sel.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id mismatch" });
            }
            try
            {
                await _sel.UpdateAsync(sel);
                return RedirectToAction(nameof(Index));
            }
            catch(NotFoundException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
            catch (DbConcurrencyException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };

            return View(viewModel);
        }
    }
}