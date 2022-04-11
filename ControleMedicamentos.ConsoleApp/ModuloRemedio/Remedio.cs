using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ControleMedicamentos.ConsoleApp.Compartilhado;

namespace ControleMedicamentos.ConsoleApp.ModuloRemedio
{
    public partial class Remedio : EntidadeBase
    {
        private readonly string _nome;
        private readonly string _descricao;
        private  int _quantidade;
        public bool teste;


        public Remedio(string nome, string descricao, int quantidade)
        {
            _nome = nome;
            _descricao = descricao;
            _quantidade = quantidade;
        }

        public string Nome => _nome;
        public int Quantidade { get => _quantidade; set => _quantidade = value; }

        public override string ToString()
        {
            return "Id: " + id + Environment.NewLine +
               "Nome: " + Nome + Environment.NewLine +
               "Descricao: " + _descricao + Environment.NewLine +
               "Quantidade: " + Quantidade + Environment.NewLine;
        }

    }
}
