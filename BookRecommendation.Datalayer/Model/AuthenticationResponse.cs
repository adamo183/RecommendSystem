﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookRecommendation.Datalayer.Model
{
    public class AuthenticationResponse
    {   
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Token { get; set; }
    }
}
