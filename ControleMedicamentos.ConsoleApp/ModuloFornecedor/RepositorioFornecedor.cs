using ControleMedicamentos.ConsoleApp.Compartilhado;
using System;

namespace ControleMedicamentos.ConsoleApp.ModuloFornecedor
{
    public class RepositorioFornecedor : RepositorioBase<Fornecedor>
    {
        internal Fornecedor getFornecedor(int numeroFornecedor)
        {
            return Registros[numeroFornecedor];
        }
    }
}
