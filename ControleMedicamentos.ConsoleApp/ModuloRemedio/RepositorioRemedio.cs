using ControleMedicamentos.ConsoleApp.Compartilhado;
using System;

namespace ControleMedicamentos.ConsoleApp.ModuloRemedio
{

    public class RepositorioRemedio : RepositorioBase<Remedio>
    {
       
        public Remedio GetRemedio(int numero)
        {
          return Registros[numero];
        }

        internal void emFalta()
        {
            foreach (Remedio remedio in Registros)
            {
                if (remedio.Quantidade  == 0)
                {
                    Console.WriteLine($"medicação :{remedio.Nome}");
                }
            }
        }
        internal void BaixaQuantidade()
        {
            foreach (Remedio remedio in Registros)
            {
                if (remedio.Quantidade < 5)
                    Console.WriteLine($"medicação :{remedio.Nome} com a quantidade de {remedio.Quantidade}");
                if (remedio.Fornecedor != null)
                    Console.WriteLine($"medicação :{remedio.Nome} Ja esta com pedido de reposição ao fornecedor");
            }
            Console.ReadKey();
        }

        internal bool VerficarDisponibilidade(int numero)
        {
            if (Registros[numero].Quantidade > 0)
                return true;
            return false;
        }
        public void DiminuirRemedio(int numero)
        {
            Registros[numero].Quantidade -= 1;
        }

        internal void Reposicao(ModuloFornecedor.Fornecedor fornecedor,int numero)
        {
            Registros[numero].Fornecedor = fornecedor;
        }
    }

}
