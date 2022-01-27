using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreEmpleadosSession.Extensions
{
    public class Class1
    {
        private IHttpContextAccessor contextAccessor;

        public Class1(IHttpContextAccessor httpContextAccessor)
        {
            this.contextAccessor = httpContextAccessor;
        }
        public void Metodo()
        {
            this.contextAccessor.HttpContext.Session.SetString("KEY", "VALUE");
        }
    }
}
