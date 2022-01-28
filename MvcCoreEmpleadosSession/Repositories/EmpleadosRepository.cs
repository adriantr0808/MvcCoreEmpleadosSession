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
        public Empleado FindEmpleado(int idempleado)
        {
            return this.context.Empleados.SingleOrDefault(x => x.No_Emp == idempleado);
        }


        public List<Empleado> GetEmpledosSession(List<int> idsEmpleados)
        {
            //CUANDO UTILIZAMOS BUSQUEDA EN COLECCIONES SE UTILIZA EL METODO Contains
            //EL METODO Contains
            var consulta = from datos in this.context.Empleados
                           where idsEmpleados.Contains(datos.No_Emp)
                           select datos;

            if (consulta.Count()==0)
            {
                return null;
            }
            else
            {
                return consulta.ToList();
            }

           
        }
    }
}
