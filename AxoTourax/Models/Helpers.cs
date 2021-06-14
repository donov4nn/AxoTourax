using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AxoTourax.Models
{
    public static class Helpers
    {
        public static string GetErrors(this IdentityResult error)
        {
            return String.Join(", " , error.Errors.Select(err => $"{err.Code} {err.Description}"));
        }
    }
}
