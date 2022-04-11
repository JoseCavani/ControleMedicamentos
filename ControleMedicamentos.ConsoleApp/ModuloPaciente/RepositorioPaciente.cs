using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ControleMedicamentos.ConsoleApp.Compartilhado;
using ControleMedicamentos.ConsoleApp.ModuloRemedio;

namespace ControleMedicamentos.ConsoleApp.ModuloPaciente
{
    public class RepositorioPaciente : RepositorioBase<Paciente>
    {
        internal Paciente GetPaciente(int numero)
        {
            return Registros[numero];
        }
    }
}
