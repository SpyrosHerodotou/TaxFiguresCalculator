using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TaxFiguresCalculator.Core.Model.Entities;

namespace TaxFiguresCalculator.Core.Entities
{
    public partial class Transaction:BaseEntity
    {
        [Key]
        public long Id { get; set; }
        [Key]
        public string AccountId { get; set; }
        [MaxLength(8)]
        public decimal Amount { get; set; }
        [StringLength(350,MinimumLength = 3)]
        public string Description { get; set; }
        public string CurrencyCode { get; set; }

        public Account AccountNoNavigation { get; set; }

    }
}
