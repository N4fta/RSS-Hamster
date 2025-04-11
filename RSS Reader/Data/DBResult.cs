using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class DBResult
    {
        public bool Success { get; }
        public string Message { get; }
        public Exception? Exception { get; }
        /* 
         * Exception Types:
         * 
         * SqlException - Generic Exception
         * 
         * InvalidCastException
         * 
         * InvalidOperationException
         * 
         * IOException - Error occured in Stream object
         * 
         */

        public DBResult(bool success, string message = "", Exception? ex = null)
        {
            Success = success;
            Message = message;
            Exception = ex;
        }
    }
}
