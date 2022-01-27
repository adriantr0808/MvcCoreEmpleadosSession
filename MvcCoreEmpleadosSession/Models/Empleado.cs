using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreEmpleadosSession.Models
{
    [Serializable]
    [Table("EMP")]
    public class Empleado
    {
        [Key]
        [Column("EMP_NO")]
        public int No_Emp { get; set; }
        [Column("APELLIDO")]
        public string Apellido { get; set; }
        [Column("OFICIO")]
        public string Oficio { get; set; }
       [Column("SALARIO")]
       public int Salario { get; set; }
    }
}
