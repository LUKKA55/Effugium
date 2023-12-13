namespace jogoInicial
{
    public enum Direcao {
        Cima = 1,
        Esquerda,
        Baixo,
        Direita
    }
    public class Game
    {
        public static int nivelFase = 0;
        public static FaseStatus FaseAtual = DB.fases[nivelFase];
        public static float dificuldade;
        public static ConsoleKeyInfo key;
        public static bool pararRenderizacoes = false;

        public static void CheckProximaFase() {
            bool todosInimigoMortos = new List<int>{ 
                FaseAtual._qntInimigosTipo1, 
                FaseAtual._qntInimigosTipo2, 
                FaseAtual._qntInimigosTipo3,
                FaseAtual._qntInimigosTipo4,
                FaseAtual._qntInimigosTipo5
            }.TrueForAll(
                (qntInimigo) => qntInimigo <= 0
            );

            if (todosInimigoMortos) {
                if (nivelFase == DB.fases.Count() - 1) {
                    Vitoria();
                } else {
                    ProximaFase();
                }
            }
        }

        public static async Task ProximaFase() {
            nivelFase += 1;
            pararRenderizacoes = true;

            char[] load = new char[40];
            for(int i = 0; i < load.Length; i++){
                load[i] = '.';

                MostrarMensagem.NextLevel();
                Console.Write(load);

                await Task.Delay(75);
            } 
            pararRenderizacoes = false;
            
            FaseAtual = DB.fases[nivelFase];
            Personagem.ResetPosicaoPersonagem();
            Mapa.ResetaSpawnsItensDoMapa();
            Mapa.CheckMapaIsRenderizando();
        }

        public static void Vitoria() {
            Mapa.CheckMapaIsRenderizando();
            MostrarMensagem.Win();
            Environment.Exit(0);
        }

        public static string[,] GetMapa() {
            return FaseAtual._mapa;
        }

        public static void Main() {
            Console.Clear();
            Console.WriteLine("Jogo inicializado");
            Console.WriteLine("Aperte X para encerrar");
            start:
            MostrarMensagem.Start();
            ConsoleKeyInfo respostaUsuario = Console.ReadKey();
            
            int dificuldadeExcolhida = respostaUsuario.KeyChar - '0';

            if (
                !char.IsDigit(respostaUsuario.KeyChar) ||
                new List<int>{1, 2, 3}
                    .FindIndex(d => d == dificuldadeExcolhida) 
                == -1
            ) {
                MostrarMensagem.ErrorDifficulty();
                goto start;
            }

            switch (dificuldadeExcolhida) {
                case 1:
                    dificuldade = 1f;
                break;
                case 2:
                    dificuldade = 0.8f;
                break;
                case 3:
                    dificuldade = 0.5f;
                break;
            }
            
            foreach(FaseStatus fase in DB.fases) {
                int faseAtual = nivelFase;

                if (DB.fases.ElementAt(nivelFase)._qntInimigosTipo1 > 0) 
                    Inimigo.IntervaloMovimentoInimigo();

                if (DB.fases.ElementAt(nivelFase)._qntInimigosTipo2 > 0) 
                    Inimigo.IntervaloMovimentoInimigo2();

                if (DB.fases.ElementAt(nivelFase)._qntInimigosTipo3 > 0) 
                    Inimigo.IntervaloMovimentoInimigo3();

                if (DB.fases.ElementAt(nivelFase)._modoJogo == "FUGA") 
                    Inimigo.IntervaloMovimentoInimigo5();

                Mapa.ResetaSpawnsItensDoMapa();
                Mapa.CheckMapaIsRenderizando();
                
                do{
                    key = Console.ReadKey();
                    if (pararRenderizacoes) {
                        continue;
                    }
                    if(key.Key == ConsoleKey.W || key.Key == ConsoleKey.UpArrow){
                        Personagem.Movimentacao(Direcao.Cima);
                    }
                    if(key.Key == ConsoleKey.A || key.Key == ConsoleKey.LeftArrow){
                        Personagem.Movimentacao(Direcao.Esquerda);
                    }
                    if(key.Key == ConsoleKey.S || key.Key == ConsoleKey.DownArrow){
                        Personagem.Movimentacao(Direcao.Baixo);
                    }
                    if(key.Key == ConsoleKey.D || key.Key == ConsoleKey.RightArrow){
                        Personagem.Movimentacao(Direcao.Direita);
                    }
                    if(char.IsDigit(key.KeyChar)){
                        Personagem.inventario.UsarItem(key.KeyChar);
                    }
                    if (key.Key == ConsoleKey.X){
                        MostrarMensagem.Exit();
                        Environment.Exit(0);
                    }
                }while (faseAtual == nivelFase || pararRenderizacoes);
            }
        }
    }
}