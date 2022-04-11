using ControleMedicamentos.ConsoleApp.ModuloFornecedor;
using ControleMedicamentos.ConsoleApp.ModuloFuncionario;
using ControleMedicamentos.ConsoleApp.ModuloRemedio;
using ControleMedicamentos.ConsoleApp.ModuloPaciente;
using ControleMedicamentos.ConsoleApp.ModuloRequisicao;
using System;

namespace ControleMedicamentos.ConsoleApp.Compartilhado
{
    public class TelaMenuPrincipal
    {
        private RepositorioFuncionario repositorioFuncionario;
        private TelaCadastroFuncionario telaCadastroFuncionario;

        private RepositorioFornecedor repositorioFornecedor;
        private TelaCadastroFornecedor telaCadastroFornecedor;

        private RepositorioRemedio repositorioRemedio;
        private TelaCadastroRemedio telaCadastroRemedio;

        private RepositorioPaciente repositorioPaciente;
        private TelaCadastroPaciente telaCadastroPaciente;

        private RepositorioRequisicao repositorioRequisicao;
        private TelaCadastroRequisicao telaCadastroRequisicao;

        public TelaMenuPrincipal(Notificador notificador)
        {
            repositorioFuncionario = new RepositorioFuncionario();
            telaCadastroFuncionario = new TelaCadastroFuncionario(repositorioFuncionario, notificador);

  repositorioFornecedor = new RepositorioFornecedor();
            telaCadastroFornecedor = new TelaCadastroFornecedor(repositorioFornecedor, notificador);

            repositorioRemedio = new RepositorioRemedio();
            telaCadastroRemedio = new TelaCadastroRemedio(repositorioRemedio, notificador, telaCadastroFornecedor);

          

            repositorioPaciente = new RepositorioPaciente();
            telaCadastroPaciente = new TelaCadastroPaciente(repositorioPaciente, notificador);

            repositorioRequisicao = new RepositorioRequisicao();
            telaCadastroRequisicao = new TelaCadastroRequisicao(repositorioRequisicao, notificador, telaCadastroPaciente, telaCadastroRemedio);
        }

        public string MostrarOpcoes()
        {
            Console.Clear();

            Console.WriteLine("Controle de Retirada de Medicamentos 1.0");

            Console.WriteLine();

            Console.WriteLine("Digite 1 para Gerenciar Funcionários");
            Console.WriteLine("Digite 2 para Gerenciar Fornecedores");
            Console.WriteLine("Digite 3 para Gerenciar Remedios");
            Console.WriteLine("Digite 4 para Gerenciar Pacientes");
            Console.WriteLine("Digite 5 para Gerenciar Requisicoes");


            Console.WriteLine("Digite s para sair");

            string opcaoSelecionada = Console.ReadLine();

            return opcaoSelecionada;
        }

        public TelaBase ObterTela()
        {
            string opcao = MostrarOpcoes();

            TelaBase tela = null;

            if (opcao == "1")
                tela = telaCadastroFuncionario;

            else if (opcao == "2")
                tela = telaCadastroFornecedor;

            else if (opcao == "3")
                tela = telaCadastroRemedio;

            else if (opcao == "4")
                tela = telaCadastroPaciente;
            else if (opcao == "5")
                tela = telaCadastroRequisicao;

            return tela;
        }
    }
}
