﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Models.Requests.ContactUs
{ 
    public class ProductRequest
    {
        public int Price { get; set; }
        public string ProductName { get; set; }
        public int CatageryId { get; set; }
    }
}
