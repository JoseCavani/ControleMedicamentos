using System;
using System.Collections.Generic;

namespace ControleMedicamentos.ConsoleApp.Compartilhado
{
    public class RepositorioBase<T> : IRepositorio<T> where T : EntidadeBase
    {
        private readonly List<T> registros;

        protected int contadorId;

        protected List<T> Registros => registros;

        public RepositorioBase()
        {
            registros = new List<T>();
        }

        public virtual string Inserir(T entidade)
        {
            entidade.id = ++contadorId;

            Registros.Add(entidade);

            return "REGISTRO_VALIDO";
        }

        public bool Editar(int idSelecionado, T novaEntidade)
        {
            foreach (T entidade in Registros)
            {
                if (idSelecionado == entidade.id)
                {
                    novaEntidade.id = entidade.id;

                    int posicaoParaEditar = Registros.IndexOf(entidade);
                    Registros[posicaoParaEditar] = novaEntidade;

                    return true;
                }
            }

            return false;
        }

        public bool Editar(Predicate<T> condicao, T novaEntidade)
        {
            foreach (T entidade in Registros)
            {
                if (condicao(entidade))
                {
                    novaEntidade.id = entidade.id;

                    int posicaoParaEditar = Registros.IndexOf(entidade);
                    Registros[posicaoParaEditar] = novaEntidade;

                    return true;
                }
            }

            return false;
        }

        public bool Excluir(int idSelecionado)
        {
            foreach (T entidade in Registros)
            {
                if (idSelecionado == entidade.id)
                {
                    Registros.Remove(entidade);
                    return true;
                }
            }
            return false;
        }

        public bool Excluir(Predicate<T> condicao)
        {
            foreach (T entidade in Registros)
            {
                if (condicao(entidade))
                {
                    Registros.Remove(entidade);
                    return true;
                }
            }
            return false;
        }

        public T SelecionarRegistro(int idSelecionado)
        {
            foreach (T entidade in Registros)
            {
                if (idSelecionado == entidade.id)
                    return entidade;
            }

            return null;
        }

        public T SelecionarRegistro(Predicate<T> condicao)
        {
            foreach (T entidade in Registros)
            {
                if (condicao(entidade))
                    return entidade;
            }

            return null;
        }

        public List<T> SelecionarTodos()
        {
            return Registros;
        }

        public List<T> Filtrar(Predicate<T> condicao)
        {
            List<T> registrosFiltrados = new List<T>();

            foreach (T registro in Registros)
                if (condicao(registro))
                    registrosFiltrados.Add(registro);

            return registrosFiltrados;
        }

        public bool ExisteRegistro(int idSelecionado)
        {
            foreach (T entidade in Registros)
                if (idSelecionado == entidade.id)
                    return true;

            return false;
        }

        public bool ExisteRegistro(Predicate<T> condicao)
        {
            foreach (T entidade in Registros)
                if (condicao(entidade))
                    return true;

            return false;
        }
    }
}
