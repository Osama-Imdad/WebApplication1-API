﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetIdentity.Shared
{
    public class UserMangerResponse
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }

        public string? Token { get; set; }

        public IEnumerable<string> Errors { get; set; }
        
        public DateTime?ExpireDate { get; set; }    
    }
}