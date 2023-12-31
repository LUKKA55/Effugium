namespace Effugium
{
    public class Mapa {
        public static bool estaRenderizando = false;
        public static void RenderizarMapa(){
           if(DB.fases[Game.nivelFase]._modoJogo != "ESCURO"){
                MostrarMapaNormal();
           
           }else{
                MostraMapaEscuro();
           }
        }
        public static void CheckMapaIsRenderizando() {
            if (estaRenderizando || Game.pararRenderizacoes || Game.telaInfoAberta) {
                return;
            }
            estaRenderizando = true;
            RenderizarMapa();
            estaRenderizando = false;
            VerificaSpawnItens();
        }
        public static void MostrarMapaNormal(){
            Console.Clear(); 
            for (int i = 0; i < Game.GetMapa().GetLength(0); i++){
                for (int j = 0; j < Game.GetMapa().GetLength(1); j++){
                    if(Game.pararRenderizacoes || Game.telaInfoAberta)return;
                    
                    Console.Write(Game.GetMapa()[i,j]);
                }
                Console.WriteLine();
            }
            Personagem.inventario.MostrarInventario();

            // MENSAGEM DE AVISO
        }
        public static void MostraMapaEscuro() {
            List<int> posicaoUsuario = Personagem.posicaoPersonagem;
            List<List<int>> posicoesMapaRevelado = new (){
                new() {posicaoUsuario[0], posicaoUsuario[1]},
                new() {posicaoUsuario[0]+1, posicaoUsuario[1]}, 
                new() {posicaoUsuario[0]-1, posicaoUsuario[1]}, 
                new() {posicaoUsuario[0], posicaoUsuario[1]+1}, 
                new() {posicaoUsuario[0], posicaoUsuario[1]-1}, 
                new() {posicaoUsuario[0]-1, posicaoUsuario[1]-1},  
                new() {posicaoUsuario[0]-1, posicaoUsuario[1]+1},  
                new() {posicaoUsuario[0]+1, posicaoUsuario[1]-1},  
                new() {posicaoUsuario[0]+1, posicaoUsuario[1]+1},  
                new() {posicaoUsuario[0]-2, posicaoUsuario[1]-2},  
                new() {posicaoUsuario[0]-2, posicaoUsuario[1]-1},  
                new() {posicaoUsuario[0]-2, posicaoUsuario[1]},  
                new() {posicaoUsuario[0]-2, posicaoUsuario[1]+1},  
                new() {posicaoUsuario[0]-2, posicaoUsuario[1]+2},  
                new() {posicaoUsuario[0]-1, posicaoUsuario[1]-2},  
                new() {posicaoUsuario[0]-1, posicaoUsuario[1]+2},  
                new() {posicaoUsuario[0], posicaoUsuario[1]-2},  
                new() {posicaoUsuario[0], posicaoUsuario[1]+2},   
                new() {posicaoUsuario[0]+1, posicaoUsuario[1]-2},    
                new() {posicaoUsuario[0]+1, posicaoUsuario[1]+2},    
                new() {posicaoUsuario[0]+2, posicaoUsuario[1]-2}, 
                new() {posicaoUsuario[0]+2, posicaoUsuario[1]-1},   
                new() {posicaoUsuario[0]+2, posicaoUsuario[1]},  
                new() {posicaoUsuario[0]+2, posicaoUsuario[1]+1},   
                new() {posicaoUsuario[0]+2, posicaoUsuario[1]+2},  
            };
            Console.Clear(); 
            for (int i = 0; i < Game.GetMapa().GetLength(0); i++){
                for (int j = 0; j < Game.GetMapa().GetLength(1); j++){
                    if(Game.pararRenderizacoes || Game.telaInfoAberta)return;

                    bool posicaoEhRevelada = posicoesMapaRevelado.Exists((pmr) => 
                        pmr.ElementAt(0) == i && pmr.ElementAt(1) == j);

                    if (posicaoEhRevelada) {
                        Console.Write(Game.GetMapa()[i,j]);
                    } else {
                        Console.Write("  ");
                    }
                }
                Console.WriteLine();
            }
            // MENSAGEM DE AVISO
            Personagem.inventario.MostrarInventario();
        }
        public static void ResetaSpawnsItensDoMapa() {
            if(DB.fases.ElementAt(Game.nivelFase)._arco)
                Personagem.inventario.arco._itemNoMapa = true;

            if(DB.fases.ElementAt(Game.nivelFase)._picareta)
                Personagem.inventario.picareta._itemNoMapa = true;

            if(DB.fases.ElementAt(Game.nivelFase)._escudo)
                Personagem.inventario.escudo._itemNoMapa = true;

            Personagem.inventario.espada._itemNoMapa = true;
        }

        public static void VerificaSpawnItens() {
            Personagem.inventario.arco.VerificaSpawn(
                !Personagem.inventario.arco._equipado && 
                Game.FaseAtual._arco
            );

            Personagem.inventario.espada.VerificaSpawn(
                Personagem.inventario.espada.nmrPuloAtaqueValido <= 0
            );

            Personagem.inventario.escudo.VerificaSpawn(
                !Personagem.inventario.escudo._equipado &&
                Game.FaseAtual._escudo
            );

            Personagem.inventario.picareta.VerificaSpawn(
                !Personagem.inventario.picareta._equipado &&
                Game.FaseAtual._picareta
            );
        }
    }
}