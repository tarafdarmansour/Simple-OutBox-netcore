﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountingService.Queries
{
    public class GetAccountingCustomerListQuery : IRequest<GetAccountingCustomerListResult>
    {
    }
}
