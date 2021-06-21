using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AxoTourax.Models
{
    public static class Helper
    {
        public static string GetErrors(this IdentityResult error)
        {
            return String.Join(", " , error.Errors.Select(err => $"{err.Code} {err.Description}"));
        }

        public static string GetErrors(this ModelStateDictionary modelState)
        {
            return string.Join("; " , modelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage));
        }
    }
}
