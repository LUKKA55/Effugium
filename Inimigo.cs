namespace jogoInicial
{
    public class Inimigo
    {
        static public List<string> todosTiposInimigo = new(){"XX", ")("};
        static public TipoInimigo tiposInimigosBase = new("XX");
        static public TipoInimigo tiposInimigosBase2 = new(")(");

        public static void MovimentacaoInimigo (TipoInimigo tipoInimigo){
            int[,] posicaoInimigo = new int[Mapa.mapa.GetLength(0) * Mapa.mapa.GetLength(1), 2];

            bool isMovimentoInimigoFoiRealizado = false;

            List<TipoInimigo> inimigos = new() { };

            for (int i = 0; i < Mapa.mapa.GetLength(0); i++){
                for (int j = 0; j < Mapa.mapa.GetLength(1); j++){
                    if(tipoInimigo._aparencia == Mapa.mapa[i,j]){
                        posicaoInimigo[inimigos.Count, 0] = i;
                        posicaoInimigo[inimigos.Count, 1] = j;
                        inimigos.Insert(inimigos.Count, tipoInimigo);
                    }
                }
            }

            static string limpaLugarAntigoInimigo(int[,] posicaoInimigo, int i) => Mapa.mapa[posicaoInimigo[i, 0], posicaoInimigo[i, 1]] = "  ";

            for(int i = 0; i < inimigos.Count; i++){
                do {
                    Random rnd = new();
                    int movimentoAleatorioInimigo = rnd.Next(1,5);
                    int[] variacaoPosicoes = new int[2];

                    int variacaoPosicaoZero = movimentoAleatorioInimigo % 2 != 0 ? movimentoAleatorioInimigo == 1 ? -1 : 1 : 0;
                    int variacaoPosicaoUm = movimentoAleatorioInimigo % 2 == 0 ? movimentoAleatorioInimigo == 2 ? -1 : 1 : 0;
                    
                    variacaoPosicoes[0] = posicaoInimigo[i, 0] + variacaoPosicaoZero;
                    variacaoPosicoes[1] = posicaoInimigo[i, 1] + variacaoPosicaoUm;

                    string destino = Mapa.mapa[variacaoPosicoes[0],variacaoPosicoes[1]];

                    if(
                        destino == "  " || 
                        Inventario.todosTiposItens.FindIndex(i => i == destino) >= 0
                    ){
                        limpaLugarAntigoInimigo(posicaoInimigo, i);
                        Mapa.mapa[variacaoPosicoes[0],variacaoPosicoes[1]] = inimigos[i]._aparencia;
                        posicaoInimigo[i, 0] = variacaoPosicoes[0];
                        posicaoInimigo[i, 1] = variacaoPosicoes[1];
                        isMovimentoInimigoFoiRealizado = true;
                    }
                    else if(destino == "{]"){
                        int idtipoInimigo = todosTiposInimigo
                            .FindIndex(inimigo => 
                                inimigo == tipoInimigo._aparencia)
                            + 1;
                        Game.MatarInimigo((enumInimigos)idtipoInimigo);
                        
                        limpaLugarAntigoInimigo(posicaoInimigo, i);
                        ItemDefesa.usandoDefesa = false;
                        ItemAtaque.nmrPuloAtaqueValido = 0;
                        destino = "()";
                        limpaLugarAntigoInimigo(posicaoInimigo, i);
                        isMovimentoInimigoFoiRealizado = true;
                    }
                    else if(destino == "{}"){
                        int idtipoInimigo = todosTiposInimigo
                            .FindIndex(inimigo => 
                                inimigo == tipoInimigo._aparencia)
                            + 1;
                        Game.MatarInimigo((enumInimigos)idtipoInimigo);

                        limpaLugarAntigoInimigo(posicaoInimigo, i);
                        ItemAtaque.nmrPuloAtaqueValido = 0;
                        destino = "()";
                        limpaLugarAntigoInimigo(posicaoInimigo, i);
                        isMovimentoInimigoFoiRealizado = true;
                    }
                    else if(destino == "[]"){
                        ItemDefesa.usandoDefesa = false;
                        destino = "()";
                        isMovimentoInimigoFoiRealizado = true;
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
            Mapa.CheckMapaIsRenderizando();
        }

        public static async Task IntervaloMovimentoInimigo(){
            if (Game.qntInimigosTipo1 > 0) {              
                await Task.Delay((int)(1500 * Game.dificuldade));
                MovimentacaoInimigo(tiposInimigosBase);
                await IntervaloMovimentoInimigo();  
            }
        }

        public static async Task IntervaloMovimentoInimigo2(){
            if (Game.qntInimigosTipo2 > 0) {
                await Task.Delay((int)(750 * Game.dificuldade));
                MovimentacaoInimigo(tiposInimigosBase2);
                await IntervaloMovimentoInimigo2();  
            }
        }
    }
}