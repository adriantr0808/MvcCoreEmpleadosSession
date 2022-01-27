using MvcCoreEmpleadosSession.Data;
using MvcCoreEmpleadosSession.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreEmpleadosSession.Repositories
{

    public class EmpleadosRepository
    {

        private EmpleadosContext context;

        public EmpleadosRepository(EmpleadosContext context)
        {
            this.context = context;
        }

        public List<Empleado> GetEmpleados()
        {
            var consulta = from datos in this.context.Empleados
                           select datos;

            return consulta.ToList();
        }
    }
}
