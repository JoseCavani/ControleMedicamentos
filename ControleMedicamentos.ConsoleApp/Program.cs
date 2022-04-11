using ControleMedicamentos.ConsoleApp.Compartilhado;
using System;
using ControleMedicamentos.ConsoleApp.ModuloRemedio;
using ControleMedicamentos.ConsoleApp.ModuloRequisicao;

namespace ControleMedicamentos.ConsoleApp
{

    // O PROGRAMA FAZ BAIXA AUTOMATICO DOS MEDICAMENTOS REQUISTADOS NÃO PRECISA EDITAR A QUANTIDADE DO MEDICAMENTO
    internal class Program
    {
        static void Main(string[] args)
        {
            Notificador notificador = new Notificador();
            TelaMenuPrincipal menuPrincipal = new TelaMenuPrincipal(notificador);

            while (true)
            {
                TelaBase telaSelecionada = menuPrincipal.ObterTela();

                if (telaSelecionada is null)
                    return;

                string opcaoSelecionada = telaSelecionada.MostrarOpcoes();

                if (telaSelecionada is ITelaCadastravel)
                {
                    ITelaCadastravel telaCadastravel = (ITelaCadastravel)telaSelecionada;

                    if (opcaoSelecionada == "1")
                        telaCadastravel.Inserir();

                    else if (opcaoSelecionada == "2")
                        telaCadastravel.Editar();

                    else if (opcaoSelecionada == "3")
                        telaCadastravel.Excluir();

                    else if (opcaoSelecionada == "4")
                        telaCadastravel.VisualizarRegistros("Tela");
                }
                if (telaSelecionada is TelaCadastroRemedio)
                {
                    TelaCadastroRemedio telaCadastravel = (TelaCadastroRemedio)telaSelecionada;

                    if (opcaoSelecionada == "1")
                        telaCadastravel.Inserir();

                    else if (opcaoSelecionada == "2")
                        telaCadastravel.Editar();

                    else if (opcaoSelecionada == "3")
                        telaCadastravel.Excluir();

                    else if (opcaoSelecionada == "4")
                        telaCadastravel.VisualizarRegistros("Tela");

                    else if (opcaoSelecionada == "5")
                        telaCadastravel.EmFalta();

                    else if (opcaoSelecionada == "6")
                        telaCadastravel.BaixaQuantidade();

                    else if (opcaoSelecionada == "7")
                        telaCadastravel.Reposicao();
                }
                if (telaSelecionada is TelaCadastroRequisicao)
                {
                    TelaCadastroRequisicao telaCadastravel = (TelaCadastroRequisicao)telaSelecionada;

                    if (opcaoSelecionada == "1")
                        telaCadastravel.Inserir();

                    else if (opcaoSelecionada == "2")
                        telaCadastravel.Editar();

                    else if (opcaoSelecionada == "3")
                        telaCadastravel.Excluir();

                    else if (opcaoSelecionada == "4")
                        telaCadastravel.VisualizarRegistros("Tela");

                    else if (opcaoSelecionada == "5")
                        telaCadastravel.MaisRequisitados();
                }
            }
        }
    }
}
