using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ControleMedicamentos.ConsoleApp.Compartilhado;

namespace ControleMedicamentos.ConsoleApp.ModuloPaciente
{
    public class Paciente : EntidadeBase
    {

        private readonly string _nome;
        private readonly int _idade;


        public Paciente(string nome, int idade)
        {
            _nome = nome;
            _idade = idade;
        }

        public string Nome => _nome;

        public override string ToString()
        {
            return "Id: " + id + Environment.NewLine +
               "Nome: " + _nome + Environment.NewLine +
               "Idade: " + _idade + Environment.NewLine;
        }


    }
}
