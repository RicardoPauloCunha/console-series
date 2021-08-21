using System;
using DIO.Series.Classes;
using DIO.Series.Enums;
using DIO.Series.Repositorios;

namespace DIO.Series
{
    class Program
    {
        static SerieRepositorio _serieRepositorio = new SerieRepositorio();

        static void Main(string[] args)
        {
            string opcaoUsuario = ObterOpcaoUsuario();

            while (opcaoUsuario.ToUpper() != "X")
            {
                switch (opcaoUsuario)
                {
                    case "1":
                        ListarSeries();
                        break;
                    case "2":
                        InserirSerie();
                        break;
                    case "3":
                        AtualizarSerie();
                        break;
                    case "4":
                        VisualizarSerie();
                        break;
                    case "5":
                        ExcluirSerie();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                opcaoUsuario = ObterOpcaoUsuario();
            }

            Console.WriteLine("Obrigado por utilizar nossos serviços.");
        }

        private static void ListarSeries()
        {
            Console.WriteLine("Listar séries");

            var lista = _serieRepositorio.Listar();

            if (lista.Count == 0)
            {
                Console.WriteLine("Nenhuma série cadastrada.");
                Pause();
                return;
            }

            foreach (var serie in lista)
            {
                var excluido = serie.RetornarExcluido();
                Console.WriteLine("[{0}] {1} {2}", serie.RetornarId(), serie.RetornarTitulo(), (excluido ? "(Excluído)" : ""));
            }
            Pause();
        }

        private static void InserirSerie()
        {
            Console.WriteLine("Inserir nova série");

            Serie novaSerie = ObterDadosSerie();
            novaSerie.DefinirId(_serieRepositorio.ProximoId());

            _serieRepositorio.Inserir(novaSerie);
        }

        private static void AtualizarSerie()
        {
            int serieId = ObterIdSerie();

            Console.Clear();
            Console.WriteLine("Série: {0}. Informe os novos valores:", _serieRepositorio.BuscarTituloPorId(serieId));
            Console.WriteLine("");

            Serie serieAtualizada = ObterDadosSerie();
            serieAtualizada.DefinirId(serieId);

            _serieRepositorio.Atualizar(serieId, serieAtualizada);
        }

        private static void ExcluirSerie()
        {
            int serieId = ObterIdSerie();

            Console.Clear();
            Console.WriteLine("Tem certeza que deseja excluir a série: {0}? [SIM/NAO]", _serieRepositorio.BuscarTituloPorId(serieId));
            string confirmarExclusao = Console.ReadLine();

            if (confirmarExclusao.ToLower() != "sim")
                return;

            _serieRepositorio.Excluir(serieId);

            Console.Clear();
            Console.WriteLine("Série excluida com sucesso.");
            Pause();
        }

        private static void VisualizarSerie()
        {
            int serieId = ObterIdSerie();

            Console.Clear();
            Console.WriteLine("Dados da série:");
            Console.WriteLine("");

            var serie = _serieRepositorio.RetornaPorId(serieId);

            Console.WriteLine(serie);
            Pause();
        }

        private static string ObterOpcaoUsuario()
        {
            Console.Clear();
            Console.WriteLine("DIO Séries a seu dispor!!!");
            Console.WriteLine("Informe a opção desejada:");

            Console.WriteLine("1 - Listar séries");
            Console.WriteLine("2 - Inserir nova série");
            Console.WriteLine("3 - Atualizar série");
            Console.WriteLine("4 - Visualizar série");
            Console.WriteLine("5 - Excluir série");
            Console.WriteLine("X - Sair");
            Console.WriteLine();

            string opcaoUsuario = Console.ReadLine().ToUpper();
            Console.WriteLine();
            Console.Clear();
            return opcaoUsuario;
        }

        private static Serie ObterDadosSerie()
        {
            foreach (int i in Enum.GetValues(typeof(Genero)))
            {
                Console.WriteLine("[{0}] {1}", i, Enum.GetName(typeof(Genero), i));
            }
            Console.Write("Digite o gênero entre as opções acima: ");
            Genero genero = (Genero)int.Parse(Console.ReadLine());

            Console.Write("Digite o título da série: ");
            string titulo = Console.ReadLine();

            Console.Write("Digite o ano de início da série: ");
            int ano = int.Parse(Console.ReadLine());

            Console.Write("Digite a descrição da série: ");
            string descricao = Console.ReadLine();

            return new Serie(genero, titulo, descricao, ano);
        }

        public static int ObterIdSerie()
        {
            bool serieExiste = false;
            int serieId = 0;

            do
            {
                Console.Write("Digite o id da série: ");
                serieId = int.Parse(Console.ReadLine());

                serieExiste = _serieRepositorio.VerificarExistePorId(serieId);
                if (!serieExiste)
                    Console.WriteLine("Série com o id {0} não encontrada", serieId);
            } while (!serieExiste);

            return serieId;
        }

        private static void Pause()
        {
            Console.WriteLine("");
            Console.WriteLine("Aperte qualquer tecla para continuar...");
            Console.ReadLine();
        }
    }
}
