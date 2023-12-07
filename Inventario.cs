namespace jogoInicial
{
    public class Inventario
    {
        static public List<string> todosTiposItens= new(){"<-", "<>", "T "}; 
        public static void MostrarInventario(){
            if(Espada.nmrPuloAtaqueValido > 0){
                Console.WriteLine("Ataque tem efeito pelos próximos {0} pulos", Espada.nmrPuloAtaqueValido);
            }
            if(Escudo.usandoDefesa){
                Console.WriteLine("Está protegido de UM ataque");
            }
            if(Picareta.equipada){
                Console.WriteLine("Está pode quebrar UMA parede do mapa (menos as bordas)");
            }
            if (Espada.quantidade > 0 || Escudo.quantidade > 0 || Picareta.quantidade > 0){
                Console.WriteLine("Inventário");
            }
            if (Espada.quantidade > 0 ){
                Console.WriteLine(" -Espada para ataque {0}", Espada.quantidade);
            }
            if (Escudo.quantidade > 0 ){
                Console.WriteLine(" -Escudo para defesa {0}", Escudo.quantidade);
            }
            if (Picareta.quantidade > 0 ){
                Console.WriteLine(" -Picareta para quebrar parede {0}", Picareta.quantidade);
            }
        }

        public static void UsarItem(char numeroAcao){
            int acao = int.Parse(numeroAcao.ToString());
            int[] posicao = new int[2];

            for (int i = 0; i < Mapa.mapa.GetLength(0); i++){
                for (int j = 0; j < Mapa.mapa.GetLength(1); j++){
                    if(Personagem.modelosPersonagem.Contains(Mapa.mapa[i,j])){
                        posicao[0] = i;
                        posicao[1] = j;
                    }
                }
            }
       
            switch (acao)
            {
                case 1:
                    if(Espada.quantidade > 0){
                        Espada.quantidade--;
                        Espada.nmrPuloAtaqueValido = 10;

                        if(Escudo.usandoDefesa){
                            Mapa.mapa[posicao[0],posicao[1]] = "{]";
                            Mapa.CheckMapaIsRenderizando();
                        }else{
                            Mapa.mapa[posicao[0],posicao[1]] = "{}";
                            Mapa.CheckMapaIsRenderizando();
                        }
                    }
                break;
                case 2:
                    if(Escudo.quantidade > 0){
                        Escudo.quantidade--;
                        Escudo.usandoDefesa = true;

                        if(Espada.nmrPuloAtaqueValido > 0){
                            Mapa.mapa[posicao[0],posicao[1]] = "{]";
                            Mapa.CheckMapaIsRenderizando();
                        }else{
                            Mapa.mapa[posicao[0],posicao[1]] = "[]";
                            Mapa.CheckMapaIsRenderizando();
                        }
                    }
                break;
                case 3:
                    if(Picareta.quantidade > 0){
                        if(Espada.nmrPuloAtaqueValido == 0 ){
                            Picareta.quantidade --;
                            Picareta.equipada = true;
                            
                            if(Escudo.usandoDefesa){
                                Mapa.mapa[posicao[0],posicao[1]] = "[T";
                                Mapa.CheckMapaIsRenderizando();
                            }else{
                                Mapa.mapa[posicao[0],posicao[1]] = "(T";
                                Mapa.CheckMapaIsRenderizando();
                            }
                        }else{
                            // MENSAGEM DE AVISO
                        }
                    }
                break;
            }
        }    
    }
}