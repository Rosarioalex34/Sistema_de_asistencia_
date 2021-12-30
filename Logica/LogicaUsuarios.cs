using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ORUSCURSO.Logica
{
    public class LogicaUsuarios
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public byte[] Icono { get; set; }
        public string Estado { get; set; }
    }
}
