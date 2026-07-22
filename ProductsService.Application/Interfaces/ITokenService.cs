using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsService.Application.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(string userId, string role);
    }
}
