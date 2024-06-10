using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Models.Requests.ContactUs
{
    public class CatageryRequest
    {
        [Required]
        public string Name { get; set; }
    }
}
