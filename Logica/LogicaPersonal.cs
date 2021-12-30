using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sistema_de_asistencia.Logica
{
   public class LogicaPersonal
    {
		public int Id_personal { get; set; }
		public string Nombres { get; set; }
		public string Identificacion { get; set; }
		public string Pais { get; set; }
		public int Id_cargo { get; set; }
		public double SueldoPorHoras { get; set; }
		public string Estado { get; set; }
		public string codigo { get; set; }
	}
}
