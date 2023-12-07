namespace jogoInicial
{
    public class Picareta
    {
        public static int quantidade = 0;

        public static bool equipada = false;

        public static async Task IntervaloVerificaPicareta(int faseAtual){
            await Task.Delay(15000);
            if (faseAtual != Game.nivelFase) return;
            
            VerificaPicareta();
            await IntervaloVerificaPicareta(faseAtual);  
        }

        public static void VerificaPicareta(){
            bool achouItem = false;

            for (int i = 0; i < Mapa.mapa.GetLength(0); i++){
                for (int j = 0; j < Mapa.mapa.GetLength(1); j++){
                    if(Mapa.mapa[i,j] == "T "){
                        achouItem = true;
                    }
                }
            }

            if(!achouItem && quantidade == 0 && equipada == false){
                List<List<int>> spawnsDisponiveis = new List<List<int>>{
                    new List<int>{11, 1}, new List<int>{2, 17}, new List<int>{2, 1}
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
                        ] = "T ";
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