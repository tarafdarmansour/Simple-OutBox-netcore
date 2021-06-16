using System.Threading;
using System.Threading.Tasks;
using AccountingService.AccountingDBContext;
using AccountingService.Models;
using CustomerService.Events;
using MediatR;

namespace DashboardService.Listeners
{
    public class CustomerUpdatedHandler : INotificationHandler<CustomerUpdated>
    {
        private readonly AccountingContext _context;

        public CustomerUpdatedHandler(AccountingContext context)
        {
            _context = context;
        }

        public async Task Handle(CustomerUpdated notification, CancellationToken cancellationToken)
        {

            var customer = await _context.Customers.FindAsync(notification.Id);
            if (customer != null)
            {
                var accountingCustomer = new AccountingCustomer(notification);
                _context.Customers.Add(accountingCustomer);

                await _context.SaveChangesAsync(cancellationToken);
            }

        }
    }
}
