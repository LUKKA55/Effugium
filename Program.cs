namespace jogoInicial

{
    internal class Program
    {
        private static string[,] mapaDefault = {
                {"||","==","==","==","==","==","==","==","==","==","==","==","==","==","==","==","==","==","==","||"},
                {"||","  ","  ","  ","  ","||","  ","  ","  ","||","  ","  ","  ","  ","||","  ","  ","  ","  ","||"},
                {"||","  ","  ","  ","  ","  ","  ","  ","  ","{}","  ","  ","  ","  ","||","  ","  ","  ","  ","||"},
                {"||","  ","||","==","||","  ","||","==","||","  ","||","==","||","  ","||","  ","  ","||","  ","||"},
                {"||","  ","||","  ","||","  ","||","  ","||","  ","||","  ","||","  ","||","  ","  ","||","  ","||"},
                {"||","  ","||","  ","||","  ","||","  ","||","  ","||","  ","||","  ","||","  ","  ","||","  ","||"},
                {"||","  ","||","==","||","  ","||","==","||","  ","||","==","||","  ","||","==","==","||","  ","||"},
                {"||","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","XX","  ","  ","  ","  ","  ","||"},
                {"||","  ","==","==","==","  ","||","==","||","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","||"},
                {"||","  ","  ","  ","  ","  ","||","  ","||","  ","||","||","  ","==","==","||","==","==","  ","||"},
                {"||","  ","  ","||","||","  ","||","==","||","  ","||","||","  ","  ","  ","||","  ","  ","  ","||"},
                {"||","  ","  ","||","||","  ","  ","  ","  ","  ","||","||","  ","||","  ","  ","  ","||","  ","||"},
                {"||","==","==","==","==","==","==","==","==","==","==","==","==","==","==","==","==","==","==","||"},
            };
        
        private static bool isMovimentoInimigoFoiRealizado = false;

        private static int renderizacoesPendentes = 0;

        private static readonly int[] posicaoInimigo = new int[2];

        static void Main(string[] args)
        {
            ConsoleKeyInfo key;
            
            IntervaloMovimentoInimigo();

            Console.WriteLine("Jogo inicializado");
            Console.WriteLine("Aperte X para encerrar");
            
            MostrarMapa(mapaDefault);

            do{
                key = Console.ReadKey();

                if(key.Key == ConsoleKey.W || key.Key == ConsoleKey.UpArrow){
                    Movimentacao(mapaDefault, "W");
                }

                if(key.Key == ConsoleKey.A || key.Key == ConsoleKey.LeftArrow){
                    Movimentacao(mapaDefault, "A");
                }

                if(key.Key == ConsoleKey.S || key.Key == ConsoleKey.DownArrow){
                    Movimentacao(mapaDefault, "S");
                }

                if(key.Key == ConsoleKey.D || key.Key == ConsoleKey.RightArrow){
                    Movimentacao(mapaDefault, "D");
                }


            } while (key.Key != ConsoleKey.X);
        }

        static void MostrarMapa(string [,] mapa){
            Console.Clear(); 
            for (int i = 0; i < mapa.GetLength(0); i++){
                for (int j = 0; j < mapa.GetLength(1); j++){
                    Console.Write(mapa[i,j]);
                }

                Console.WriteLine();
            }
        }

        static void CheckMapaIsRenderizando() {
            int qntRenderizacoesAtuais = renderizacoesPendentes;
            if (renderizacoesPendentes != 0) {
                while (!(renderizacoesPendentes < qntRenderizacoesAtuais));
            }
            renderizacoesPendentes++;
            MostrarMapa(mapaDefault);
            renderizacoesPendentes--;
        }

        static void Movimentacao(string [,] mapa, string direcao){
            bool movimentoRealizado = false;
            for (int i = 0; i < mapa.GetLength(0); i++){
                for (int j = 0; j < mapa.GetLength(1); j++){
                    if(mapa[i,j] == "{}"){
                        switch (direcao)
                        {
                            case "W":
                                if(mapa[i-1,j] == "  "){
                                    mapa[i,j] = "  ";
                                    mapa[i-1,j] = "{}";

                                } else {
                                    return;
                                }
                            break;
                            case "A":
                                if(mapa[i,j-1] == "  "){ 
                                    mapa[i,j] = "  ";
                                    mapa[i,j-1] = "{}";

                                } else { 
                                    return; 
                                }
                            break;
                            case "S":
                                if(mapa[i+1,j] == "  "){
                                    mapa[i,j] = "  ";
                                    mapa[i+1,j] = "{}";

                                } else {
                                    return;
                                }
                            break;
                            case "D":
                                if(mapa[i,j+1] == "  "){  
                                    mapa[i,j] = "  ";
                                    mapa[i,j+1] = "{}";

                                } else {
                                    return;
                                }
                            break;
                        }
                        movimentoRealizado = true;
                        break;
                    }
                }
                if(movimentoRealizado)
                    break;
            }
            mapaDefault = mapa;
            CheckMapaIsRenderizando();
        }

        static void MovimentacaoInimigo (string [,] mapa){
            for (int i = 0; i < mapa.GetLength(0); i++){
                for (int j = 0; j < mapa.GetLength(1); j++){
                    if(mapa[i,j] == "XX"){
                        posicaoInimigo[0] = i;
                        posicaoInimigo[1] = j;
                    }
                }
            }
            string limpaLugarAntigoInimigo() => mapa[posicaoInimigo[0], posicaoInimigo[1]] = "  ";
            do {
                Random rnd = new();
                int movimentoAleatorioInimigo = rnd.Next(1,4);

                switch (movimentoAleatorioInimigo)
                {
                    case 1:
                        if(mapa[posicaoInimigo[0]-1,posicaoInimigo[1]] == "  "){
                            limpaLugarAntigoInimigo();
                            mapa[posicaoInimigo[0]-1,posicaoInimigo[1]] = "XX";
                            isMovimentoInimigoFoiRealizado = true;
                        }
                    break;
                    case 2:
                        if(mapa[posicaoInimigo[0],posicaoInimigo[1]-1] == "  "){ 
                            limpaLugarAntigoInimigo();                        
                            mapa[posicaoInimigo[0],posicaoInimigo[1]-1] = "XX";
                            isMovimentoInimigoFoiRealizado = true;
                        }
                    break;
                    case 3:
                        if(mapa[posicaoInimigo[0]+1,posicaoInimigo[1]] == "  "){
                            limpaLugarAntigoInimigo();                        
                            mapa[posicaoInimigo[0]+1,posicaoInimigo[1]] = "XX";
                            isMovimentoInimigoFoiRealizado = true;
                        }
                    break;
                    case 4:
                        if(mapa[posicaoInimigo[0],posicaoInimigo[1]+1] == "  "){  
                            limpaLugarAntigoInimigo();                        
                            mapa[posicaoInimigo[0],posicaoInimigo[1]+1] = "XX";
                            isMovimentoInimigoFoiRealizado = true;
                        }
                    break;
                }
            } while (!isMovimentoInimigoFoiRealizado);

            isMovimentoInimigoFoiRealizado = false;
            mapaDefault = mapa;
            CheckMapaIsRenderizando();
        }

        static async Task IntervaloMovimentoInimigo()
        {
            await Task.Delay(1500);
            MovimentacaoInimigo(mapaDefault);
            await IntervaloMovimentoInimigo();  
        }
    }
}