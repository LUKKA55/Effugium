namespace Effugium
{
    public class Espada : ModeloBaseItem {
        public int nmrPuloAtaqueValido = 0;
        public Espada(EnumItens tipoItem, List<List<int>> spawnsDisponiveis) : base(tipoItem, spawnsDisponiveis){}
    }
}