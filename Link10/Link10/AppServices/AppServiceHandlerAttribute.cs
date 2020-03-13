using System;
using System.Collections.Generic;
using System.Text;

namespace Link10.AppServices
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public class AppServiceHandlerAttribute : Attribute
    {
        public string AppServiceName { get; set; }

        public bool IsNameContainsPostFix { get; set; }

        public AppServiceHandlerAttribute()
        {
        }
    }
}
