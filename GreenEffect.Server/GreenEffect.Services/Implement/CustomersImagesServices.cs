using GreenEffect.DomainObject.CustomersImages;
using GreenEffect.Services.Interface;
using MVCCore;
using MVCCore.Data;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace GreenEffect.Services.Implement
{
    public class CustomersImagesServices : ICustomersImagesServices
    {
        private readonly IRepository<CustomersImages> _customersimagesRepository;
        public CustomersImagesServices(IRepository<CustomersImages> customersimagesRepository)
        {
            _customersimagesRepository = customersimagesRepository;
        }
        public ServiceResult<CustomersImages> Create(CustomersImages customersimages)
        {
            try
            {
                _customersimagesRepository.Insert(customersimages);
                return new ServiceResult<CustomersImages>(customersimages);
            }
            catch (Exception ex)
            {
                return new ServiceResult<CustomersImages>(new[]
                                                              {
                                                                  new RuleViolation("Ex",
                                                                                    "Insert data error:" +
                                                                                    ex.Message)
               });
            }
        }
    }
}
