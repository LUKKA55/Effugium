namespace Effugium
{
    public enum EnumItens {
        espada = 0,
        escudo = 1,
        picareta = 2,
        arco = 3
    } 
    public class Inventario {
        public Espada espada = new (EnumItens.espada, DB.todosTiposItens[(int)EnumItens.espada]._spawns);
        public Escudo escudo = new (EnumItens.escudo, DB.todosTiposItens[(int)EnumItens.escudo]._spawns);
        public Picareta picareta = new (EnumItens.picareta, DB.todosTiposItens[(int)EnumItens.picareta]._spawns);
        public Arco arco = new (EnumItens.arco, DB.todosTiposItens[(int)EnumItens.arco]._spawns);
        public void MostrarInventario(){
            if(espada.nmrPuloAtaqueValido > 0)
                Console.WriteLine("Ataque tem efeito pelos próximos {0} pulos", espada.nmrPuloAtaqueValido);
            
            if(escudo._equipado)
                Console.WriteLine("Está protegido de UM ataque");
            
            if(picareta._equipado)
                Console.WriteLine("Pode quebrar UMA parede do mapa (menos as bordas)");
            
            if(arco._equipado)
                Console.WriteLine("Aperte a tecla da direção que deseja atirar a flecha");
            
           
            Console.WriteLine("----------------------------------------");
            Console.WriteLine("|               Inventário             |");
            
            if (espada._quantidade > 0 )
                Console.WriteLine("|         [1]   {0}x Espada              |", espada._quantidade);
            
            if (escudo._quantidade > 0 )
                Console.WriteLine("|         [2]   {0}x Escudo              |", escudo._quantidade);
            
            if (picareta._quantidade > 0 )
                Console.WriteLine("|         [3]   {0}x Picareta            |", picareta._quantidade);
            
            if (arco._quantidade > 0 )
                Console.WriteLine("|         [4]   {0}x Arco                |", arco._quantidade);

            Console.WriteLine("----------------------------------------");
        }
        public void UsarItem(char numeroAcao){
            int acao = int.Parse(numeroAcao.ToString());
            int[] posicao = new int[2];

            for (int i = 0; i < Game.GetMapa().GetLength(0); i++){
                for (int j = 0; j < Game.GetMapa().GetLength(1); j++){
                    if(DB.modelosPersonagem.Contains(Game.GetMapa()[i,j])){
                        posicao[0] = i;
                        posicao[1] = j;
                    }
                }
            }
       
            switch (acao)
            {
                case 1:
                    if(espada._quantidade > 0){
                        if(!picareta._equipado && !arco._equipado){
                            espada._quantidade--;
                            espada.nmrPuloAtaqueValido = 10;

                            if(escudo._equipado){
                                Game.GetMapa()[posicao[0],posicao[1]] = "[}";
                            }else{
                                Game.GetMapa()[posicao[0],posicao[1]] = "{}";
                            }
                            Mapa.CheckMapaIsRenderizando();
                        }else{
                            // MENSAGEM DE AVISO
                        }
                    }
                break;
                case 2:
                    if(escudo._quantidade > 0){
                        escudo._quantidade--;
                        escudo._equipado = true;

                        Game.GetMapa()[posicao[0],posicao[1]] = 
                            espada.nmrPuloAtaqueValido > 0
                                ? "[}"
                                : "[]";
                                
                        Mapa.CheckMapaIsRenderizando();
                    }
                break;
                case 3:
                    if(picareta._quantidade > 0){
                        if(espada.nmrPuloAtaqueValido == 0 && !arco._equipado){
                            picareta._quantidade--;
                            picareta._equipado = true;
                            
                            if(escudo._equipado){
                                Game.GetMapa()[posicao[0],posicao[1]] = "[T";
                            }else{
                                Game.GetMapa()[posicao[0],posicao[1]] = "(T";
                            }
                            Mapa.CheckMapaIsRenderizando();
                        }else{
                            // MENSAGEM DE AVISO
                        }
                    }
                break;
                case 4:
                    if(arco._quantidade > 0){
                        if(espada.nmrPuloAtaqueValido == 0 && !picareta._equipado){
                            arco._quantidade--;
                            arco._equipado = true;
                            
                            if(escudo._equipado){
                                Game.GetMapa()[posicao[0],posicao[1]] = "[D";
                            }else{
                                Game.GetMapa()[posicao[0],posicao[1]] = "(D";
                            }
                            Mapa.CheckMapaIsRenderizando();
                        }else{
                            // MENSAGEM DE AVISO
                        }
                    }
                break;
            }
        }    
        public void AcrescentarItemInventario(EnumItens item) {
            int indexItem = (int)item;

            switch (indexItem)
            {
                case 0:
                    espada._quantidade++;
                    espada._itemNoMapa = false;
                    break;
                case 1:
                    escudo._quantidade++;
                    escudo._itemNoMapa = false;
                    break;
                case 2:
                    picareta._quantidade++;
                    picareta._itemNoMapa = false;
                    break;
                case 3:
                    arco._quantidade++;
                    arco._itemNoMapa = false;
                    break;
            }
        }
        public void ResetarInventario() {
            ModeloBaseItem[] itens = new ModeloBaseItem[]{ espada, escudo, picareta, arco};
            foreach(ModeloBaseItem item in itens) {
                item._itemNoMapa = false;
                item._quantidade = 0;
                item._spawnando = false;
            }

            espada.nmrPuloAtaqueValido = 0;
            escudo._equipado = false;
            picareta._equipado = false;
            arco._equipado = false;
        }

        public void SaveBackupInventario() {
            Inventario backup = new();
            ModeloBaseItem[] itensBackup = new ModeloBaseItem[]{ backup.espada, backup.escudo, backup.picareta, backup.arco};
            ModeloBaseItem[] itensAtuais = new ModeloBaseItem[]{ espada, escudo, picareta, arco};
            
            for(int i = 0; i < itensAtuais.GetLength(0); i++) {
                itensBackup[i]._quantidade = itensAtuais[i]._quantidade;
            };

            backup.espada.nmrPuloAtaqueValido = 0;
            backup.escudo._equipado = escudo._equipado;
            backup.picareta._equipado = picareta._equipado;
            backup.arco._equipado = arco._equipado;
            
            DB.backupInventario = backup;
        }

        public void LoadBackupInventario() {
            ModeloBaseItem[] itensBackup = new ModeloBaseItem[]{ 
                DB.backupInventario.espada, 
                DB.backupInventario.escudo, 
                DB.backupInventario.picareta, 
                DB.backupInventario.arco
            };
            ModeloBaseItem[] itensAtuais = new ModeloBaseItem[]{ espada, escudo, picareta, arco};
            
            for(int i = 0; i < itensAtuais.GetLength(0); i++) {
                itensAtuais[i]._quantidade = itensBackup[i]._quantidade;
            };

            espada.nmrPuloAtaqueValido = 0;
            escudo._equipado = DB.backupInventario.escudo._equipado;
            picareta._equipado = DB.backupInventario.picareta._equipado;
            arco._equipado = DB.backupInventario.arco._equipado;
        }
    }
}