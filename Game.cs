namespace Effugium
{
    public enum enumDirecao {
        Cima = 1,
        Esquerda,
        Baixo,
        Direita
    }
    public class Game {
        public static int nivelFase = 0;
        public static FaseStatus FaseAtual = DB.fases[nivelFase].CopiaFase();
        public static float dificuldade;
        public static ConsoleKeyInfo key;
        public static bool telaInfoAberta = false;
        public static bool telaGameOver = false;
        public static bool pararRenderizacoes = false;
        public static bool faseNaoMudou = false;

        public static void CheckProximaFase() {
            bool todosInimigoMortos = new List<int>{ 
                FaseAtual._qntInimigosTipo1, 
                FaseAtual._qntInimigosTipo2, 
                FaseAtual._qntInimigosTipo3,
                FaseAtual._qntInimigosTipo4,
                FaseAtual._qntInimigosTipo5,
                FaseAtual._qntInimigosTipo6,
                FaseAtual._qntInimigosTipo7,
                FaseAtual._qntInimigosTipo8,
                FaseAtual._qntInimigosTipo9,
            }.TrueForAll(
                (qntInimigo) => qntInimigo <= 0
            );

            bool jogadorFugiuDoMapa = 
                !(Personagem.posicaoPersonagem[0] > 0 && Personagem.posicaoPersonagem[0] < GetMapa().GetLength(0)-1 
                && Personagem.posicaoPersonagem[1] > 0 && Personagem.posicaoPersonagem[1] < GetMapa().GetLength(1)-1);

            if (todosInimigoMortos || jogadorFugiuDoMapa) {
                if (nivelFase == DB.fases.Count - 1) {
                    Vitoria();
                } else {
                    ProximaFase();
                }
            }
        }

        public static async Task ProximaFase() {
            pararRenderizacoes = true;

            char[] load = new char[50];
            for(int i = 0; i < load.Length; i++){
                load[i] = '.';

                MostrarMensagem.NextLevel();
                Console.Write(load);

                await Task.Delay(75);
            } 
            
            nivelFase += 1;
            Personagem.inventario.SaveBackupInventario();
            FaseAtual = DB.fases[nivelFase].CopiaFase();
            Personagem.ResetPosicaoPersonagem();
            Mapa.ResetaSpawnsItensDoMapa();
            pararRenderizacoes = false;
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

        public

        enum enumOpcoesGameOver {
            TentarNovamente = 1,
            NovoJogo,
            Sair
        }
        public static void GameOver() {
            if(telaGameOver == true)
                return;
            
            telaGameOver = true;
            pararRenderizacoes = true;
            bool respostaJaFoiEscolhida = false;
            ConsoleKeyInfo inputUsuario;
            int respostaEscolhida;

            do{
                MostrarMensagem.GameOver();
                inputUsuario = Console.ReadKey();
                respostaEscolhida = inputUsuario.KeyChar - '0';

                if(new List<int>(){1, 2, 3}.Contains(respostaEscolhida)){
                    enumOpcoesGameOver opcao = (enumOpcoesGameOver)respostaEscolhida;
                    switch (opcao){
                        case enumOpcoesGameOver.TentarNovamente:
                            Personagem.inventario.LoadBackupInventario();
                            nivelFase -= 1;
                            break;
                        case enumOpcoesGameOver.NovoJogo:
                            Personagem.inventario.ResetarInventario();
                            nivelFase = -1;
                            break;
                        case enumOpcoesGameOver.Sair:
                            MostrarMensagem.Exit();
                            Environment.Exit(0);
                            break;
                    }
                    ProximaFase();
                    faseNaoMudou = false;
                    respostaJaFoiEscolhida = true;
                }
            }while (!respostaJaFoiEscolhida);
            telaGameOver = false;
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
                !new List<int>{1, 2, 3}.Contains(dificuldadeExcolhida)
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
            
            for(; nivelFase < DB.fases.Count;) {
                int faseAtual = nivelFase;
                faseNaoMudou = true;

                if (DB.fases.ElementAt(nivelFase)._qntInimigosTipo1 > 0) 
                    Inimigos.IntervaloMovimentoInimigo();

                if (DB.fases.ElementAt(nivelFase)._qntInimigosTipo2 > 0) 
                    Inimigos.IntervaloMovimentoInimigo2();

                if (DB.fases.ElementAt(nivelFase)._qntInimigosTipo3 > 0) 
                    Inimigos.IntervaloMovimentoInimigo3();

                if (DB.fases.ElementAt(nivelFase)._qntInimigosTipo4 > 0) 
                    Inimigos.IntervaloMovimentoInimigo4();

                if (DB.fases.ElementAt(nivelFase)._qntInimigosTipo5 > 0)  
                    Inimigos.IntervaloMovimentoInimigo5();

                if (DB.fases.ElementAt(nivelFase)._qntInimigosTipo6 > 0)  
                    Inimigos.IntervaloMovimentoInimigo6();

                if (DB.fases.ElementAt(nivelFase)._qntInimigosTipo7 > 0)  
                    Inimigos.IntervaloMovimentoInimigo7();

                if (DB.fases.ElementAt(nivelFase)._qntInimigosTipo8 > 0)  
                    Inimigos.IntervaloMovimentoInimigo8();

                if (DB.fases.ElementAt(nivelFase)._qntInimigosTipo9 > 0)  
                    Inimigos.IntervaloMovimentoInimigo9();

                Mapa.ResetaSpawnsItensDoMapa();
                Mapa.CheckMapaIsRenderizando();
                
                do{
                    key = Console.ReadKey();

                    if (pararRenderizacoes) {
                        continue;
                    }
                    if(key.Key == ConsoleKey.W || key.Key == ConsoleKey.UpArrow){
                        Personagem.Movimentacao(enumDirecao.Cima);
                    }
                    if(key.Key == ConsoleKey.A || key.Key == ConsoleKey.LeftArrow){
                        Personagem.Movimentacao(enumDirecao.Esquerda);
                    }
                    if(key.Key == ConsoleKey.S || key.Key == ConsoleKey.DownArrow){
                        Personagem.Movimentacao(enumDirecao.Baixo);
                    }
                    if(key.Key == ConsoleKey.D || key.Key == ConsoleKey.RightArrow){
                        Personagem.Movimentacao(enumDirecao.Direita);
                    }
                    if(char.IsDigit(key.KeyChar)){
                        Personagem.inventario.UsarItem(key.KeyChar);
                    }
                    if (key.Key == ConsoleKey.X){
                        MostrarMensagem.Exit();
                        Environment.Exit(0);
                    }
                }while ((faseAtual == nivelFase || pararRenderizacoes) && faseNaoMudou);
            }
        }
    }
}