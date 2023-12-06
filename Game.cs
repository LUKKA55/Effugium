using System;
namespace jogoInicial
{
    public enum enumInimigos {
        tipo1 = 1,
        tipo2,
        tipo3
    }

    public class Game
    {
        public static int nivelFase = 0;
        public static float dificuldade;

        public static int qntInimigosTipo1 = DBFases.mapas[nivelFase]._qntInimigosTipo1;
        public static int qntInimigosTipo2 = DBFases.mapas[nivelFase]._qntInimigosTipo2;
        public static int qntInimigosTipo3 = DBFases.mapas[nivelFase]._qntInimigosTipo3;
        public static ConsoleKeyInfo key;

        public static bool pararRenderizacoes = false;

        public static void MatarInimigo(enumInimigos tipoInimigo) {
            switch(tipoInimigo) {
                case enumInimigos.tipo1:
                    qntInimigosTipo1--;
                break;
                case enumInimigos.tipo2:
                    qntInimigosTipo2--;                
                break;
                case enumInimigos.tipo3:
                    qntInimigosTipo3--;                
                break;                                
            }
            CheckProximaFase();
        }

        public static void CheckProximaFase() {
            bool todosInimigoMortos = new List<int>{ 
                qntInimigosTipo1, 
                qntInimigosTipo2, 
                qntInimigosTipo3 
            }.TrueForAll(
                (qntInimigo) => qntInimigo <= 0
            );

            if (todosInimigoMortos) {
                nivelFase += 1;
                if (nivelFase == DBFases.mapas.Count()) {
                    Vitoria();
                } else {
                    ProximaFase();
                }
            }
        }

        static async Task ProximaFase() {
            pararRenderizacoes = true;

            char[] load = new char[40];
            for(int i = 0; i < load.Length; i++){
                load[i] = '.';

                MostrarMensagem.NextLevel();
                Console.Write(load);

                await Task.Delay(75);
            } 
            pararRenderizacoes = false;

            Mapa.mapa = DBFases.mapas[nivelFase]._mapa;
            qntInimigosTipo1 = DBFases.mapas[nivelFase]._qntInimigosTipo1;
            qntInimigosTipo2 = DBFases.mapas[nivelFase]._qntInimigosTipo2;
            qntInimigosTipo3 = DBFases.mapas[nivelFase]._qntInimigosTipo3;
            Mapa.MostrarMapa();
        }

        static void Vitoria() {
            Mapa.MostrarMapa();
            MostrarMensagem.Win();
            Environment.Exit(0);
        }
 
        public enum Direcao {
            Cima = 1,
            Esquerda,
            Baixo,
            Direita
        }

        static void Main()
        {
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
            
            ItemAtaque.IntervaloVerificaItemAtaque();
            
            foreach(FaseStatus fase in DBFases.mapas) {
                int faseAtual = nivelFase;

                if (fase._qntInimigosTipo1 > 0) {
                    Inimigo.IntervaloMovimentoInimigo();
                }

                if (fase._qntInimigosTipo2 > 0) {
                    Inimigo.IntervaloMovimentoInimigo2();
                }

                if(fase._temItemDefesa){
                    ItemDefesa.IntervaloVerificaItemDefesa();
                }
            
                Mapa.MostrarMapa();
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
                        Inventario.UsarItem(key.KeyChar);
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