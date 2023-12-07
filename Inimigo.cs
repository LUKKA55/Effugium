namespace jogoInicial
{
    public class Inimigo
    {
        static public List<string> todosTiposInimigo = new(){"XX", ")("};
        static public TipoInimigo tipoInimigo1 = new("XX");
        static public TipoInimigo tipoInimigo2 = new(")(");
        
        // Variacao da movimentação do inimigo
        static public List<List<int>> altPosicao = new List<List<int>>{
            new List<int>{1, 0}, 
            new List<int>{-1, 0}, 
            new List<int>{0, 1}, 
            new List<int>{0, -1}, 
        };

        public static void MovimentacaoInimigo (TipoInimigo tipoInimigo){
            int[,] posicoesInimigos = new int[Mapa.mapa.GetLength(0) * Mapa.mapa.GetLength(1), 2];

            int qntInimigos = 0;

            for (int i = 0; i < Mapa.mapa.GetLength(0); i++){
                for (int j = 0; j < Mapa.mapa.GetLength(1); j++){
                    if(tipoInimigo._aparencia == Mapa.mapa[i,j]){
                        posicoesInimigos[qntInimigos, 0] = i;
                        posicoesInimigos[qntInimigos, 1] = j;
                        qntInimigos++;
                    }
                }
            }

            static string limpaLugarAntigoInimigo(int[,] posicaoInimigo, int i) => 
                Mapa.mapa[posicaoInimigo[i, 0], posicaoInimigo[i, 1]] = "  ";


            for(int i = 0; i < qntInimigos; i++){
                List<List<int>> cloneAltPosicao = new List<List<int>>(altPosicao);
                
                for(int c = 0; c < altPosicao.Count; c++) {
                    int idxAleatorio = new Random().Next(0, cloneAltPosicao.Count);

                    string destino = Mapa.mapa[
                        cloneAltPosicao.ElementAt(idxAleatorio).ElementAt(0) + posicoesInimigos[i, 0],
                        cloneAltPosicao.ElementAt(idxAleatorio).ElementAt(1) + posicoesInimigos[i, 1]
                    ];

                    void acaoInimigo(bool destroiItem = false) => 
                        Mapa.mapa[ 
                            cloneAltPosicao.ElementAt(idxAleatorio).ElementAt(0) + posicoesInimigos[i, 0],
                            cloneAltPosicao.ElementAt(idxAleatorio).ElementAt(1) + posicoesInimigos[i, 1]
                        ] = destroiItem 
                            ? 
                            "()" 
                            : 
                            tipoInimigo._aparencia;
                    
                    if(
                        destino == "  " || 
                        Inventario.todosTiposItens.FindIndex(i => i == destino) >= 0
                    ){
                        limpaLugarAntigoInimigo(posicoesInimigos, i);
                        acaoInimigo();
                        break;
                        
                    } else if(destino == "{]"){
                        // Procura inimigo nos tipos para matar, precisa de index NAO aparencia
                        int idtipoInimigo = todosTiposInimigo
                            .FindIndex(inimigo => 
                                inimigo == tipoInimigo._aparencia)
                            + 1;
                        Game.MatarInimigo((enumInimigos)idtipoInimigo);
                        ItemDefesa.usandoDefesa = false;
                        ItemAtaque.nmrPuloAtaqueValido = 0;
                        acaoInimigo(true);
                        limpaLugarAntigoInimigo(posicoesInimigos, i);
                        break;

                    } else if(destino == "{}"){
                        int idtipoInimigo = todosTiposInimigo
                            .FindIndex(inimigo => 
                                inimigo == tipoInimigo._aparencia)
                            + 1;
                        Game.MatarInimigo((enumInimigos)idtipoInimigo);
                        ItemAtaque.nmrPuloAtaqueValido = 0;
                        acaoInimigo(true);
                        limpaLugarAntigoInimigo(posicoesInimigos, i);
                        break;

                    } else if(destino == "[]"){
                        ItemDefesa.usandoDefesa = false;
                        acaoInimigo(true);
                        break;
                        
                    } else if(destino == "()"){
                        limpaLugarAntigoInimigo(posicoesInimigos, i);
                        acaoInimigo();
                        Mapa.CheckMapaIsRenderizando();
                        MostrarMensagem.GameOver();
                        Environment.Exit(0);
                        
                    } else {
                        cloneAltPosicao.RemoveAt(idxAleatorio);
                    }
                };
            }
            Mapa.CheckMapaIsRenderizando();
        }
        public static async Task IntervaloMovimentoInimigo(){
            if (Game.qntInimigosTipo1 > 0) {              
                await Task.Delay((int)(1500 * Game.dificuldade));
                MovimentacaoInimigo(tipoInimigo1);
                await IntervaloMovimentoInimigo();  
            }
        }
        public static async Task IntervaloMovimentoInimigo2(){
            if (Game.qntInimigosTipo2 > 0) {
                await Task.Delay((int)(750 * Game.dificuldade));
                MovimentacaoInimigo(tipoInimigo2);
                await IntervaloMovimentoInimigo2();  
            }
        }
    }
}