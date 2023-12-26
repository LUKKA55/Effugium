namespace Effugium
{
    public class Picareta : ModeloBaseItem {
        public bool _equipado = false;
        public Picareta(EnumItens tipoItem, List<List<int>> spawnsDisponiveis) : base(tipoItem, spawnsDisponiveis){}
    }
}