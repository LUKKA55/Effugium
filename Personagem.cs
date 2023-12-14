namespace jogoInicial
{
    public class Personagem
    {
        public static List<int> posicaoPersonagem = new (){2, 9};
        public static List<bool> jaEncontrouOItem = new(){
            false,
            false,
            false,
            false
        };
        public static Inventario inventario = new();

        public static string GetPersonagemEquipado() {  
            string personagemAtualizado = "()";
            if (inventario.espada.nmrPuloAtaqueValido > 0)
                personagemAtualizado = "{}";
            
            if (inventario.escudo._equipado) 
                personagemAtualizado = personagemAtualizado == "{}" ? "[}" : "[]";
            
            if (inventario.picareta._equipado) 
                personagemAtualizado = personagemAtualizado == "[]" ? "[T" : "(T";

            if (inventario.arco._equipado)
                personagemAtualizado = personagemAtualizado == "[]" ? "[D" : "(D";
            
            return personagemAtualizado;
        }
        public static void Movimentacao(Direcao direcao){
            int[] posicao = new int[2];
            for (int i = 0; i < Game.GetMapa().GetLength(0); i++){
                for (int j = 0; j < Game.GetMapa().GetLength(1); j++){
                    if(DB.modelosPersonagem.Contains(Game.GetMapa()[i,j])){
                        posicao[0] = i;
                        posicao[1] = j;
                    }
                }
            }
            static string limpaLugarAntigo(int[] posicao) => Game.GetMapa()[posicao[0],posicao[1]] = "  ";

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

            string destino = Game.GetMapa()[variacaoPosicao[0],variacaoPosicao[1]];

            if (inventario.arco._equipado){
                inventario.arco._equipado = false;
                inventario.arco.AtirarFlecha(direcao, posicaoPersonagem);
                Mapa.CheckMapaIsRenderizando();
                return;
            }
            if(destino == "  "){
                limpaLugarAntigo(posicao);

                if(inventario.espada.nmrPuloAtaqueValido > 0)
                    inventario.espada.nmrPuloAtaqueValido--;

                Game.GetMapa()[variacaoPosicao[0],variacaoPosicao[1]] = GetPersonagemEquipado();    

                posicaoPersonagem[0] = variacaoPosicao[0];
                posicaoPersonagem[1] = variacaoPosicao[1];
                
                Game.CheckProximaFase();

            }else if(DB.todosTiposItens.FindIndex((i) => i._modelo == destino) >= 0){
                int tipoItem = DB.todosTiposItens.FindIndex((i) => i._modelo == destino);

                inventario.AcrescentarItemInventario((EnumItens)tipoItem);

                if (!jaEncontrouOItem[tipoItem]) {
                    Game.telaInfoAberta = true;
                    jaEncontrouOItem[tipoItem] = true;
                    
                    ConsoleKeyInfo key;
                    do {
                        MostrarMensagem.InfoItem((EnumItens)tipoItem);
                        key = Console.ReadKey();
                    } while (key.Key != ConsoleKey.C);

                    Game.telaInfoAberta = false;
                    Mapa.CheckMapaIsRenderizando();
                }

                limpaLugarAntigo(posicao);

                if(inventario.espada.nmrPuloAtaqueValido > 0)
                    inventario.espada.nmrPuloAtaqueValido--;

                Game.GetMapa()[variacaoPosicao[0],variacaoPosicao[1]] = GetPersonagemEquipado();    

                posicaoPersonagem[0] = variacaoPosicao[0];
                posicaoPersonagem[1] = variacaoPosicao[1];

            }else if(destino == "~~"  || destino == "//"){
                if(inventario.picareta._equipado){
                    inventario.picareta._equipado = false;
                    Game.GetMapa()[variacaoPosicao[0],variacaoPosicao[1]] = "  ";
                    posicaoPersonagem[0] = variacaoPosicao[0];
                    posicaoPersonagem[1] = variacaoPosicao[1];
                }
            }else if(destino == "||" || destino == "=="){
                if(
                    variacaoPosicao[0] > 0 && variacaoPosicao[0] < Game.GetMapa().GetLength(0)-1 && 
                    variacaoPosicao[1] > 0 && variacaoPosicao[1] < Game.GetMapa().GetLength(1)-1 &&
                    inventario.picareta._equipado
                ){
                    inventario.picareta._equipado = false;
                    Game.GetMapa()[variacaoPosicao[0],variacaoPosicao[1]] = "  ";

                    posicaoPersonagem[0] = variacaoPosicao[0];
                    posicaoPersonagem[1] = variacaoPosicao[1];
                }
            }else if(inventario.espada.nmrPuloAtaqueValido > 0 && DB.todosTiposInimigo.Find(inimigo => inimigo == destino) != null){
                int tipoInimigo = DB.todosTiposInimigo.FindIndex(inimigo => inimigo == destino);

                limpaLugarAntigo(posicao);
                
                inventario.espada.nmrPuloAtaqueValido = 0;
                Game.GetMapa()[variacaoPosicao[0],variacaoPosicao[1]] = GetPersonagemEquipado();
                
                MatarInimigo((EnumInimigos)tipoInimigo);

                posicaoPersonagem[0] = variacaoPosicao[0];
                posicaoPersonagem[1] = variacaoPosicao[1];
            }
            
            Mapa.CheckMapaIsRenderizando();
        }

        public static void ResetPosicaoPersonagem() {
            posicaoPersonagem[0] = 2;
            posicaoPersonagem[1] = 9;
        }

        public static void MatarInimigo(EnumInimigos tipoInimigo) {
            switch(tipoInimigo) {
                case EnumInimigos.tipo1:
                    Game.FaseAtual._qntInimigosTipo1--;
                break;
                case EnumInimigos.tipo2:
                    Game.FaseAtual._qntInimigosTipo2--;              
                break;
                case EnumInimigos.tipo3:
                    Game.FaseAtual._qntInimigosTipo3--;             
                break; 
                case EnumInimigos.tipo4:
                    Game.FaseAtual._qntInimigosTipo4--;             
                break;
                case EnumInimigos.tipo5:
                    Game.FaseAtual._qntInimigosTipo5--;             
                break;                                                                   
            }
            Game.CheckProximaFase();
        }
    }
}