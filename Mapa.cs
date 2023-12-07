namespace jogoInicial
{
    public class Mapa
    {
        public static int renderizacoesPendentes = 0;
        public static string[,] mapa = DBFases.mapas[Game.nivelFase]._mapa;

        public static void MostrarMapa(){
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

        public static void CheckMapaIsRenderizando() {
            int qntRenderizacoesAtuais = renderizacoesPendentes;
            if (renderizacoesPendentes != 0) {
                while (!(renderizacoesPendentes < qntRenderizacoesAtuais));
            }
            renderizacoesPendentes++;
            if (Game.pararRenderizacoes) {
                renderizacoesPendentes = 0;
            } else {
                MostrarMapa();
                renderizacoesPendentes--;
            }
        }
    }
}