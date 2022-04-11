using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ControleMedicamentos.ConsoleApp.Compartilhado;
using ControleMedicamentos.ConsoleApp.ModuloPaciente;
using ControleMedicamentos.ConsoleApp.ModuloRemedio;

namespace ControleMedicamentos.ConsoleApp.ModuloRequisicao
{
    public class TelaCadastroRequisicao : TelaBase
    {
        private readonly RepositorioRequisicao _repositorioRequisicao;
        private readonly Notificador _notificador;
        private readonly TelaCadastroRemedio _telaCadastroRemedio;
        private readonly TelaCadastroPaciente _telaCadastroPaciente;
        private DateTime _date;

        public TelaCadastroRequisicao(RepositorioRequisicao repositorioRequisicao, Notificador notificador, TelaCadastroPaciente telaCadastroPaciente, TelaCadastroRemedio telaCadastroRemedio)
            : base("Cadastro de Requisicao")
        {
            _repositorioRequisicao = repositorioRequisicao;
            _notificador = notificador;
            _telaCadastroRemedio = telaCadastroRemedio;
            _telaCadastroPaciente = telaCadastroPaciente;
        }

        public override string MostrarOpcoes()
        {
            MostrarTitulo(Titulo);

            Console.WriteLine("Digite 1 para Inserir");
            Console.WriteLine("Digite 2 para Editar");
            Console.WriteLine("Digite 3 para Excluir");
            Console.WriteLine("Digite 4 para Visualizar");
            Console.WriteLine("Digite 5 para Visualizar os mais requisitados");

            Console.WriteLine("Digite s para sair");

            string opcao = Console.ReadLine();

            return opcao;
        }


        public void Inserir()
        {
            MostrarTitulo("Cadastro de Remedios");

            Requisicao novoRequisicao = ObterRequisicao(out int numeroRemedio);
            if (novoRequisicao == null)
                return;

            _telaCadastroRemedio.RepositorioRemedio.DiminuirRemedio(numeroRemedio);

            _repositorioRequisicao.Inserir(novoRequisicao);

            _notificador.ApresentarMensagem("Requisicao cadastrado com sucesso!", TipoMensagem.Sucesso);
        }

        public void Editar()
        {
            MostrarTitulo("Editando Requisicao");

            bool temRequisicaoCadastrados = VisualizarRegistros("Pesquisando");

            if (temRequisicaoCadastrados == false)
            {
                _notificador.ApresentarMensagem("Nenhum Requisicao cadastrado para editar.", TipoMensagem.Atencao);
                return;
            }

            int numeroRequisicao = ObterNumeroRegistro();

            Requisicao requisicaoAtualizado = ObterRequisicao(out int numeroRemedio);// parametro inutil
            if (requisicaoAtualizado == null)
                return;

            bool conseguiuEditar = _repositorioRequisicao.Editar(numeroRequisicao, requisicaoAtualizado);

            if (!conseguiuEditar)
                _notificador.ApresentarMensagem("Não foi possível editar.", TipoMensagem.Erro);
            else
                _notificador.ApresentarMensagem("Requisicao editado com sucesso!", TipoMensagem.Sucesso);
        }

        public void MaisRequisitados()
        {
            _repositorioRequisicao.MaisRequistados();
        }

        public void Excluir()
        {
            MostrarTitulo("Excluindo Requisicao");

            bool temRequisicaoRegistrados = VisualizarRegistros("Pesquisando");

            if (temRequisicaoRegistrados == false)
            {
                _notificador.ApresentarMensagem("Nenhum Requisicao cadastrado para excluir.", TipoMensagem.Atencao);
                return;
            }

            int numeroRequisicao = ObterNumeroRegistro();

            bool conseguiuExcluir = _repositorioRequisicao.Excluir(numeroRequisicao);

            if (!conseguiuExcluir)
                _notificador.ApresentarMensagem("Não foi possível excluir.", TipoMensagem.Erro);
            else
                _notificador.ApresentarMensagem("Requisicao excluído com sucesso1", TipoMensagem.Sucesso);
        }


        public bool VisualizarRegistros(string tipoVisualizacao)
        {
            if (tipoVisualizacao == "Tela")
                MostrarTitulo("Visualização de Requisicao");

            List<Requisicao> requisicoes = _repositorioRequisicao.SelecionarTodos();

            if (requisicoes.Count == 0)
            {
                _notificador.ApresentarMensagem("Nenhum Requisicao disponível.", TipoMensagem.Atencao);
                return false;
            }

            foreach (Requisicao requisicao in requisicoes)
                Console.WriteLine(requisicao.ToString());

            Console.ReadLine();

            return true;
        }

        private Requisicao ObterRequisicao(out int numeroRemedio)
        {
            
            do
            {
              Console.WriteLine("Digite a data e hora da dia/mes/ano hora:min:segundo requisicao: ");
            }
            while (!(DateTime.TryParse(Console.ReadLine(), out _date)));

            bool temRemediosCadastrados = _telaCadastroRemedio.VisualizarRegistros("Pesquisando");
            numeroRemedio = _telaCadastroRemedio.ObterNumeroRegistro() -1;
            Remedio remedio = _telaCadastroRemedio.RepositorioRemedio.GetRemedio(numeroRemedio);

          

            if (temRemediosCadastrados == false)
            {
                _notificador.ApresentarMensagem("Nenhum remedio cadastrado", TipoMensagem.Atencao);
                return null;
            }


            if (_telaCadastroRemedio.RepositorioRemedio.VerficarDisponibilidade(numeroRemedio) == false)
            {
                _notificador.ApresentarMensagem("não ha esse medicamento", TipoMensagem.Erro);
                return null;
            }

        

            bool temPaciCadastrados = _telaCadastroPaciente.VisualizarRegistros("Pesquisando");

            if (temPaciCadastrados == false)
            {
                _notificador.ApresentarMensagem("Nenhum Paciente cadastrado", TipoMensagem.Atencao);
                return null;
            }



            int numeroPaciente = _telaCadastroPaciente.ObterNumeroRegistro()-1;
            Paciente paciente = _telaCadastroPaciente.RepositorioPaciente.GetPaciente(numeroPaciente);



            return new Requisicao(_date, paciente, remedio);
        }

        public int ObterNumeroRegistro()
        {
            int numeroRegistro;
            bool numeroRegistroEncontrado;

            do
            {
                Console.Write("Digite o ID da Requisição que deseja editar: ");
                numeroRegistro = Convert.ToInt32(Console.ReadLine());

                numeroRegistroEncontrado = _repositorioRequisicao.ExisteRegistro(numeroRegistro);

                if (numeroRegistroEncontrado == false)
                    _notificador.ApresentarMensagem("ID da requisição não foi encontrado, digite novamente", TipoMensagem.Atencao);

            } while (numeroRegistroEncontrado == false);

            return numeroRegistro;
        }
    }
}

