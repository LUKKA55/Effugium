
namespace jogoInicial
{
    public class Personagem
    {
        public static int[] posicaoPersonagem = new int[2]{2, 9};
        public static string[] modelosPersonagem = { "()", "[]", "{}", "{]", "(T", "[T" };
        public static void Movimentacao(Direcao direcao){
            int[] posicao = new int[2];
            for (int i = 0; i < Mapa.mapa.GetLength(0); i++){
                for (int j = 0; j < Mapa.mapa.GetLength(1); j++){
                    if(modelosPersonagem.Contains(Mapa.mapa[i,j])){
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
                if(Espada.nmrPuloAtaqueValido > 0 && Escudo.usandoDefesa){
                    Mapa.mapa[variacaoPosicao[0],variacaoPosicao[1]] = "{]";
                    Espada.nmrPuloAtaqueValido--;
               

                }else if(Escudo.usandoDefesa && Picareta.equipada){
                    Mapa.mapa[variacaoPosicao[0],variacaoPosicao[1]] = "[T";

                }else if(Espada.nmrPuloAtaqueValido > 0){
                    Mapa.mapa[variacaoPosicao[0],variacaoPosicao[1]] = "{}";
                    Espada.nmrPuloAtaqueValido--;

                }else if (Escudo.usandoDefesa){
                    Mapa.mapa[variacaoPosicao[0],variacaoPosicao[1]] = "[]";
                    
                }else if(Picareta.equipada){
                    Mapa.mapa[variacaoPosicao[0],variacaoPosicao[1]] = "(T";

                }else{
                    Mapa.mapa[variacaoPosicao[0],variacaoPosicao[1]] = "()";
                }

                if(
                    !(variacaoPosicao[0] > 0 && variacaoPosicao[0] < Mapa.mapa.GetLength(0)-1 && 
                    variacaoPosicao[1] > 0 && variacaoPosicao[1] < Mapa.mapa.GetLength(1)-1)
                ) {
                    Game.ProximaFase();
                }
                posicaoPersonagem[0] = variacaoPosicao[0];
                posicaoPersonagem[1] = variacaoPosicao[1];

            }else if(Inventario.todosTiposItens.FindIndex((i) => i == destino) >= 0){
                int tipoItem = Inventario.todosTiposItens.FindIndex((i) => i == destino);

                if(tipoItem == 0)
                    Espada.quantidade++;

                if(tipoItem == 1)
                    Escudo.quantidade++;

                if(tipoItem == 2)
                    Picareta.quantidade++;

                limpaLugarAntigo(posicao);

                if(Escudo.usandoDefesa && Picareta.equipada){
                    Mapa.mapa[variacaoPosicao[0],variacaoPosicao[1]] = "[T";

                }else if(Espada.nmrPuloAtaqueValido > 0 && Escudo.usandoDefesa){
                    Mapa.mapa[variacaoPosicao[0],variacaoPosicao[1]] = "{]";
                    Espada.nmrPuloAtaqueValido--;

                }else if(Espada.nmrPuloAtaqueValido > 0){
                    Mapa.mapa[variacaoPosicao[0],variacaoPosicao[1]] = "{}";
                    Espada.nmrPuloAtaqueValido--;

                }else if (Escudo.usandoDefesa){
                    Mapa.mapa[variacaoPosicao[0],variacaoPosicao[1]] = "[]";

                }else if(Picareta.equipada){
                    Mapa.mapa[variacaoPosicao[0],variacaoPosicao[1]] = "(T";

                }else{
                    Mapa.mapa[variacaoPosicao[0],variacaoPosicao[1]] = "()";
                }
                posicaoPersonagem[0] = variacaoPosicao[0];
                posicaoPersonagem[1] = variacaoPosicao[1];

            }else if(destino == "~~"  || destino == "//"){
                if(Picareta.equipada){
                    Picareta.equipada = false;
                    Mapa.mapa[variacaoPosicao[0],variacaoPosicao[1]] = "  ";
                    posicaoPersonagem[0] = variacaoPosicao[0];
                    posicaoPersonagem[1] = variacaoPosicao[1];
                }
            }else if(destino == "||" || destino == "=="){
                if(
                    variacaoPosicao[0] > 0 && variacaoPosicao[0] < Mapa.mapa.GetLength(0)-1 && 
                    variacaoPosicao[1] > 0 && variacaoPosicao[1] < Mapa.mapa.GetLength(1)-1 &&
                    Picareta.equipada
                ){
                    Picareta.equipada = false;
                    Mapa.mapa[variacaoPosicao[0],variacaoPosicao[1]] = "  ";
                    posicaoPersonagem[0] = variacaoPosicao[0];
                    posicaoPersonagem[1] = variacaoPosicao[1];
                }
            }else if(Espada.nmrPuloAtaqueValido > 0 && Inimigo.todosTiposInimigo.Find(inimigo => inimigo == destino) != null){
                int tipoInimigo = Inimigo.todosTiposInimigo.FindIndex(inimigo => inimigo == destino) + 1;

                limpaLugarAntigo(posicao);
                
                Espada.nmrPuloAtaqueValido = 0;
                Mapa.mapa[variacaoPosicao[0],variacaoPosicao[1]] = Escudo.usandoDefesa ? "[]" : "()";
                
                Game.MatarInimigo((enumInimigos)tipoInimigo);
                posicaoPersonagem[0] = variacaoPosicao[0];
                posicaoPersonagem[1] = variacaoPosicao[1];
            }
            
            Mapa.CheckMapaIsRenderizando();
        }
    }
}