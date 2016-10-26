﻿using MVCCore;
using System.Collections.Generic;
using GreenEffect.DomainObject.Customer;

namespace GreenEffect.Services.Interface
{
    public interface ICustomersRoutesServices
    {
        ServiceResult<ICollection<CustomerRoute>> GetByRoute(int RouteID);
    }
} 
