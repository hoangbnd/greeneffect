﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GreenEffect.Api.Models
{
    public class BasePagedModel<T> 
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public ICollection<T> Data { get; set; }
    }
}