using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ControleMedicamentos.ConsoleApp.Compartilhado;
using ControleMedicamentos.ConsoleApp.ModuloFornecedor;

namespace ControleMedicamentos.ConsoleApp.ModuloRemedio
{
    public class TelaCadastroRemedio : TelaBase
    {
        
        private readonly RepositorioRemedio _repositorioRemedio;
        private readonly Notificador _notificador;
        private int quantidade;
        private readonly TelaCadastroFornecedor _telaCadastroFornecedor;
        public RepositorioRemedio RepositorioRemedio => _repositorioRemedio;


        public TelaCadastroRemedio(RepositorioRemedio repositorioRemedio, Notificador notificador, TelaCadastroFornecedor telaCadastroFornecedor)
            : base("Cadastro de remedios")
        {
            _repositorioRemedio = repositorioRemedio;
            _notificador = notificador;
            _telaCadastroFornecedor = telaCadastroFornecedor;
        }

        public override string MostrarOpcoes()
        {
            MostrarTitulo(Titulo);

            Console.WriteLine("Digite 1 para Inserir");
            Console.WriteLine("Digite 2 para Editar");
            Console.WriteLine("Digite 3 para Excluir");
            Console.WriteLine("Digite 4 para Visualizar");
            Console.WriteLine("Digite 5 para Visualizar os em falta");
            Console.WriteLine("Digite 6 para Visualizar os com baixa quantidade");
            Console.WriteLine("Digite 7 para Solicitar reposição");
            Console.WriteLine("Digite s para sair");

            string opcao = Console.ReadLine();

            return opcao;
        }


        public void Inserir()
        {
            MostrarTitulo("Cadastro de Remedios");

            Remedio novoRemedio = ObterRemedio();

            RepositorioRemedio.Inserir(novoRemedio);

            _notificador.ApresentarMensagem("Remedio cadastrado com sucesso!", TipoMensagem.Sucesso);
        }

        public void Editar()
        {
            MostrarTitulo("Editando Remedio");

            bool temRemediosCadastrados = VisualizarRegistros("Pesquisando");

            if (temRemediosCadastrados == false)
            {
                _notificador.ApresentarMensagem("Nenhum remedio cadastrado para editar.", TipoMensagem.Atencao);
                return;
            }

            int numeroRemedio = ObterNumeroRegistro();

            Remedio remedioAtualizado = ObterRemedio();

            bool conseguiuEditar = RepositorioRemedio.Editar(numeroRemedio, remedioAtualizado);

            if (!conseguiuEditar)
                _notificador.ApresentarMensagem("Não foi possível editar.", TipoMensagem.Erro);
            else
                _notificador.ApresentarMensagem("Remedio editado com sucesso!", TipoMensagem.Sucesso);
        }

        internal void Reposicao()
        {
            MostrarTitulo("Repondo Remedio");

            bool temRemediosCadastrados = VisualizarRegistros("Pesquisando");

            if (temRemediosCadastrados == false)
            {
                _notificador.ApresentarMensagem("Nenhum remedio cadastrado para editar.", TipoMensagem.Atencao);
                return;
            }

            int numeroRemedio = ObterNumeroRegistro() - 1;


            _telaCadastroFornecedor.VisualizarRegistros("");
            int numeroFornecedor = _telaCadastroFornecedor.ObterNumeroRegistro() - 1;

           Fornecedor fornecedor = _telaCadastroFornecedor.RepositorioFornecedor.getFornecedor(numeroFornecedor);


            RepositorioRemedio.Reposicao(fornecedor, numeroRemedio);
            _notificador.ApresentarMensagem("Pedido de reposição feita.", TipoMensagem.Sucesso);
        }

        internal void BaixaQuantidade()
        {
            Console.WriteLine("as seguintes medicações estao com baixas quantidades");
            RepositorioRemedio.BaixaQuantidade();
        }

        public void Excluir()
        {
            MostrarTitulo("Excluindo Remedio");

            bool temRemediosRegistrados = VisualizarRegistros("Pesquisando");

            if (temRemediosRegistrados == false)
            {
                _notificador.ApresentarMensagem("Nenhum Remedios cadastrado para excluir.", TipoMensagem.Atencao);
                return;
            }

            int numeroRemedios = ObterNumeroRegistro();

            bool conseguiuExcluir = RepositorioRemedio.Excluir(numeroRemedios);

            if (!conseguiuExcluir)
                _notificador.ApresentarMensagem("Não foi possível excluir.", TipoMensagem.Erro);
            else
                _notificador.ApresentarMensagem("Remedio excluído com sucesso1", TipoMensagem.Sucesso);
        }

        public bool VisualizarRegistros(string tipoVisualizacao)
        {
            if (tipoVisualizacao == "Tela")
                MostrarTitulo("Visualização de Remedios");

            List<Remedio> remedios = RepositorioRemedio.SelecionarTodos();

            if (remedios.Count == 0)
            {
                _notificador.ApresentarMensagem("Nenhum Remedio disponível.", TipoMensagem.Atencao);
                return false;
            }

            foreach (Remedio remedio in remedios)
                Console.WriteLine(remedio.ToString());

            Console.ReadLine();

            return true;
        }

        private Remedio ObterRemedio()
        {
            Console.WriteLine("Digite o nome do remedio: ");
            string nome = Console.ReadLine();

            Console.WriteLine("Digite o descrição do remedio: ");
            string descricao = Console.ReadLine();
            do
            {
                Console.WriteLine("Digite a quantidade do remedio: ");
            }
           while(!(int.TryParse(Console.ReadLine(),out quantidade)));

            return new Remedio(nome, descricao, quantidade);
        }

        public int ObterNumeroRegistro()
        {
            int numeroRegistro;
            bool numeroRegistroEncontrado;

            do
            {
                Console.Write("Digite o ID do Remedio que deseja editar: ");
                numeroRegistro = Convert.ToInt32(Console.ReadLine());

                numeroRegistroEncontrado = RepositorioRemedio.ExisteRegistro(numeroRegistro);

                if (numeroRegistroEncontrado == false)
                    _notificador.ApresentarMensagem("ID do remedio não foi encontrado, digite novamente", TipoMensagem.Atencao);

            } while (numeroRegistroEncontrado == false);

            return numeroRegistro;
        }
        public void EmFalta()
        {
            Console.WriteLine("as seguintes medicações estao em falta");
            RepositorioRemedio.emFalta();
        }
    }

}

