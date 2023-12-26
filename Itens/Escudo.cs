namespace Effugium
{
    public class Escudo : ModeloBaseItem {
        public bool _equipado = false;
        public Escudo(EnumItens tipoItem, List<List<int>> spawnsDisponiveis) : base(tipoItem, spawnsDisponiveis){}
    }
}