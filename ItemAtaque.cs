namespace jogoInicial
{
    public class ItemAtaque
    {
        public static int quantidade = 0;

        public static int nmrPuloAtaqueValido = 0;

        public static async Task IntervaloVerificaItemAtaque(){
            await Task.Delay(20000);
            VerificaItemAtaque();
            await IntervaloVerificaItemAtaque();  
        }

        public static void VerificaItemAtaque(){
            bool achouItem = false;

            for (int i = 0; i < Mapa.mapa.GetLength(0); i++){
                for (int j = 0; j < Mapa.mapa.GetLength(1); j++){
                    if(Mapa.mapa[i,j] == "<-"){
                        achouItem = true;
                    }
                }
            }

            if(!achouItem && quantidade == 0 && nmrPuloAtaqueValido == 0){
                if(Mapa.mapa[5,16] == "  "){
                    Mapa.mapa[5,16] = "<-";
                }else if(Mapa.mapa[1,1] == "  "){
                    Mapa.mapa[1,1] = "<-";
                }else if(Mapa.mapa[5,9] == "  "){
                    Mapa.mapa[5,9] = "<-";
                }
                Mapa.CheckMapaIsRenderizando();
            }
        }
    }
}