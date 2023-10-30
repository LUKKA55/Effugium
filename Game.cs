namespace jogoInicial

{
    public class Game
    {
        public static ConsoleKeyInfo key;

        public enum Direcao {
            Cima = 1,
            Esquerda,
            Baixo,
            Direita
        }

        static void Main()
        {
            Inimigo.IntervaloMovimentoInimigo();
            ItemAtaque.IntervaloVerificaItemAtaque();

            Console.WriteLine("Jogo inicializado");
            Console.WriteLine("Aperte X para encerrar");
            
            Mapa.MostrarMapa();

            do{
                key = Console.ReadKey();

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
            }while (key.Key != ConsoleKey.X);
        }
    }
}