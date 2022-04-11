using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ControleMedicamentos.ConsoleApp.Compartilhado;

namespace ControleMedicamentos.ConsoleApp.ModuloRequisicao
{

    public class RepositorioRequisicao : RepositorioBase<Requisicao>
    {
        int quantidade;
        internal void MaisRequistados()
        {
            foreach (Requisicao requisicao1 in Registros)
            {
                foreach (Requisicao requisicao2 in Registros)
                {
                    if (requisicao1.Remedio.Nome == requisicao2.Remedio.Nome && requisicao1.Remedio.teste == false)
                    {
                        quantidade++;
                    }
                }
                requisicao1.Remedio.teste = true;
                if (quantidade >0)
                Console.WriteLine($"medicamento : { requisicao1.Remedio.Nome} retirado {quantidade} vezes");
                quantidade = 0;
            }
            Console.ReadKey();
            foreach (Requisicao requisicao in Registros)
            {
                requisicao.Remedio.teste=false;
            }
        }
    }

}
