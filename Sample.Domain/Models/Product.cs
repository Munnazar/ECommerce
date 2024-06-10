using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Domain.Models
{
    public class Product 
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int Price { get; set; }
       
        public string ProductName { get; set; }
     
        public string Image { get; set; }     
      


        [ForeignKey("CatageryId")]
        [Required]
        public int CatageryId { get; set; }
        [NotMapped]
        public string CatageryName { get; set; }
        
    }
}
