namespace jogoInicial
{
    public class Inimigo
    {
        static public TipoInimigo[] tiposInimigos = { new TipoInimigo("XX", 1), new TipoInimigo(")(", 2)};
        public static void MovimentacaoInimigo (){
            int[,] posicaoInimigo = new int[Mapa.mapa.GetLength(0) * Mapa.mapa.GetLength(1), 2];

            bool isMovimentoInimigoFoiRealizado = false;

            int quantidadeInimigo = 0;

            for (int i = 0; i < Mapa.mapa.GetLength(0); i++){
                for (int j = 0; j < Mapa.mapa.GetLength(1); j++){
                    if(tiposInimigos.Any(inimigo => inimigo._aparencia == Mapa.mapa[i,j])){
                        posicaoInimigo[quantidadeInimigo, 0] = i;
                        posicaoInimigo[quantidadeInimigo, 1] = j;
                        quantidadeInimigo++;
                    }
                }
            }

            static string limpaLugarAntigoInimigo(int[,] posicaoInimigo, int i) => Mapa.mapa[posicaoInimigo[i, 0], posicaoInimigo[i, 1]] = "  ";

            for(int i = 0; i < quantidadeInimigo; i++){
                do {
                    Random rnd = new();
                    int movimentoAleatorioInimigo = rnd.Next(1,5);
                    Console.WriteLine(movimentoAleatorioInimigo);
                    int[] variacaoPosicoes = new int[2];

                    int variacaoPosicaoZero = movimentoAleatorioInimigo % 2 != 0 
                        ? movimentoAleatorioInimigo == 1 
                            ? -1 
                            : 1 
                        : 0;
                        
                    int variacaoPosicaoUm = movimentoAleatorioInimigo % 2 == 0
                        ? movimentoAleatorioInimigo == 2 
                            ? -1 
                            : 1 
                        : 0;

                    variacaoPosicoes[0] = posicaoInimigo[i, 0] + variacaoPosicaoZero;
                    variacaoPosicoes[1] = posicaoInimigo[i, 1] + variacaoPosicaoUm;

                    string destino = Mapa.mapa[variacaoPosicoes[0],variacaoPosicoes[1]];

                    if(destino == "  "){
                        limpaLugarAntigoInimigo(posicaoInimigo, i);
                        Mapa.mapa[variacaoPosicoes[0],variacaoPosicoes[1]] = "XX";
                        isMovimentoInimigoFoiRealizado = true;
                    }
                    else if(destino == "{]"){
                        limpaLugarAntigoInimigo(posicaoInimigo, i);
                        ItemDefesa.usandoDefesa = false;
                        ItemAtaque.nmrPuloAtaqueValido = 0;
                        destino = "()";
                    }
                    else if(destino == "{}"){
                        limpaLugarAntigoInimigo(posicaoInimigo, i);
                        ItemAtaque.nmrPuloAtaqueValido = 0;
                        destino = "()";
                    }
                    else if(destino == "[]"){
                        ItemDefesa.usandoDefesa = false;
                        destino = "()";
                    }
                    else if(destino == "()"){
                        limpaLugarAntigoInimigo(posicaoInimigo, i);
                        destino = "XX";

                        MostrarMensagem.GameOver();
                        Environment.Exit(0);
                    }
                } while (!isMovimentoInimigoFoiRealizado);
                isMovimentoInimigoFoiRealizado = false;

                Mapa.CheckMapaIsRenderizando();
            }
        }

        public static async Task IntervaloMovimentoInimigo(){
            await Task.Delay(1500);
            MovimentacaoInimigo();
            await IntervaloMovimentoInimigo();  
        }
    }
}