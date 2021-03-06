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


        public IActionResult SessionEmpleadosCorrecto(int? idempleado)
        {
            if(idempleado != null)
            {
                List<int> listIdEmpleados;
                if (HttpContext.Session.GetString("IDSEMPLEADOS") == null)
                {
                    //NO EXISTE NADA EN SESSION, CREAMOS LA COLECCION
                    listIdEmpleados = new List<int>();
                }
                else
                {  
                    //EXISTE Y RECUPERAMOS LA COLECCION DE SESSION
                    listIdEmpleados = HttpContext.Session.GetObject<List<int>>("IDSEMPLEADOS");
                }

                    listIdEmpleados.Add(idempleado.Value);

                //ALMACENAMOS EL ID DENTRO DE LA COLECCION
                //ALMACENAMOS LA COLECCION DE NUEVO EN SESSION
                HttpContext.Session.SetObject("IDSEMPLEADOS", listIdEmpleados);
                ViewData["MENSAJE"] = "Empleados almacenados: " + listIdEmpleados.Count;
            }
            return View(this.repo.GetEmpleados());
        }

        public IActionResult EmpleadosAlmacenadosCorrecto(int? ideliminar)
        {
            List<int> listIdEmpleados = HttpContext.Session.GetObject<List<int>>("IDSEMPLEADOS");
            if (listIdEmpleados == null)
            {
                ViewData["MENSAJE"] = "No existen empleados almacenados";
                return View();
            }
            else
            {

                if(ideliminar != null)
                {
                    listIdEmpleados.Remove(ideliminar.Value);
                    if(listIdEmpleados.Count == 0)
                    {
                        //ESTA SENTENCIA ELIMINA LA SESION??
                       // HttpContext.Session.SetObject("IDSEMPLEADOS", null);
                        HttpContext.Session.Remove("IDSEMPLEADOS");
                    }
                    else
                    {
                        HttpContext.Session.SetObject("IDSEMPLEADOS", listIdEmpleados);
                    }
                    
                }
           
                //NECESITAMOS UN METODO EN EL REPO QUE LE ENVIAREMOS UNA COLECCION DE ID 
                //Y NOS DEVOLVERA LOS EMPLEADOS
            
                List<Empleado> empleados = this.repo.GetEmpledosSession(listIdEmpleados);

                return View(empleados);
            }
            
        }

        
        [HttpPost]
        public IActionResult EmpleadosAlmacenadosCorrecto(List<int>cantidades)
        {
            TempData.Put("PRODUCTOS", cantidades);
            return RedirectToAction("ResumenCompraEmpleados");

        }

        public IActionResult ResumenCompraEmpleados()
        {
            List<int> idsempleados = HttpContext.Session.GetObject<List<int>>("IDSEMPLEADOS");
            List<Empleado> emps = new List<Empleado>();
            foreach (int i in idsempleados)
            {
                Empleado emp = this.repo.FindEmpleado(i);
                emps.Add(emp);
            }

            ViewData["EMPLEADOS"] = emps;
            var cants = TempData.Get<List<int>>("PRODUCTOS");
            return View(cants);
        }
        



    }


}
