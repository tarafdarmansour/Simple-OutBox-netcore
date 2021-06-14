﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerService.Command
{
    public class DeleteCustomerCommand : IRequest<DeleteCustomerResult>
    {
        public Guid Id { get; set; }
    }
}
