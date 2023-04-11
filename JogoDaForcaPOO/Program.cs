using System.Linq;
using System.Collections.Generic;
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

    public class JogoDaForca
    {
        public List<string> Palavras { get; }
        public string PalavraSorteada { get; }
        public List<char> PalavraOculta { get; }
        public int Tentativas { get; private set; }

        public JogoDaForca()
        {
            this.Palavras = ListarPalavras();
            this.PalavraSorteada = ObterPalavraOculta();
            this.PalavraOculta = OcultarPalavra();
            this.Tentativas = 6;

        }

        public void LancarPalpite(char letra)
        {
            if (!VerificarAcertou(letra))
            {
                AtribuirErro();
            }
        }

        public List<char> MostrarPalavra()
        {
            return this.PalavraOculta;
        }

        public bool VerificarFimDeJogo()
        {
            return Tentativas == 0 ? true : false;
        }
        public bool VerificarPalavraCompleta()
        {
            var palavra = "";
            PalavraOculta.ForEach(i => palavra += i);

            return palavra == PalavraSorteada ? true : false;
        }

        public string MostrarMensagem(bool venceu)
        {
            return $"{(venceu ? "Parabéns voçe acertou a palavra" : "Deu forca... A palavra era")} {this.PalavraSorteada}";
        }

        private List<string> ListarPalavras()
        {
            return new List<string>(){ "ABACATE","ABACAXI","ACEROLA","AÇAÍ","ARAÇA","ABACATE","BACABA","BACURI","BANANA","CAJÁ","CAJÚ","CARAMBOLA","CUPUAÇU",
            "GRAVIOLA","GOIABA","JABUTICABA","JENIPAPO","MAÇÃ","MANGABA","MANGA","MARACUJÁ","MURICI","PEQUI","PITANGA","PITAYA",
            "SAPOTI","TANGERINA","UMBU","UVA","UVAIA"};
        }

        private string ObterPalavraOculta()
        {
            var random = new Random();
            return Palavras[random.Next(Palavras.Count)];
        }

        private List<char> OcultarPalavra()
        {
            var letrasOcultas = new List<char>();

            for (int i = 0; i < PalavraSorteada.Length; i++)
            {
                letrasOcultas.Add('_');
            }

            return letrasOcultas;

        }

        private bool VerificarAcertou(char letra)
        {
            bool acertou = false;

            for (int i = 0; i < PalavraSorteada.Length; i++)
            {
                if (PalavraSorteada[i] == letra)
                {
                    PalavraOculta[i] = letra;
                    acertou = true;
                }
            }
            return acertou;
        }

        private void AtribuirErro()
        {
            this.Tentativas--;
        }

    }
}