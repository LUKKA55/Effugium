namespace jogoInicial
{
    public class Inimigo
    {
        static public List<TipoInimigo> todosTiposInimigo = new() { new( "XX", 1 ), new(")(", 2)};
        static public List<TipoInimigo> tiposInimigosBase = new() { new( "XX", 1 )};
        static public List<TipoInimigo> tiposInimigosBase2 = new() { new(")(", 1)};

        public static void MovimentacaoInimigo (List<TipoInimigo> tiposInimigos){
            int[,] posicaoInimigo = new int[Mapa.mapa.GetLength(0) * Mapa.mapa.GetLength(1), 2];

            bool isMovimentoInimigoFoiRealizado = false;

            List<TipoInimigo> inimigos = new() { };

            for (int i = 0; i < Mapa.mapa.GetLength(0); i++){
                for (int j = 0; j < Mapa.mapa.GetLength(1); j++){
                    var tipoInimigo = tiposInimigos.Find(inimigo => inimigo._aparencia == Mapa.mapa[i,j]);

                    if(tipoInimigo != null){
                        posicaoInimigo[inimigos.Count, 0] = i;
                        posicaoInimigo[inimigos.Count, 1] = j;
                        inimigos.Insert(inimigos.Count, tipoInimigo);
                    }
                }
            }

            static string limpaLugarAntigoInimigo(int[,] posicaoInimigo, int i) => Mapa.mapa[posicaoInimigo[i, 0], posicaoInimigo[i, 1]] = "  ";

            for(int i = 0; i < inimigos.Count; i++){
                for(int j = 0; j < inimigos[i]._qntPassos; j++){
                    do {
                        Random rnd = new();
                        int movimentoAleatorioInimigo = rnd.Next(1,5);
                        int[] variacaoPosicoes = new int[2];

                        int variacaoPosicaoZero = movimentoAleatorioInimigo % 2 != 0 ? movimentoAleatorioInimigo == 1 ? -1 : 1 : 0;
                        int variacaoPosicaoUm = movimentoAleatorioInimigo % 2 == 0 ? movimentoAleatorioInimigo == 2 ? -1 : 1 : 0;
                        
                        variacaoPosicoes[0] = posicaoInimigo[i, 0] + variacaoPosicaoZero;
                        variacaoPosicoes[1] = posicaoInimigo[i, 1] + variacaoPosicaoUm;

                        string destino = Mapa.mapa[variacaoPosicoes[0],variacaoPosicoes[1]];

                        if(destino == "  "){
                            limpaLugarAntigoInimigo(posicaoInimigo, i);
                            Mapa.mapa[variacaoPosicoes[0],variacaoPosicoes[1]] = inimigos[i]._aparencia;
                            posicaoInimigo[i, 0] = variacaoPosicoes[0];
                            posicaoInimigo[i, 1] = variacaoPosicoes[1];

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
                            Mapa.mapa[variacaoPosicoes[0],variacaoPosicoes[1]] = inimigos[i]._aparencia;
                            Mapa.CheckMapaIsRenderizando();

                            MostrarMensagem.GameOver();
                            Environment.Exit(0);
                        }
                    } while (!isMovimentoInimigoFoiRealizado);
                    isMovimentoInimigoFoiRealizado = false;
                }
            }
            Mapa.CheckMapaIsRenderizando();
        }

        public static async Task IntervaloMovimentoInimigo(){
            await Task.Delay(1500);
            MovimentacaoInimigo(tiposInimigosBase);
            await IntervaloMovimentoInimigo();  
        }

        public static async Task IntervaloMovimentoInimigo2(){
            await Task.Delay(750);
            MovimentacaoInimigo(tiposInimigosBase2);
            await IntervaloMovimentoInimigo2();  
        }
    }
}