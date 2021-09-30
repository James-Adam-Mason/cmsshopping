using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CmsShoppingCart.Models
{
    public class Page
    {
        public int Id { get; set; }  // This is the Primary Key when Database is created
        [Required, MinLength(2, ErrorMessage ="Minimium length is 2")]
        
        public string Title { get; set; }
        
        public string Slug { get; set; }
        [Required, MinLength(4, ErrorMessage = "Minimium length is 4")]
        public string Content { get; set; }
        public int Sorting { get; set; }

        public static implicit operator PageRemoteAttribute(Page v)
        {
            throw new NotImplementedException();
        }
    }
}
