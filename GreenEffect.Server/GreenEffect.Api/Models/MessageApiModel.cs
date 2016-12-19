﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GreenEffect.Api.Models
{
    public class MessageApiModel 
    {
        public int Id { get; set; }
        public int FromId { get; set; }
        public string FromUserName { get; set; }
        public int[] ToIds { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public bool IsRead { get; set; }
        public string Content { get; set; }
        public DateTime DateTime { get; set; }
    }
}