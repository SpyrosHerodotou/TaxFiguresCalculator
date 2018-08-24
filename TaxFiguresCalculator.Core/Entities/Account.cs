using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TaxFiguresCalculator.Core.Model.Entities;

namespace TaxFiguresCalculator.Core.Entities
{
    public partial class Account:BaseEntity
    {
        public Account()
        {
            Transactions = new HashSet<Transaction>();
        }

        [Key]
        public string Id { get; set; }
        public long customerId { get; set; }
        public string AccountType { get; set; }
        public string AccountDescription { get; set; }

        public Customer Customer { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
    }
}
