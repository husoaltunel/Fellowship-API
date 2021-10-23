using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Extensions
{
    public static class DateTimeExtensions
    {
        public static int CalculateAge(this DateTime? dateOfBirth)
        {
            var age = DateTime.Now.Year - dateOfBirth.Value.Year;
            if(DateTime.Now.Month < dateOfBirth.Value.Month)
                age--;
            return age;
        }
    }
}
