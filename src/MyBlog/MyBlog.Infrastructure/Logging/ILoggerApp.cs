using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Infrastructure.Logging
{
    public interface ILoggerApp<T>
    {
         void LogWarning(string message, params object[] args);
        void LogInformation(string message, params object[] args);
    }
}
