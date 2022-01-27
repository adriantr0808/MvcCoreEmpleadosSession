using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Index()

        {
            List<Empleado> emps = this.repo.GetEmpleados();
            return View(emps);
        }
    }
}
