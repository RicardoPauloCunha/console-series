using System.Collections.Generic;
using DIO.Series.Classes;
using DIO.Series.Enums;
using DIO.Series.Interfaces;

namespace DIO.Series.Repositorios
{
    public class SerieRepositorio : IRepositorio<Serie>, ISerieRepositorio
    {
        private List<Serie> _series = new List<Serie>() {
            new Serie(0, Genero.Acao, "Fate I", "Fate Series", 2017),
            new Serie(1, Genero.Acao, "Fate II", "Fate Series", 2018),
            new Serie(2, Genero.Acao, "Fate III", "Fate Series", 2019),
            new Serie(3, Genero.Acao, "Fate IV", "Fate Series", 2020)
        };

        public Serie RetornaPorId(int id)
        {
            return _series[id];
        }

        public int ProximoId()
        {
            return _series.Count;
        }

        public bool VerificarExistePorId(int id)
        {
            return id < _series.Count;
        }

        public void Inserir(Serie objeto)
        {
            _series.Add(objeto);
        }

        public void Atualizar(int id, Serie objeto)
        {
            _series[id] = objeto;
        }

        public void Excluir(int id)
        {
            _series[id].Excluir();
        }

        public List<Serie> Listar()
        {
            return _series;
        }

        public string BuscarTituloPorId(int id)
        {
            return _series[id].RetornarTitulo();
        }
    }
}