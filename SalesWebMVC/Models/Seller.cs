using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMVC.Models
{
    public class Seller
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public double  BaseSalary { get; set; }
        public Departament Departament { get; set; }
        public ICollection<SallesRecord> Sales { get; set; } = new List<SallesRecord>();

        public Seller()
        {

        }

        public Seller(int id, string name, string email, DateTime birthDate, double baseSalary, Departament departament)
        {
            Id = id;
            Name = name;
            Email = email;
            BirthDate = birthDate;
            BaseSalary = baseSalary;
            Departament = departament;
        }

        public void AddSales(SallesRecord sale)
        {
            Sales.Add(sale);
        }

        public void RemoveSales(SallesRecord sale)
        {
            Sales.Remove(sale);
        }

        public double TotalSales(DateTime instant1,DateTime instant2)
        {
            return Sales.Where(sr => sr.Date >= instant1 && sr.Date <= instant2).Sum(sr => sr.Amount);
        }
    }
}
