using System.Runtime.Serialization;

namespace jogoInicial
{
    public class Arco
    {
        public static int quantidade = 0;
        public static bool equipado = false;
        public static async Task IntervaloVerificaArco(int faseAtual){
            await Task.Delay(15000);
            if (faseAtual != Game.nivelFase) return;

            VerificaArco();
            await IntervaloVerificaArco(faseAtual);  
        }

        public static void VerificaArco(){
            bool achouItem = false;

            for (int i = 0; i < Mapa.mapa.GetLength(0); i++){
                for (int j = 0; j < Mapa.mapa.GetLength(1); j++){
                    if(Mapa.mapa[i,j] == "D-"){
                        achouItem = true;
                    }
                }
            }

            if(!achouItem && quantidade == 0 && equipado == false){
                List<List<int>> spawnsDisponiveis = new List<List<int>>{
                    new List<int>{10,5}, new List<int>{9,12}, new List<int>{4,18}
                };

                int tentativasDeSpawn = spawnsDisponiveis.Count;
                
                for (int c = 0; c < tentativasDeSpawn; c++) {
                    int randomIndex = new Random().Next(0, spawnsDisponiveis.Count);

                    if(Mapa.mapa[
                        spawnsDisponiveis.ElementAt(randomIndex).ElementAt(0), 
                        spawnsDisponiveis.ElementAt(randomIndex).ElementAt(1)
                    ] == "  "){
                        Mapa.mapa[
                            spawnsDisponiveis.ElementAt(randomIndex).ElementAt(0), 
                            spawnsDisponiveis.ElementAt(randomIndex).ElementAt(1)
                        ] = "D-";
                        break;

                    }else if(Personagem.modelosPersonagem.Contains(
                        Mapa.mapa[
                            spawnsDisponiveis.ElementAt(randomIndex).ElementAt(0), 
                            spawnsDisponiveis.ElementAt(randomIndex).ElementAt(1)
                        ]
                    )){
                        quantidade +=1;
                        break;
                    } else {
                        spawnsDisponiveis.RemoveAt(randomIndex);
                    }
                }

                Mapa.CheckMapaIsRenderizando();
            }
        }

        static string limpaLugarAntigoFlecha(List<int> posicao) => 
            Mapa.mapa[posicao[0], posicao[1]] = "  ";

        public static async Task AtirarFlecha(Direcao direcao, List<int> posicaoFlecha) {
            List<int> destinoPosicoes = new List<int>(posicaoFlecha);

            if (
                destinoPosicoes[0] == Mapa.mapa.GetLength(0) - 1
                || destinoPosicoes[1] == Mapa.mapa.GetLength(1) - 1
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
            
            string destinoString = Mapa.mapa[destinoPosicoes[0], destinoPosicoes[1]];

            if (destinoString == "  ") {
                if (!(Personagem.modelosPersonagem.Contains(Mapa.mapa[posicaoFlecha[0], posicaoFlecha[1]]))) {
                    limpaLugarAntigoFlecha(posicaoFlecha);
                }
                switch (direcao) {
                    case Direcao.Cima:
                        Mapa.mapa[destinoPosicoes[0], destinoPosicoes[1]] = " i";
                    break;
                    case Direcao.Baixo:
                        Mapa.mapa[destinoPosicoes[0], destinoPosicoes[1]] = " !";
                    break;
                    case Direcao.Direita:
                        Mapa.mapa[destinoPosicoes[0], destinoPosicoes[1]] = "--";
                    break; 
                    case Direcao.Esquerda:
                        Mapa.mapa[destinoPosicoes[0], destinoPosicoes[1]] = "--";
                    break;                                      
                }
                Mapa.CheckMapaIsRenderizando();
                await Task.Delay(650);
                await AtirarFlecha(direcao, destinoPosicoes);                
            }else{
                if(Inimigo.todosTiposInimigo.Find(inimigo => inimigo == destinoString) != null ){
                    int tipoInimigo = Inimigo.todosTiposInimigo.FindIndex(inimigo => inimigo == destinoString) + 1;
                    Mapa.mapa[destinoPosicoes[0],destinoPosicoes[1]] = "  ";

                    Game.MatarInimigo((enumInimigos)tipoInimigo);

                }else if(Inventario.todosTiposItens.FindIndex((i) => i == destinoString) >= 0){
                    Mapa.mapa[destinoPosicoes[0],destinoPosicoes[1]] = "  ";

                }else if(Personagem.modelosPersonagem.Contains(destinoString)){
                    if (Personagem.modelosPersonagemComEscudo.Contains(destinoString)) {
                        Escudo.usandoDefesa = false;
                        Mapa.mapa[destinoPosicoes[0], destinoPosicoes[1]] = Personagem.GetPersonagemEquipado();
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