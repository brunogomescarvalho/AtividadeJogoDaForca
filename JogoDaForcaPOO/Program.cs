namespace JogoDaForcaPOO
{
    public class Program
    {
        static bool Continuar = true;
        static JogoDaForca jogo = null!;
        static void Main(string[] args)
        {
            IniciarNovoJogo();

            while (Continuar)
            {
                MostrarPalavra(jogo);

                char letraPalpite = SolicitarJogada();

                jogo.LancarPalpite(letraPalpite);

                bool completou = jogo.VerificarPalavraCompleta();
                bool ehFimDeJogo = jogo.VerificarFimDeJogo();

                Console.Clear();

                if (completou)
                    Console.WriteLine(jogo.MostrarMensagem(true));

                else if (ehFimDeJogo)
                    Console.WriteLine(jogo.MostrarMensagem(false));

                if (completou || ehFimDeJogo)
                {
                    Continuar = ConfirmarNovoJogo();

                    if (Continuar)
                    {
                        IniciarNovoJogo();
                    }
                }
            }

        }

        static void IniciarNovoJogo()
        {
            jogo = new JogoDaForca();
        }

        static char SolicitarJogada()
        {
            Console.Write("\n\nInforme uma letra: ");
            return Convert.ToChar(Console.ReadLine()!.ToUpper());
        }

        static void MostrarPalavra(JogoDaForca jogo)
        {
            Console.Clear();

            var palavraOculta = jogo.MostrarPalavra();
            foreach (var item in palavraOculta)
            {
                Console.Write(item + " ");
            }

        }

        static bool ConfirmarNovoJogo()
        {
            Console.Write("\nDeseja Jogar Novamente? [1]Sim [2]Não\n=> ");
            return Console.ReadLine()! == "1" ? true : false;
        }
    }
}