using System;
using System.Collections.Generic;
using System.Text;

namespace BlogCore.Application.Share.Interceptors
{
    [AttributeUsage(AttributeTargets.Method, Inherited = true)]
    public class OpsLogAttribute : Attribute
    {
        public string LogName { get; set; }
    }
}
