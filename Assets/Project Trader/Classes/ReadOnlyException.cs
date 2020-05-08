using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTrader
{
    public class ReadOnlyException : Exception
    {
        public ReadOnlyException() : this("읽기 전용 멤버에 접근했습니다.")
        {

        }
        public ReadOnlyException(string message) : base(message)
        {
        }

        public ReadOnlyException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
