﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alea_Books_API.Web.RequestModels
{
    public class Authenticate_RequestModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
