namespace JogoDaForca
{
    public class Program
    {
        static string[] Palavras =
        {
            "ABACATE","ABACAXI","ACEROLA","AÇAÍ","ARAÇA","ABACATE","BACABA","BACURI","BANANA","CAJÁ","CAJÚ","CARAMBOLA","CUPUAÇU",
            "GRAVIOLA","GOIABA","JABUTICABA","JENIPAPO","MAÇÃ","MANGABA","MANGA","MARACUJÁ","MURICI","PEQUI","PITANGA","PITAYA",
            "SAPOTI","TANGERINA","UMBU","UVA","UVAIA"
        };

        static int Tentativas = 0;
        static string? PalavraSorteada;
        static char LetraJogada;
        static char[]? PalavraOculta;

        static void Main(string[] args)
        {
            ApresentarJogo();
            InicializarJogo();
            Jogar();
        }


        static void Jogar()
        {
            while (Tentativas != 6)
            {
                CriarForca();
                MostrarPalavraOculta();

                bool acertou = SolicitarJogada();
                CalcularTentativas(acertou);

                if (VerificarPalavraCompleta())
                    if (!ConfirmarNovoJogo())
                        break;

                if (Tentativas == 6)
                    if (!ConfirmarNovoJogo())
                        break;

            }
        }

        static void InicializarJogo()
        {
            SortearPalavra();
            CriarArrayPalavraSorteada();
        }

        static bool ConfirmarNovoJogo()
        {
            Console.Write("\nDeseja Jogar Novamente? [1]Sim [2]Não\n=> ");
            string resposta = Console.ReadLine()!.ToUpper();
            if (resposta == "1")
            {
                ReiniciarJogo();
                InicializarJogo();
                return true;
            }
            return false;
        }


        static bool VerificarPalavraCompleta()
        {
            if (new string(PalavraOculta) == PalavraSorteada)
            {
                Console.Clear();
                Console.WriteLine($"Parabéns! Você acertou a palavra: {PalavraSorteada}\n");
                return true;
            }
            return false;
        }


        static void ReiniciarJogo()
        {
            PalavraOculta = null;
            PalavraSorteada = null;
            LetraJogada = default;
            Tentativas = default;
        }


        static void CalcularTentativas(bool acertou)
        {
            if (!acertou)
            {
                Tentativas++;
            }
            if (Tentativas == 6)
            {
                CriarForca();
                Console.WriteLine($"Fim de jogo! {Tentativas} tentativas efetuadas. A palavra era: {PalavraSorteada}\n");
            }
        }


        static bool SolicitarJogada()
        {
            Console.Write("\n\nInforme uma letra: ");

            string letra = Console.ReadLine()!.ToUpper();
            LetraJogada = Convert.ToChar(letra);

            bool acertou = false;

            return VerificarAcertou(LetraJogada, acertou);
        }


        static bool VerificarAcertou(char letra, bool acertou)
        {
            for (int i = 0; i < PalavraSorteada!.Length; i++)
            {
                if (PalavraSorteada[i] == letra)
                {
                    PalavraOculta![i] = letra;
                    acertou = true;
                }
            }
            return acertou;
        }


        static void MostrarPalavraOculta()
        {
            foreach (var item in PalavraOculta!)
            {
                Console.Write($"{item} ");
            }
        }



        static void CriarArrayPalavraSorteada()
        {
            PalavraOculta = new char[PalavraSorteada!.Length];

            for (int i = 0; i < PalavraOculta.Length; i++)
            {
                PalavraOculta[i] = '_';
            }
        }



        static void SortearPalavra()
        {
            Random random = new Random();
            PalavraSorteada = Palavras[random.Next(Palavras.Length)];
        }

        static void ApresentarJogo()
        {
            Console.Clear();
            string jogoDaForca = "\n\n--- Jogo Da Forca ---";

            for (int i = 0; i < jogoDaForca.Length; i++)
            {
                Console.CursorLeft += 1;
                Console.Write(jogoDaForca[i]);
                Console.ForegroundColor = (ConsoleColor)1 + i % 10;
                Thread.Sleep(100);
            }
            Thread.Sleep(200);
            Console.CursorLeft = 100;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("\n\n\n\nTecle para iniciar... ");
            Console.ReadKey();
        }



        static void CriarForca()
        {
            Console.Clear();
            var cabeca = Tentativas > 1 ? "O" : " ";
            var tronco = Tentativas > 2 ? "X" : " ";
            var bracoD = Tentativas > 3 ? "/" : " ";
            var bracoE = Tentativas > 4 ? "\\" : " ";
            var pernas = Tentativas > 5 ? "X" : " ";

            if (Tentativas == 0)
                Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\n");


            if (Tentativas > 0)
            {
                Console.WriteLine("_ _ _ _ _ _ _ _ ");
                Console.WriteLine("  |/        | ");
                Console.WriteLine("  |         {0}", cabeca);
                Console.WriteLine("  |        {0}{1}{2}", bracoD, tronco, bracoE);
                Console.WriteLine("  |         {0}", pernas);
                Console.WriteLine("  |");
                Console.WriteLine("  |");
                Console.WriteLine("  |");
                Console.WriteLine("  |");
                Console.WriteLine(" _|_ _ _ _  \n\n");
            }
        }

    }
}

