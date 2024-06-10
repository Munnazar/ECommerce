using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Models.Responces.Accounts
{
    public class AdminResponce
    {
        public string UserRole { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
