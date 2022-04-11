using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ControleMedicamentos.ConsoleApp.ModuloPaciente;
using ControleMedicamentos.ConsoleApp.ModuloRemedio;
using ControleMedicamentos.ConsoleApp.Compartilhado;

namespace ControleMedicamentos.ConsoleApp.ModuloRequisicao
{
    public class Requisicao : EntidadeBase
    {

        private readonly DateTime _dataHora;
        private readonly Paciente _paciente;
        private readonly Remedio _remedio;


        public Requisicao(DateTime dataHora, Paciente paciente, Remedio remedio)
        {
            _dataHora = dataHora;
            _paciente = paciente;
            _remedio = remedio;
        }

        public Remedio Remedio => _remedio;

        public override string ToString()
        {
            return "Id: " + id + Environment.NewLine +
               "data/hora: " + _dataHora + Environment.NewLine +
               "paciente: " + _paciente.Nome + Environment.NewLine +
               "Remedio: " + Remedio.Nome + Environment.NewLine;
        }

    }



}
