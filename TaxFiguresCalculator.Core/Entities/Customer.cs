using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TaxFiguresCalculator.Core.Model.Entities;

namespace TaxFiguresCalculator.Core.Entities
{
    public partial class Customer:BaseEntity
    {
        public Customer()
        {
            Accounts = new HashSet<Account>();
        }
        [Key]
        public long Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Telephone { get; set; }
        public string EmailAddress { get; set; }

        public ICollection<Account> Accounts { get; set; }
    }
}
