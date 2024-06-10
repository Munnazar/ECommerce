using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Models.Responces
{
    public class ApiResponce<T>
    {
        public string ErrorMessage { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Result { get; set; }

    }
}
