using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMVC.Models
{
    public class Departament
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Seller> Sellers { get; set; } = new List<Seller>();

        public Departament() { }

        public Departament(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public void AddSeller(Seller seller)
        {
            Sellers.Add(seller);
        }
        public void RemoveSeller(Seller seller)
        {
            Sellers.Remove(seller);
        }

        public double TotalSales(DateTime instant1, DateTime instant2)
        {
            return Sellers.Sum(s => s.TotalSales(instant1, instant2));
        }

    }
}
