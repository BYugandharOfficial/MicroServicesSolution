using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductsService.Application.Interfaces;

namespace ProductsService.Application.DTOs
{
    public class LoginDto
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
    }
}
