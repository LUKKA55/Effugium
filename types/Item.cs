class Item {
    public string _modelo;
    public  List<List<int>> _spawns;

    public Item(
        string modelo,
        List<List<int>> spawns
    ) {
        _modelo = modelo;
        _spawns = spawns;
    }
}