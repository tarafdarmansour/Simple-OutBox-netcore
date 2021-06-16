using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountingService.Models
{
    public class AccountingCustomer
    {
        public Guid Id { get; protected set; }
        public string FirstName { get; protected set; }
        public string LastName { get; protected set; }
        public int Balance { get; protected set; }
        public DateTime CreateDateTime { get; protected set; }
        public DateTime? UpdateDateTime { get; protected set; }
    }
}
