﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookRecommendation.Shared.Model
{
    public class AuthenticationRequest
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
