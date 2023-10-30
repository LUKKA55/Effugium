namespace jogoInicial
{
    public class Personagem
    {
        public static void Movimentacao(string[,] mapaDefault,string direcao){
            int[] posicao = new int[2];
            for (int i = 0; i < mapaDefault.GetLength(0); i++){
                for (int j = 0; j < mapaDefault.GetLength(1); j++){
                    if(mapaDefault[i,j] == "()" || mapaDefault[i,j] == "{}" || mapaDefault[i,j] == "[]" || mapaDefault[i,j] == "{]"){
                        posicao[0] = i;
                        posicao[1] = j;
                    }
                }
            }
            static string limpaLugarAntigo(int[] posicao, string[,] mapaDefault) => mapaDefault[posicao[0],posicao[1]] = "  ";
            switch (direcao)
            {
                case "W":
                    if(mapaDefault[posicao[0]-1,posicao[1]] == "  "){
                        limpaLugarAntigo(posicao, mapaDefault);
                        if(ItemAtaque.nmrPuloAtaqueValido > 0 && ItemDefesa.usandoDefesa){
                            mapaDefault[posicao[0]-1,posicao[1]] = "{]";
                            ItemAtaque.nmrPuloAtaqueValido--;

                        }else if(ItemAtaque.nmrPuloAtaqueValido > 0){
                            mapaDefault[posicao[0]-1,posicao[1]] = "{}";
                            ItemAtaque.nmrPuloAtaqueValido--;

                        }else if (ItemDefesa.usandoDefesa){
                            mapaDefault[posicao[0]-1,posicao[1]] = "[]";
                            
                        }else{
                            mapaDefault[posicao[0]-1,posicao[1]] = "()";
                        }
                    }else if(mapaDefault[posicao[0]-1,posicao[1]] == "<-"){
                        limpaLugarAntigo(posicao, mapaDefault);
                        if (ItemDefesa.usandoDefesa){
                            mapaDefault[posicao[0]-1,posicao[1]] = "[]";
                            
                        }else{
                            mapaDefault[posicao[0]-1,posicao[1]] = "()";
                        }
                        ItemAtaque.quantidade++;

                    }else if(mapaDefault[posicao[0]-1,posicao[1]] == "<>"){
                        limpaLugarAntigo(posicao, mapaDefault);
                        if(ItemAtaque.nmrPuloAtaqueValido > 0){
                            mapaDefault[posicao[0]-1,posicao[1]] = "{}";
                            ItemAtaque.nmrPuloAtaqueValido--;

                        }else{
                            mapaDefault[posicao[0]-1,posicao[1]] = "()";
                        }
                        ItemDefesa.quantidade++;

                    }else if(ItemAtaque.nmrPuloAtaqueValido > 0 && mapaDefault[posicao[0]-1,posicao[1]] == "XX"){
                        limpaLugarAntigo(posicao, mapaDefault);
                        ItemAtaque.nmrPuloAtaqueValido = 0;
                        if(ItemDefesa.usandoDefesa){
                            mapaDefault[posicao[0]-1,posicao[1]] = "[]";
                            
                        }else{
                            mapaDefault[posicao[0]-1,posicao[1]] = "()";
                        }
                    }
                break;
                case "A":
                    if(mapaDefault[posicao[0],posicao[1]-1] == "  "){ 
                        limpaLugarAntigo(posicao, mapaDefault);
                        if(ItemAtaque.nmrPuloAtaqueValido > 0 && ItemDefesa.usandoDefesa){
                            mapaDefault[posicao[0],posicao[1]-1] = "{]";
                            ItemAtaque.nmrPuloAtaqueValido--;

                        }else if(ItemAtaque.nmrPuloAtaqueValido > 0){
                            mapaDefault[posicao[0],posicao[1]-1] = "{}";
                            ItemAtaque.nmrPuloAtaqueValido--;

                        }else if(ItemDefesa.usandoDefesa){
                            mapaDefault[posicao[0],posicao[1]-1] = "[]";

                        }else{
                            mapaDefault[posicao[0],posicao[1]-1] = "()";
                        }
                    }else if(mapaDefault[posicao[0],posicao[1]-1] == "<-"){ 
                        limpaLugarAntigo(posicao, mapaDefault);
                        if(ItemDefesa.usandoDefesa){
                            mapaDefault[posicao[0],posicao[1]-1] = "[]";

                        }else{
                            mapaDefault[posicao[0],posicao[1]-1] = "()";
                        }
                        ItemAtaque.quantidade++;

                    }else if(mapaDefault[posicao[0],posicao[1]-1] == "<>"){ 
                        limpaLugarAntigo(posicao, mapaDefault);
                        if(ItemAtaque.nmrPuloAtaqueValido > 0){
                            mapaDefault[posicao[0],posicao[1]-1] = "{}";
                            ItemAtaque.nmrPuloAtaqueValido--;

                        }else{
                            mapaDefault[posicao[0],posicao[1]-1] = "()";
                        }
                        ItemDefesa.quantidade++;

                    }else if(ItemAtaque.nmrPuloAtaqueValido > 0 && mapaDefault[posicao[0],posicao[1]-1] == "XX"){
                        limpaLugarAntigo(posicao, mapaDefault);
                        ItemAtaque.nmrPuloAtaqueValido = 0;
                        if(ItemDefesa.usandoDefesa){
                            mapaDefault[posicao[0],posicao[1]-1] = "[]";

                        }else{
                            mapaDefault[posicao[0],posicao[1]-1] = "()";
                        }
                    }
                break;
                case "S":
                    if(mapaDefault[posicao[0]+1,posicao[1]] == "  "){
                        limpaLugarAntigo(posicao, mapaDefault);
                        if(ItemAtaque.nmrPuloAtaqueValido > 0  && ItemDefesa.usandoDefesa){
                            mapaDefault[posicao[0]+1,posicao[1]] = "{]";
                            ItemAtaque.nmrPuloAtaqueValido--;

                        }else if(ItemAtaque.nmrPuloAtaqueValido > 0){
                            mapaDefault[posicao[0]+1,posicao[1]] = "{}";
                            ItemAtaque.nmrPuloAtaqueValido--;

                        }else if(ItemDefesa.usandoDefesa){
                            mapaDefault[posicao[0]+1,posicao[1]] = "[]";

                        }else{
                            mapaDefault[posicao[0]+1,posicao[1]] = "()";
                        }
                    }else if(mapaDefault[posicao[0]+1,posicao[1]] == "<-"){
                        limpaLugarAntigo(posicao, mapaDefault);
                        if(ItemDefesa.usandoDefesa){
                            mapaDefault[posicao[0]+1,posicao[1]] = "[]";

                        }else{
                            mapaDefault[posicao[0]+1,posicao[1]] = "()";
                        }
                        ItemAtaque.quantidade++;
                    
                    }else if(mapaDefault[posicao[0]+1,posicao[1]] == "<>"){
                        limpaLugarAntigo(posicao, mapaDefault);
                          if(ItemAtaque.nmrPuloAtaqueValido > 0){
                            mapaDefault[posicao[0]+1,posicao[1]] = "{}";
                            ItemAtaque.nmrPuloAtaqueValido--;

                        }else{
                            mapaDefault[posicao[0]+1,posicao[1]] = "()";
                        }
                        ItemDefesa.quantidade++;
                    
                    }else if(ItemAtaque.nmrPuloAtaqueValido > 0 && mapaDefault[posicao[0]+1,posicao[1]] == "XX"){
                        limpaLugarAntigo(posicao, mapaDefault);
                        ItemAtaque.nmrPuloAtaqueValido = 0;
                        if(ItemDefesa.usandoDefesa){
                            mapaDefault[posicao[0]+1,posicao[1]] = "[]";

                        }else{
                            mapaDefault[posicao[0]+1,posicao[1]] = "()";
                        }
                    }
                break;
                case "D":
                    if(mapaDefault[posicao[0],posicao[1]+1] == "  "){  
                        limpaLugarAntigo(posicao, mapaDefault);
                        if(ItemAtaque.nmrPuloAtaqueValido > 0 && ItemDefesa.usandoDefesa){
                            mapaDefault[posicao[0],posicao[1]+1] = "{]";
                            ItemAtaque.nmrPuloAtaqueValido--;

                        }else if(ItemAtaque.nmrPuloAtaqueValido > 0){
                            mapaDefault[posicao[0],posicao[1]+1] = "{}";
                            ItemAtaque.nmrPuloAtaqueValido--;

                        }else if(ItemDefesa.usandoDefesa){
                            mapaDefault[posicao[0],posicao[1]+1] = "[]";

                        }else{
                            mapaDefault[posicao[0],posicao[1]+1] = "()";
                        }
                    }else if(mapaDefault[posicao[0],posicao[1]+1] == "<-"){  
                        limpaLugarAntigo(posicao, mapaDefault);
                        if(ItemDefesa.usandoDefesa){
                            mapaDefault[posicao[0],posicao[1]+1] = "[]";

                        }else{
                            mapaDefault[posicao[0],posicao[1]+1] = "()";
                        }
                        ItemAtaque.quantidade++;

                    }else if(mapaDefault[posicao[0],posicao[1]+1] == "<>"){  
                        limpaLugarAntigo(posicao, mapaDefault);
                        if(ItemAtaque.nmrPuloAtaqueValido > 0){
                            mapaDefault[posicao[0],posicao[1]+1] = "{}";
                            ItemAtaque.nmrPuloAtaqueValido--;

                        }else{
                            mapaDefault[posicao[0],posicao[1]+1] = "()";
                        }
                        ItemDefesa.quantidade++;

                    }else if(ItemAtaque.nmrPuloAtaqueValido > 0 && mapaDefault[posicao[0],posicao[1]+1] == "XX"){
                        limpaLugarAntigo(posicao, mapaDefault);
                        ItemAtaque.nmrPuloAtaqueValido = 0;
                        if(ItemDefesa.usandoDefesa){
                            mapaDefault[posicao[0],posicao[1]+1] = "[]";

                        }else{
                            mapaDefault[posicao[0],posicao[1]+1] = "()";
                        }
                    }
                break;
            }
            Mapa.CheckMapaIsRenderizando();
        }
    }
}