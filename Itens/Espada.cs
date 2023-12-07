namespace jogoInicial
{
    public class Espada
    {
        public static int quantidade = 0;

        public static int nmrPuloAtaqueValido = 0;

        public static async Task IntervaloVerificaEspada(int faseAtual){
            await Task.Delay(15000);
            if (faseAtual != Game.nivelFase) return;

            VerificaEspada();
            await IntervaloVerificaEspada(faseAtual);  
        }

        public static void VerificaEspada(){
            bool achouItem = false;

            for (int i = 0; i < Mapa.mapa.GetLength(0); i++){
                for (int j = 0; j < Mapa.mapa.GetLength(1); j++){
                    if(Mapa.mapa[i,j] == "<-"){
                        achouItem = true;
                    }
                }
            }

            if(!achouItem && quantidade == 0 && nmrPuloAtaqueValido == 0){
                List<List<int>> spawnsDisponiveis = new List<List<int>>{
                    new List<int>{5, 16}, new List<int>{1, 1}, new List<int>{5, 9}
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
                        ] = "<-";
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
    }
}