using System.Collections.Generic;

namespace DIO.Series.Interfaces
{
    public interface IRepositorio<T>
    {
        T RetornaPorId(int id);
        int ProximoId();
        bool VerificarExistePorId(int id);
        void Inserir(T entidade);
        void Atualizar(int id, T entidade);
        void Excluir(int id);
        List<T> Listar();
    }
}