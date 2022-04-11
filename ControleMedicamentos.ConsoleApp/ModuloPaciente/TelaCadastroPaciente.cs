using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ControleMedicamentos.ConsoleApp.Compartilhado;

namespace ControleMedicamentos.ConsoleApp.ModuloPaciente
{
    public class TelaCadastroPaciente : TelaBase, ITelaCadastravel
    {
        private readonly RepositorioPaciente _repositorioPaciente;
        private readonly Notificador _notificador;
        private int idade;

        public RepositorioPaciente RepositorioPaciente => _repositorioPaciente;

        public TelaCadastroPaciente(RepositorioPaciente repositorioPaceinte, Notificador notificador)
            : base("Cadastro de pacientes")
        {
            _repositorioPaciente = repositorioPaceinte;
            _notificador = notificador;
        }

        public void Inserir()
        {
            MostrarTitulo("Cadastro de Remedios");

            Paciente novoPaciente = ObterPaciente();

            RepositorioPaciente.Inserir(novoPaciente);

            _notificador.ApresentarMensagem("Paciente cadastrado com sucesso!", TipoMensagem.Sucesso);
        }

        public void Editar()
        {
            MostrarTitulo("Editando Remedio");

            bool temCadastrados = VisualizarRegistros("Pesquisando");

            if (temCadastrados == false)
            {
                _notificador.ApresentarMensagem("Nenhum paciente cadastrado para editar.", TipoMensagem.Atencao);
                return;
            }

            int numeroPaciente = ObterNumeroRegistro();

            Paciente remedioAtualizado = ObterPaciente();

            bool conseguiuEditar = RepositorioPaciente.Editar(numeroPaciente, remedioAtualizado);

            if (!conseguiuEditar)
                _notificador.ApresentarMensagem("Não foi possível editar.", TipoMensagem.Erro);
            else
                _notificador.ApresentarMensagem("Paciente editado com sucesso!", TipoMensagem.Sucesso);
        }

        public void Excluir()
        {
            MostrarTitulo("Excluindo Paciente");

            bool temRegistrados = VisualizarRegistros("Pesquisando");

            if (temRegistrados == false)
            {
                _notificador.ApresentarMensagem("Nenhum Paciente cadastrado para excluir.", TipoMensagem.Atencao);
                return;
            }

            int numeroPaciente = ObterNumeroRegistro();

            bool conseguiuExcluir = RepositorioPaciente.Excluir(numeroPaciente);

            if (!conseguiuExcluir)
                _notificador.ApresentarMensagem("Não foi possível excluir.", TipoMensagem.Erro);
            else
                _notificador.ApresentarMensagem("Paciente excluído com sucesso1", TipoMensagem.Sucesso);
        }


        public bool VisualizarRegistros(string tipoVisualizacao)
        {
            if (tipoVisualizacao == "Tela")
                MostrarTitulo("Visualização de Remedios");

            List<Paciente> remedios = RepositorioPaciente.SelecionarTodos();

            if (remedios.Count == 0)
            {
                _notificador.ApresentarMensagem("Nenhum Paciente disponível.", TipoMensagem.Atencao);
                return false;
            }

            foreach (Paciente remedio in remedios)
                Console.WriteLine(remedio.ToString());

            Console.ReadLine();

            return true;
        }

        private Paciente ObterPaciente()
        {
            Console.WriteLine("Digite o nome do paciente: ");
            string nome = Console.ReadLine();
            do
            {
                Console.WriteLine("Digite a idade do paciente: ");
            }
            while (!(int.TryParse(Console.ReadLine(), out idade)));

            return new Paciente(nome, idade);
        }

        public int ObterNumeroRegistro()
        {
            int numeroRegistro;
            bool numeroRegistroEncontrado;

            do
            {
                Console.Write("Digite o ID do Paciente que deseja editar: ");
                numeroRegistro = Convert.ToInt32(Console.ReadLine());

                numeroRegistroEncontrado = RepositorioPaciente.ExisteRegistro(numeroRegistro);

                if (numeroRegistroEncontrado == false)
                    _notificador.ApresentarMensagem("ID do Paciente não foi encontrado, digite novamente", TipoMensagem.Atencao);

            } while (numeroRegistroEncontrado == false);

            return numeroRegistro;
        }
    }

}

