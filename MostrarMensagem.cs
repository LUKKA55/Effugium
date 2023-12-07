namespace jogoInicial
{
    public class MostrarMensagem
    {
        public static void GameOver(){
            Console.Clear();
            Console.WriteLine("");
            Console.WriteLine("---------------------------------------------------");
            Console.WriteLine("-------------------- GAME OVER --------------------");
            Console.WriteLine("---------------------- Fase {0} ---------------------", 
                Game.nivelFase != 0 
                    ? Game.nivelFase 
                    : "INICIAL"
            );
            Console.WriteLine("");
        }
        public static void NextLevel(){
            Console.Clear();
            Console.WriteLine("");
            Console.WriteLine("---------------------- NEXT --------------------");
            Console.WriteLine("----------------------- LEVEL {0} --------------------", Game.nivelFase);
            Console.WriteLine("---------------------    -->    -------------------");
            Console.WriteLine("");
        }
        public static void Exit(){
            Console.Clear();
            Console.WriteLine("");
            Console.WriteLine("--------------------- GOOD BYE --------------------");
            Console.WriteLine("---------------------- FASE {0} ---------------------", 
                Game.nivelFase != 0 
                    ? Game.nivelFase 
                    : "INICIAL"
            );
            Console.WriteLine("---------------------------------------------------");
            Console.WriteLine("");
        }
        public static void Win(){
            Console.Clear();
            Console.WriteLine("");
            Console.WriteLine("---------------------------------------------------");
            Console.WriteLine("----------------------  WIN  ----------------------");
            Console.WriteLine("---------------------------------------------------");
            Console.WriteLine("");
        }
        public static void Start(){
            Console.WriteLine("");
            Console.WriteLine("-------------- SELECT A DIFFICULTY ----------------");
            Console.WriteLine("------------ EASY -- MEDIUM -- HARD ---------------");
            Console.WriteLine("------------ (1) ---- (2) ---- (3) ----------------");
            Console.WriteLine("");
        }
        public static void ErrorDifficulty(){
            Console.Clear();
            Console.WriteLine("");
            Console.WriteLine("---------------------------------------------------");
            Console.WriteLine("------------ CHOOSE A VALID DIFFICULTY ------------");
            Console.WriteLine("---------------------------------------------------");
            Console.WriteLine("");
        }
    }
}