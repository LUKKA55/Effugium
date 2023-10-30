namespace jogoInicial
{
    public class Inventario
    {
        public static void MostrarInventario(){
            if(ItemAtaque.nmrPuloAtaqueValido > 0){
                Console.WriteLine("Ataque tem efeito pelos próximos {0} pulos", ItemAtaque.nmrPuloAtaqueValido);
            }
            if(ItemDefesa.usandoDefesa){
                Console.WriteLine("Está protegido de UM ataque");
            }
            if (ItemAtaque.quantidade > 0 || ItemDefesa.quantidade > 0){
                Console.WriteLine("Inventário");
            }
            if (ItemAtaque.quantidade > 0 ){
                Console.WriteLine(" -Item para ataque {0}", ItemAtaque.quantidade);
            }
            if (ItemDefesa.quantidade > 0 ){
                Console.WriteLine(" -Item para defesa {0}", ItemDefesa.quantidade);
            }
        }

        public static void UsarItem(char numeroAcao){
            int acao = int.Parse(numeroAcao.ToString());
            int[] posicao = new int[2];

            for (int i = 0; i < Mapa.mapa.GetLength(0); i++){
                for (int j = 0; j < Mapa.mapa.GetLength(1); j++){
                    if(Mapa.mapa[i,j] == "()" || Mapa.mapa[i,j] == "{}" || Mapa.mapa[i,j] == "[]" || Mapa.mapa[i,j] == "{]"){
                        posicao[0] = i;
                        posicao[1] = j;
                    }
                }
            }
       
            switch (acao)
            {
                case 1:
                    if(ItemAtaque.quantidade > 0){
                        ItemAtaque.quantidade--;
                        ItemAtaque.nmrPuloAtaqueValido = 10;

                        if(ItemDefesa.usandoDefesa){
                            Mapa.mapa[posicao[0],posicao[1]] = "{]";
                            Mapa.CheckMapaIsRenderizando();
                        }else{
                            Mapa.mapa[posicao[0],posicao[1]] = "{}";
                            Mapa.CheckMapaIsRenderizando();
                        }
                    }
                break;
                case 2:
                    if(ItemDefesa.quantidade > 0){
                        ItemDefesa.quantidade--;
                        ItemDefesa.usandoDefesa = true;

                        if(ItemAtaque.nmrPuloAtaqueValido > 0){
                            Mapa.mapa[posicao[0],posicao[1]] = "{]";
                            Mapa.CheckMapaIsRenderizando();
                        }else{
                            Mapa.mapa[posicao[0],posicao[1]] = "[]";
                            Mapa.CheckMapaIsRenderizando();
                        }
                    }
                break;
            }
        }    
    }
}