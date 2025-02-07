using Maintenance.Application.Wrapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Application.Common.Utility
{
    public static class Helper
    {
      
        #region Generate-Otp

        public static string GenerateNumericOtp(int length)
        {
            var random = new Random();
            var otp = new StringBuilder();

            for (int i = 0; i < length; i++)
            {
                otp.Append(random.Next(0, 10)); // Append a random digit (0-9)
            }

            return otp.ToString();
        }


        #endregion

        #region Process Result

        public static IActionResult ProcessResult<T>(Result<T> result)
        {

            if (result == null)
            {
                return new ObjectResult(new
                {
                    StatusCode = 500,
                    Success = false,
                    Message = "Internal Server Error: Result is null."
                })
                {
                    StatusCode = 500
                };
            }


            if (result.IsSuccess)
            {
                return new OkObjectResult(new
                {
                    StatusCode = result.StatusCode,
                    Success = true,
                    Message = result.Message,
                    Data = result.Value
                });
            }

            return new ObjectResult(new
            {
                StatusCode = result.StatusCode,
                Success = false,
                Message = result.Message
            })
            {
                StatusCode = result.StatusCode
            };
        }

        #endregion
    }
}
