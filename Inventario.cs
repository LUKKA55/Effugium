namespace jogoInicial
{
    public class Inventario
    {
        static public List<string> todosTiposItens= new(){"<-", "<>", "T ", "D-"}; 
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
            if(Arco.equipado){
                Console.WriteLine("Aperte a tecla da direção que deseja atirar a flecha");
            }
            if (Espada.quantidade > 0 || Escudo.quantidade > 0 || Picareta.quantidade > 0 || Arco.quantidade > 0){
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
            if (Arco.quantidade > 0 ){
                Console.WriteLine(" -Arco para atirar flecha {0}", Arco.quantidade);
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
                        if(!Picareta.equipada && !Arco.equipado){
                            Espada.quantidade--;
                            Espada.nmrPuloAtaqueValido = 10;

                            if(Escudo.usandoDefesa){
                                Mapa.mapa[posicao[0],posicao[1]] = "[}";
                            }else{
                                Mapa.mapa[posicao[0],posicao[1]] = "{}";
                            }
                            Mapa.CheckMapaIsRenderizando();
                        }else{
                            // MENSAGEM DE AVISO
                        }
                    }
                break;
                case 2:
                    if(Escudo.quantidade > 0){
                        Escudo.quantidade--;
                        Escudo.usandoDefesa = true;

                        if(Espada.nmrPuloAtaqueValido > 0){
                            Mapa.mapa[posicao[0],posicao[1]] = "[}";
                        }else{
                            Mapa.mapa[posicao[0],posicao[1]] = "[]";
                        }
                        Mapa.CheckMapaIsRenderizando();
                    }
                break;
                case 3:
                    if(Picareta.quantidade > 0){
                        if(Espada.nmrPuloAtaqueValido == 0 && !Arco.equipado){
                            Picareta.quantidade --;
                            Picareta.equipada = true;
                            
                            if(Escudo.usandoDefesa){
                                Mapa.mapa[posicao[0],posicao[1]] = "[T";
                            }else{
                                Mapa.mapa[posicao[0],posicao[1]] = "(T";
                            }
                            Mapa.CheckMapaIsRenderizando();
                        }else{
                            // MENSAGEM DE AVISO
                        }
                    }
                break;
                case 4:
                    if(Arco.quantidade > 0){
                        if(Espada.nmrPuloAtaqueValido == 0 && !Picareta.equipada){
                            Arco.quantidade --;
                            Arco.equipado = true;
                            
                            if(Escudo.usandoDefesa){
                                Mapa.mapa[posicao[0],posicao[1]] = "[D";
                            }else{
                                Mapa.mapa[posicao[0],posicao[1]] = "(D";
                            }
                            Mapa.CheckMapaIsRenderizando();
                        }else{
                            // MENSAGEM DE AVISO
                        }
                    }
                break;
            }
        }    
    }
}