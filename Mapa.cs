using System;
namespace jogoInicial
{
    public class Mapa
    {
        public static int renderizacoesPendentes = 0;
        public static string[,] mapa = DBFases.mapas[Game.nivelFase]._mapa;

        public static void RenderizarMapa(){
           if(DBFases.mapas[Game.nivelFase]._modoJogo != "ESCURO"){
                MostrarMapaNormal();
           
           }else{
                MostraMapaEscuro();
           }
        }

        public static void CheckMapaIsRenderizando() {
            int qntRenderizacoesAtuais = renderizacoesPendentes;
            if (renderizacoesPendentes != 0) {
                while (!(renderizacoesPendentes < qntRenderizacoesAtuais)) {
                };
            }
            renderizacoesPendentes++;
            if (Game.pararRenderizacoes) {
                renderizacoesPendentes = 0;
            } else {
                RenderizarMapa();
                renderizacoesPendentes--;
            }
        }

        public static void MostrarMapaNormal(){
            Console.Clear(); 
            for (int i = 0; i < mapa.GetLength(0); i++){
                for (int j = 0; j < mapa.GetLength(1); j++){
                    Console.Write(mapa[i,j]);
                }
                Console.WriteLine();
            }
            Inventario.MostrarInventario();

            // MENSAGEM DE AVISO
        }
        
        public static void MostraMapaEscuro() {
            int[] posicaoUsuario = Personagem.posicaoPersonagem;
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
            for (int i = 0; i < mapa.GetLength(0); i++){
                for (int j = 0; j < mapa.GetLength(1); j++){
                    bool posicaoEhRevelada = posicoesMapaRevelado.Exists((pmr) => 
                        pmr.ElementAt(0) == i && pmr.ElementAt(1) == j);

                    if (posicaoEhRevelada) {
                        Console.Write(mapa[i,j]);
                    } else {
                        Console.Write("  ");
                    }
                }
                Console.WriteLine();
            }
            // MENSAGEM DE AVISO
            Inventario.MostrarInventario();
        }
    }
}