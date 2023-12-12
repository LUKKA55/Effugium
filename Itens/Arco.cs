namespace jogoInicial
{
    public class Arco : ModeloBaseItem {
        public bool _equipado = false;
        public Arco(EnumItens tipoItem, List<List<int>> spawnsDisponiveis) : base(tipoItem, spawnsDisponiveis){}
        static string limpaLugarAntigoFlecha(List<int> posicao) => 
            Game.GetMapa()[posicao[0], posicao[1]] = "  ";

        public async Task AtirarFlecha(Direcao direcao, List<int> posicaoFlecha) {
            List<int> destinoPosicoes = new List<int>(posicaoFlecha);

            if (
                destinoPosicoes[0] == Game.GetMapa().GetLength(0) - 1
                || destinoPosicoes[1] == Game.GetMapa().GetLength(1) - 1
                || destinoPosicoes[0] == 0
                || destinoPosicoes[1] == 0
            ) {
                limpaLugarAntigoFlecha(posicaoFlecha);
                return;
            }

            switch (direcao) {
                case Direcao.Cima:
                    destinoPosicoes[0] = posicaoFlecha[0]-1;
                break;
                case Direcao.Baixo:
                    destinoPosicoes[0] = posicaoFlecha[0]+1;
                break;
                case Direcao.Direita:
                    destinoPosicoes[1] = posicaoFlecha[1]+1;
                break; 
                case Direcao.Esquerda:
                    destinoPosicoes[1] = posicaoFlecha[1]-1;
                break;       
            }
            
            string destinoString = Game.GetMapa()[destinoPosicoes[0], destinoPosicoes[1]];

            if (destinoString == "  ") {
                if (!DB.modelosPersonagem.Contains(Game.GetMapa()[posicaoFlecha[0], posicaoFlecha[1]])) {
                    limpaLugarAntigoFlecha(posicaoFlecha);
                }
                switch (direcao) {
                    case Direcao.Cima:
                        Game.GetMapa()[destinoPosicoes[0], destinoPosicoes[1]] = " i";
                    break;
                    case Direcao.Baixo:
                        Game.GetMapa()[destinoPosicoes[0], destinoPosicoes[1]] = " !";
                    break;
                    case Direcao.Direita:
                        Game.GetMapa()[destinoPosicoes[0], destinoPosicoes[1]] = "--";
                    break; 
                    case Direcao.Esquerda:
                        Game.GetMapa()[destinoPosicoes[0], destinoPosicoes[1]] = "--";
                    break;                                      
                }
                Mapa.CheckMapaIsRenderizando();
                await Task.Delay(650);
                await AtirarFlecha(direcao, destinoPosicoes);                
            }else{
                if(DB.todosTiposInimigo.Find(inimigo => inimigo == destinoString) != null ){
                    int tipoInimigo = DB.todosTiposInimigo.FindIndex(inimigo => inimigo == destinoString) + 1;
                    Game.GetMapa()[destinoPosicoes[0],destinoPosicoes[1]] = "  ";

                    Personagem.MatarInimigo((EnumInimigos)tipoInimigo);

                }else if(DB.todosTiposItens.FindIndex((i) => i._modelo == destinoString) >= 0){
                    Game.GetMapa()[destinoPosicoes[0],destinoPosicoes[1]] = "  ";

                }else if(DB.modelosPersonagem.Contains(destinoString)){
                    if (DB.modelosPersonagemComEscudo.Contains(destinoString)) {
                        Personagem.inventario.escudo._equipado = false;
                        Game.GetMapa()[destinoPosicoes[0], destinoPosicoes[1]] = Personagem.GetPersonagemEquipado();
                    } else {
                        MostrarMensagem.GameOver();
                        Environment.Exit(0);
                    };
                };
                limpaLugarAntigoFlecha(posicaoFlecha);
                Mapa.CheckMapaIsRenderizando();         
            }        
        }
    }
}