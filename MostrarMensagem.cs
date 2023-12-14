namespace jogoInicial
{
    public class MostrarMensagem
    {
        public static void GameOver(){
            Console.Clear();
            Console.WriteLine("");
            Console.WriteLine("{0}----------------------------------------------", Game.nivelFase > 9 ? "--" : "-");
            Console.WriteLine("{0}--------------- FIM DE JOGO ------------------", Game.nivelFase > 9 ? "--" : "-");
            Console.WriteLine("------------------ FASE {0} ---------------------", Game.nivelFase);
            Console.WriteLine("");
        }
        public static void NextLevel(){
            Console.Clear();
            Console.WriteLine("");
            Console.WriteLine("{0}-------------------- PRÓXIMO ----------------------", Game.nivelFase > 9 ? "--" : "-");
            Console.WriteLine("--------------------- LEVEL {0} ----------------------", Game.nivelFase);
            LevelTip();
            Console.WriteLine("{0}-------------------    -->    ---------------------", Game.nivelFase > 9 ? "--" : "-");
            Console.WriteLine("");
        }
        public static void Exit(){
            Console.Clear();
            Console.WriteLine("");
            Console.WriteLine("{0}---------------- ATÉ A PRÓXIMA ----------------", Game.nivelFase > 9 ? "--" : "-");
            Console.WriteLine("------------------ FASE {0} ----------------------", Game.nivelFase);
            Console.WriteLine("{0}-----------------------------------------------", Game.nivelFase > 9 ? "--" : "-");
            Console.WriteLine("");
        }
        public static void Win(){
            Console.Clear();
            Console.WriteLine("");
            Console.WriteLine("---------------------------------------------------");
            Console.WriteLine("---------------------  VENCEU  --------------------");
            Console.WriteLine("---------------------------------------------------");
            Console.WriteLine("");
        }
        public static void Start(){
            Console.WriteLine("");
            Console.WriteLine("------------- SELECIONE A DIFUCULDADE -------------");
            Console.WriteLine("----------- FÁCIL -- MÉDIO -- DIFÍCIL -------------");
            Console.WriteLine("------------ (1) ---- (2) ---- (3) ----------------");
            Console.WriteLine("");
        }
        public static void ErrorDifficulty(){
            Console.Clear();
            Console.WriteLine("");
            Console.WriteLine("---------------------------------------------------");
            Console.WriteLine("---------- ESCOLHA UMA DIFICULDADE VÁLIDA ---------");
            Console.WriteLine("---------------------------------------------------");
            Console.WriteLine("");
        }

        public static void LevelTip() {
            switch(Game.nivelFase) {
                case 8: //1º Fase de Fuga contra varios XX
                case 10: //1º Fase de Fuga Inimigo "@"
                    Console.WriteLine("{0}----- DICA: Fugir as vezes é a melhor opção, ------", Game.nivelFase > 9 ? "--" : "-");
                    Console.WriteLine("{0}----- quebrar paredes laterais rachadas      ------", Game.nivelFase > 9 ? "--" : "-");
                    Console.WriteLine("{0}----- resulta em vitória!                    ------", Game.nivelFase > 9 ? "--" : "-");
                break;
                case 19: //Fase não da respawn de picareta
                    Console.WriteLine("{0}------ DICA: Nem sempre os itens reaparecem! ------", Game.nivelFase > 9 ? "--" : "-");
                break;
            }
        }

        public static void InfoItem(EnumItens tipoItem) {
            Console.Clear();
            Console.WriteLine("");
            switch(tipoItem) {
                case EnumItens.espada:
                    Console.WriteLine("----------- NOVO ITEM: ESPADA | <- | -------------");
                    Console.WriteLine("Você encontrou uma espada, ative-a pressionando");
                    Console.WriteLine("a tecla 1 para que seus próximos 10 pulos");
                    Console.WriteLine("possam matar um único inimigo que estiver em seu");
                    Console.WriteLine("caminho.");
                break;
                case EnumItens.escudo:
                    Console.WriteLine("----------- NOVO ITEM: ESCUDO | <> | -------------");
                    Console.WriteLine("Você encontrou um escudo, ative-o pressionando");
                    Console.WriteLine("a tecla 2 para que fique protegido de um único");
                    Console.WriteLine("ataque.");
                break;
                case EnumItens.picareta:
                    Console.WriteLine("---------- NOVO ITEM: PICARETA | T  | ------------");
                    Console.WriteLine("Você encontrou uma picareta, ative-a pressionando");
                    Console.WriteLine("a tecla 3 para que a proxima parede que estiver em");
                    Console.WriteLine("seu caminho seja destruida, paredes laterais");
                    Console.WriteLine("do mapa só podem ser destruidas casos estejam");
                    Console.WriteLine("rachadas, sendo elas // ou ~~");
                break;  
                case EnumItens.arco:
                    Console.WriteLine("-------- NOVO ITEM: ARCO | D- | ----------");
                    Console.WriteLine("Você encontrou um arco, equipe-o pressionando");
                    Console.WriteLine("a tecla 4 para que a proxima tecla de");
                    Console.WriteLine("movimento dispare uma unica flecha.");
                break;                              
            }
            Console.WriteLine("----------------- [C] CONTINUAR ------------------");
            Console.WriteLine("");
        }
    }
}