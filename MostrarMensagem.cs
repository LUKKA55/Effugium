namespace jogoInicial
{
    public class MostrarMensagem
    {
        public static void GameOver(){
            Console.WriteLine("");
            Console.WriteLine("---------------------------------------------------");
            Console.WriteLine("-------------------- GAME OVER --------------------");
            Console.WriteLine("--------------------- Fase {0} --------------------", Game.nivelFase);
            Console.WriteLine("");
        }
    }
}