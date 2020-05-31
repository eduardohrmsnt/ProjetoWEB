using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesWebMVC.Models;
using SalesWebMVC.Models.Enums;

namespace SalesWebMVC.Data
{
    public class SeedingService
    {
        private SalesWebMVCContext _context;

        public SeedingService(SalesWebMVCContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            if(_context.Departament.Any() || _context.Seller.Any() || _context.SallesRecord.Any())
            {
                return; //DB is already seeded
            }

            Departament d1 = new Departament(1, "Computers");
            Departament d2 = new Departament(2, "Electronics");
            Departament d3 = new Departament(3, "Fashion");
            Departament d4 = new Departament(4, "Books");

            Seller sel1 = new Seller(1, "Eduardo Hermes", "eduardo@gmail.com", new DateTime(2000, 9, 13), 1000.00, d1);
            Seller sel2 = new Seller(2, "Amanda Hermes", "amanda@gmail.com", new DateTime(1999, 4, 27), 1000.00, d3);

            SallesRecord sale = new SallesRecord(1, new DateTime(2020, 5, 31), 11000.00, SaleStatus.Billed, sel1);

            _context.Departament.AddRange(d1, d2, d3, d4);
            _context.Seller.AddRange(sel1,sel2);
            _context.SallesRecord.AddRange(sale);

            _context.SaveChanges();

        }
    }
}
