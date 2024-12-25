using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Application.Wrapper
{
  
    public class Error
        {

        public string Message { get; }
        public Error(string message)
        {
                Message = message;
        }


        public static Error None
        {
            get { return new Error(string.Empty); }
        }

        public static implicit operator Error(string message)
        {
            return new Error(message);
        }

        public static implicit operator string(Error error)
        {
         return  error.Message;
        }
       
    }

   
}
