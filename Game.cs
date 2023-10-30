namespace jogoInicial

{
    internal class Game
    {
        public static ConsoleKeyInfo key;

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
                    Personagem.Movimentacao(Mapa.mapa,"W");
                }
                if(key.Key == ConsoleKey.A || key.Key == ConsoleKey.LeftArrow){
                    Personagem.Movimentacao(Mapa.mapa, "A");
                }
                if(key.Key == ConsoleKey.S || key.Key == ConsoleKey.DownArrow){
                    Personagem.Movimentacao(Mapa.mapa, "S");
                }
                if(key.Key == ConsoleKey.D || key.Key == ConsoleKey.RightArrow){
                    Personagem.Movimentacao(Mapa.mapa, "D");
                }
                if(char.IsDigit(key.KeyChar)){
                    Inventario.UsarItem(key.KeyChar);
                }
            }while (key.Key != ConsoleKey.X);
        }
    }
}