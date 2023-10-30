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

                variacaoPosicoes[0] = posicaoInimigo[0] + variacaoPosicaoZero;
                variacaoPosicoes[1] = posicaoInimigo[1] + variacaoPosicaoUm;

                string destino = Mapa.mapa[variacaoPosicoes[0],variacaoPosicoes[1]];

                if(destino == "  "){
                    limpaLugarAntigoInimigo(posicaoInimigo);
                    Mapa.mapa[variacaoPosicoes[0],variacaoPosicoes[1]] = "XX";
                    isMovimentoInimigoFoiRealizado = true;
                }
                else if(destino == "{]"){
                    limpaLugarAntigoInimigo(posicaoInimigo);
                    ItemDefesa.usandoDefesa = false;
                    ItemAtaque.nmrPuloAtaqueValido = 0;
                }
                else if(destino == "{}"){
                    limpaLugarAntigoInimigo(posicaoInimigo);
                    ItemAtaque.nmrPuloAtaqueValido = 0;
                }
                else if(destino == "[]"){
                    ItemDefesa.usandoDefesa = false;
                    Mapa.mapa[variacaoPosicoes[0],variacaoPosicoes[1]] = "()";
                }
                else if(destino == "()"){
                    limpaLugarAntigoInimigo(posicaoInimigo);
                    Mapa.mapa[variacaoPosicoes[0],variacaoPosicoes[1]] = "XX";

                    MostrarMensagem.GameOver();
                    Environment.Exit(0);
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