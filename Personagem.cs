
namespace jogoInicial
{
    public class Personagem
    {
        public static List<int> posicaoPersonagem = new (){2, 9};
        public static string[] modelosPersonagem = { "()", "[]", "{}", "[}", "(T", "[T", "[D", "(D" };
        public static string[] modelosPersonagemComEscudo = { "[]", "[}", "[T", "[D" };

        public static string GetPersonagemEquipado() {  
            string personagemAtualizado = "()";
            if (Espada.nmrPuloAtaqueValido > 0)
                personagemAtualizado = "{}";
            
            if (Escudo.usandoDefesa) 
                personagemAtualizado = personagemAtualizado == "{}" ? "[}" : "[]";
            
            if (Picareta.equipada) 
                personagemAtualizado = personagemAtualizado == "[]" ? "[T" : "(T";

            if (Arco.equipado)
                personagemAtualizado = personagemAtualizado == "[]" ? "[D" : "(D";
            
            return personagemAtualizado;
        }
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

            if (Arco.equipado){
                Arco.equipado = false;
                Arco.AtirarFlecha(direcao, posicaoPersonagem);
                Mapa.CheckMapaIsRenderizando();
                return;
            }
            if(destino == "  "){
                limpaLugarAntigo(posicao);

                if(Espada.nmrPuloAtaqueValido > 0)
                    Espada.nmrPuloAtaqueValido--;

                Mapa.mapa[variacaoPosicao[0],variacaoPosicao[1]] = GetPersonagemEquipado();    

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

                if(tipoItem == 3)
                    Arco.quantidade++;

                limpaLugarAntigo(posicao);

                if(Espada.nmrPuloAtaqueValido > 0)
                    Espada.nmrPuloAtaqueValido--;

                Mapa.mapa[variacaoPosicao[0],variacaoPosicao[1]] = GetPersonagemEquipado();    

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
                Mapa.mapa[variacaoPosicao[0],variacaoPosicao[1]] = GetPersonagemEquipado();
                
                Game.MatarInimigo((enumInimigos)tipoInimigo);

                posicaoPersonagem[0] = variacaoPosicao[0];
                posicaoPersonagem[1] = variacaoPosicao[1];
            }
            
            Mapa.CheckMapaIsRenderizando();
        }
    }
}