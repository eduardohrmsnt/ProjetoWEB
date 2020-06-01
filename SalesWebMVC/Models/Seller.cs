using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMVC.Models
{
    public class Seller
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo {0} Obrigatório")]
        [StringLength(0,MinimumLength =3,ErrorMessage ="O {0} tem que ter entre {2} e {1} letras!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Campo {0} Obrigatório")]
        [EmailAddress(ErrorMessage = "E-mail inválido!")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Campo {0} Obrigatório")]
        [Display(Name = "Birth Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "Campo {0} Obrigatório")]
        [Range(100.0,50000.0, ErrorMessage = "{0} tem que ser entre {1} e {2}")]
        [Display(Name = "Base Salary")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public double  BaseSalary { get; set; }

        public Departament Departament { get; set; }
        public int DepartamentId { get; set; }

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
