namespace jogoInicial
{
    public class Inimigo
    {
        public static void MovimentacaoInimigo (){
            int[] posicaoInimigo = new int[2];
            bool isMovimentoInimigoFoiRealizado = false;

            for (int i = 0; i < Mapa.mapa.GetLength(0); i++){
                for (int j = 0; j < Mapa.mapa.GetLength(1); j++){
                    if(Mapa.mapa[i,j] == "XX"){
                        posicaoInimigo[0] = i;
                        posicaoInimigo[1] = j;
                    }
                }
            }
            static string limpaLugarAntigoInimigo(int[] posicaoInimigo) => Mapa.mapa[posicaoInimigo[0], posicaoInimigo[1]] = "  ";
            do {
                Random rnd = new();
                int movimentoAleatorioInimigo = rnd.Next(1,5);

                switch (movimentoAleatorioInimigo)
                {
                    case 1:
                        if(Mapa.mapa[posicaoInimigo[0]-1,posicaoInimigo[1]] == "  "){
                            limpaLugarAntigoInimigo(posicaoInimigo);
                            Mapa.mapa[posicaoInimigo[0]-1,posicaoInimigo[1]] = "XX";
                            isMovimentoInimigoFoiRealizado = true;

                        }else if(Mapa.mapa[posicaoInimigo[0]-1,posicaoInimigo[1]] == "{]"){
                            limpaLugarAntigoInimigo(posicaoInimigo);
                            ItemDefesa.usandoDefesa = false;
                            ItemAtaque.nmrPuloAtaqueValido = 0;

                        }else if(Mapa.mapa[posicaoInimigo[0]-1,posicaoInimigo[1]] == "{}"){
                            limpaLugarAntigoInimigo(posicaoInimigo);
                            ItemAtaque.nmrPuloAtaqueValido = 0;

                        }else if(Mapa.mapa[posicaoInimigo[0]-1,posicaoInimigo[1]] == "[]"){
                            ItemDefesa.usandoDefesa = false;
                        }else if(Mapa.mapa[posicaoInimigo[0]-1,posicaoInimigo[1]] == "()"){
                            limpaLugarAntigoInimigo(posicaoInimigo);
                            Mapa.mapa[posicaoInimigo[0]-1,posicaoInimigo[1]] = "XX";

                            Console.WriteLine("Morreu mané");
                            Environment.Exit(0);
                        }
                    break;
                    case 2:
                        if(Mapa.mapa[posicaoInimigo[0],posicaoInimigo[1]-1] == "  "){ 
                            limpaLugarAntigoInimigo(posicaoInimigo);                        
                            Mapa.mapa[posicaoInimigo[0],posicaoInimigo[1]-1] = "XX";
                            isMovimentoInimigoFoiRealizado = true;

                        }else if(Mapa.mapa[posicaoInimigo[0],posicaoInimigo[1]-1] == "{]"){
                            limpaLugarAntigoInimigo(posicaoInimigo);
                            ItemDefesa.usandoDefesa = false;
                            ItemAtaque.nmrPuloAtaqueValido = 0;

                        }else if(Mapa.mapa[posicaoInimigo[0],posicaoInimigo[1]-1] == "{}"){
                            limpaLugarAntigoInimigo(posicaoInimigo);     
                            ItemAtaque.nmrPuloAtaqueValido = 0;                   

                        }else if(Mapa.mapa[posicaoInimigo[0],posicaoInimigo[1]-1] == "[]"){
                            ItemDefesa.usandoDefesa = false;

                        }else if(Mapa.mapa[posicaoInimigo[0],posicaoInimigo[1]-1] == "()"){
                            limpaLugarAntigoInimigo(posicaoInimigo);                        
                            Mapa.mapa[posicaoInimigo[0],posicaoInimigo[1]-1] = "XX";

                            Console.WriteLine("Morreu mané");
                            Environment.Exit(0);
                        }
                    break;
                    case 3:
                        if(Mapa.mapa[posicaoInimigo[0]+1,posicaoInimigo[1]] == "  "){
                            limpaLugarAntigoInimigo(posicaoInimigo);                        
                            Mapa.mapa[posicaoInimigo[0]+1,posicaoInimigo[1]] = "XX";
                            isMovimentoInimigoFoiRealizado = true;
                        
                        }else if(Mapa.mapa[posicaoInimigo[0]+1,posicaoInimigo[1]] == "{]"){
                            limpaLugarAntigoInimigo(posicaoInimigo);
                            ItemDefesa.usandoDefesa = false;
                            ItemAtaque.nmrPuloAtaqueValido = 0;

                        }else if(Mapa.mapa[posicaoInimigo[0]+1,posicaoInimigo[1]] == "{}"){
                            limpaLugarAntigoInimigo(posicaoInimigo);   
                            ItemAtaque.nmrPuloAtaqueValido = 0;                     
                   
                        }else if(Mapa.mapa[posicaoInimigo[0]+1,posicaoInimigo[1]] == "[]"){
                            ItemDefesa.usandoDefesa = false;

                        }else if(Mapa.mapa[posicaoInimigo[0]+1,posicaoInimigo[1]] == "()"){
                            limpaLugarAntigoInimigo(posicaoInimigo);                        
                            Mapa.mapa[posicaoInimigo[0]+1,posicaoInimigo[1]] = "XX";

                            Console.WriteLine("Morreu mané");
                            Environment.Exit(0);
                        }
                    break;
                    case 4:
                        if(Mapa.mapa[posicaoInimigo[0],posicaoInimigo[1]+1] == "  "){  
                            limpaLugarAntigoInimigo(posicaoInimigo);                        
                            Mapa.mapa[posicaoInimigo[0],posicaoInimigo[1]+1] = "XX";
                            isMovimentoInimigoFoiRealizado = true;

                        }else if(Mapa.mapa[posicaoInimigo[0],posicaoInimigo[1]+1] == "{]"){
                            limpaLugarAntigoInimigo(posicaoInimigo);
                            ItemDefesa.usandoDefesa = false;
                            ItemAtaque.nmrPuloAtaqueValido = 0;

                        }else if(Mapa.mapa[posicaoInimigo[0],posicaoInimigo[1]+1] == "{}"){
                            limpaLugarAntigoInimigo(posicaoInimigo);          
                            ItemAtaque.nmrPuloAtaqueValido = 0;              
                        
                        }else if(Mapa.mapa[posicaoInimigo[0],posicaoInimigo[1]+1] == "[]"){
                           ItemDefesa.usandoDefesa = false;

                        }else if(Mapa.mapa[posicaoInimigo[0],posicaoInimigo[1]+1] == "()"){
                            limpaLugarAntigoInimigo(posicaoInimigo);                        
                            Mapa.mapa[posicaoInimigo[0],posicaoInimigo[1]+1] = "XX";

                            Console.WriteLine("Morreu mané");
                            Environment.Exit(0);
                        }
                    break;
                }
            } while (!isMovimentoInimigoFoiRealizado);

            Mapa.CheckMapaIsRenderizando();
        }

        public static async Task IntervaloMovimentoInimigo(){
            await Task.Delay(1500);
            MovimentacaoInimigo();
            await IntervaloMovimentoInimigo();  
        }
    }
}