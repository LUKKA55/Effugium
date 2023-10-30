namespace jogoInicial
{
    public class Personagem
    {
        public static void Movimentacao(Game.Direcao direcao){
            int[] posicao = new int[2];
            for (int i = 0; i < Mapa.mapa.GetLength(0); i++){
                for (int j = 0; j < Mapa.mapa.GetLength(1); j++){
                    if(Mapa.mapa[i,j] == "()" || Mapa.mapa[i,j] == "{}" || Mapa.mapa[i,j] == "[]" || Mapa.mapa[i,j] == "{]"){
                        posicao[0] = i;
                        posicao[1] = j;
                    }
                }
            }
            static string limpaLugarAntigo(int[] posicao) => Mapa.mapa[posicao[0],posicao[1]] = "  ";

            int[] variacaoPosicao = new int[2];

            int variacaoPosicaoZero = (int)direcao % 2 != 0 
                ? (int)direcao == 1 
                    ? -1 
                    : 1 
                : 0;
                
            int variacaoPosicaoUm = (int)direcao % 2 == 0
                ? (int)direcao == 2 
                    ? -1 
                    : 1 
                : 0;

            variacaoPosicao[0] = posicao[0] + variacaoPosicaoZero;
            variacaoPosicao[1] = posicao[1] + variacaoPosicaoUm;

            string destino = Mapa.mapa[variacaoPosicao[0],variacaoPosicao[1]];

            if(destino == "  "){
                limpaLugarAntigo(posicao);
                if(ItemAtaque.nmrPuloAtaqueValido > 0 && ItemDefesa.usandoDefesa){
                    Mapa.mapa[variacaoPosicao[0],variacaoPosicao[1]] = "{]";
                    ItemAtaque.nmrPuloAtaqueValido--;

                }else if(ItemAtaque.nmrPuloAtaqueValido > 0){
                    Mapa.mapa[variacaoPosicao[0],variacaoPosicao[1]] = "{}";
                    ItemAtaque.nmrPuloAtaqueValido--;

                }else if (ItemDefesa.usandoDefesa){
                    Mapa.mapa[variacaoPosicao[0],variacaoPosicao[1]] = "[]";
                    
                }else{
                    Mapa.mapa[variacaoPosicao[0],variacaoPosicao[1]] = "()";
                }
            }else if(destino == "<-"){
                limpaLugarAntigo(posicao);
                if (ItemDefesa.usandoDefesa){
                    Mapa.mapa[variacaoPosicao[0],variacaoPosicao[1]] = "[]";
                    
                }else{
                    Mapa.mapa[variacaoPosicao[0],variacaoPosicao[1]] = "()";
                }
                ItemAtaque.quantidade++;

            }else if(destino == "<>"){
                limpaLugarAntigo(posicao);
                if(ItemAtaque.nmrPuloAtaqueValido > 0){
                    Mapa.mapa[variacaoPosicao[0],variacaoPosicao[1]] = "{}";
                    ItemAtaque.nmrPuloAtaqueValido--;

                }else{
                    Mapa.mapa[variacaoPosicao[0],variacaoPosicao[1]] = "()";
                }
                ItemDefesa.quantidade++;

            }else if(ItemAtaque.nmrPuloAtaqueValido > 0 && destino == "XX"){
                limpaLugarAntigo(posicao);
                ItemAtaque.nmrPuloAtaqueValido = 0;
                if(ItemDefesa.usandoDefesa){
                    Mapa.mapa[variacaoPosicao[0],variacaoPosicao[1]] = "[]";
                    
                }else{
                    Mapa.mapa[variacaoPosicao[0],variacaoPosicao[1]] = "()";
                }
            }
              
            Mapa.CheckMapaIsRenderizando();
        }
    }
}