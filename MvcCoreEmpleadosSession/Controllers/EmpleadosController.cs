using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MvcCoreEmpleadosSession.Extensions;
using MvcCoreEmpleadosSession.Models;
using MvcCoreEmpleadosSession.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreEmpleadosSession.Controllers
{
    public class EmpleadosController : Controller
    {
        private EmpleadosRepository repo;

        public EmpleadosController(EmpleadosRepository repo)
        {
            this.repo = repo;
        }
        public IActionResult SessionSalarios(int? salario)
        {
            if(salario != null)
            {
                int sumasalarial = 0;
                //SI YA ESTA EN SESSON COJO SU VALOR
                if (HttpContext.Session.GetString("SUMASALARIAL") != null)
                {
                    //RECUPERAMOS SU VALOR
                    sumasalarial = int.Parse(HttpContext.Session.GetString("SUMASALARIAL"));

                }
                //SUMAMOS EL SALARIO RECIBIDO CON LO QUE TENGAMOS YA ALMACENADO
                sumasalarial += salario.Value;
                //ALMACENAMOS EL NUEVO VALOR EN SESSION
                HttpContext.Session.SetString("SUMASALARIAL", sumasalarial.ToString());
                ViewData["MENSAJE"] = "Salario almacenado: " + salario.Value;
            }
            List<Empleado> emps = this.repo.GetEmpleados();
            return View(emps);
        }


        public IActionResult SumaSalarios()
        {
            return View();
        }
    

        public IActionResult SessionEmpleados(int? idempleado)
        {
            if (idempleado != null)
            {
                //BUSCAMOS EL EMPLEADO
                Empleado emp = this.repo.FindEmpleado(idempleado.Value);
                //NECESITAMOS ALMACENAR UN CONJUNTO DE EMPLEADOS
                List<Empleado> empleadosSession;

                //COMPROBAMOS SI EXISTE EMPLEADOS EN SESSION
                if (HttpContext.Session.GetObject<List<Empleado>>("EMPLEADOS") != null)
                {
                    empleadosSession = HttpContext.Session.GetObject<List<Empleado>>("EMPLEADOS");
                }
                else
                {
                    empleadosSession = new List<Empleado>();
                }
                //ALMACENAMOS EL EMPLEADO (tanto si existe la sesion como si no)
                empleadosSession.Add(emp);
                //ALMACENAMOS LOS DATOS EN SESSION
                HttpContext.Session.SetObject("EMPLEADOS", empleadosSession);
                ViewData["MENSAJE"] = "Empleado " + emp.No_Emp + ", " + emp.Apellido + " almacenado en Session";
            }
           
            return View(this.repo.GetEmpleados());
        }


        public IActionResult EmpleadosAlmacenados()
        {
            return View();
        }
    }
}
