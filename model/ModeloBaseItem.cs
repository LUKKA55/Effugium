namespace Effugium
{
    public abstract class ModeloBaseItem {
        public EnumItens _tipoItem;
        public List<List<int>> _spawnsDisponiveis;
        public bool _itemNoMapa = false;
        public bool _spawnando = false;

        public bool _spawnInstantaneo = false;
        public int _quantidade = 0;

        public ModeloBaseItem (
            EnumItens tipoItem,
            List<List<int>> spawnsDisponiveis
        ) {
            _tipoItem = tipoItem;
            _spawnsDisponiveis = spawnsDisponiveis;
        }

        public async Task VerificaSpawn(bool validacoesExtra = true){
            if (
                !_itemNoMapa && 
                !_spawnando &&
                _quantidade == 0 &&
                validacoesExtra
            ) {
                _spawnando = true;

                if(!_spawnInstantaneo) {
                    await Task.Delay(10000);
                } else {
                    _spawnInstantaneo = false;
                }
                List<List<int>> spawnsDisponiveis = _spawnsDisponiveis;

                int tentativasDeSpawn = spawnsDisponiveis.Count;

                for (int c = 0; c < tentativasDeSpawn; c++) {
                    int randomIndex = new Random().Next(0, spawnsDisponiveis.Count);

                    if(Game.GetMapa()[
                        spawnsDisponiveis.ElementAt(randomIndex).ElementAt(0), 
                        spawnsDisponiveis.ElementAt(randomIndex).ElementAt(1)
                    ] == "  "){
                        Game.GetMapa()[
                            spawnsDisponiveis.ElementAt(randomIndex).ElementAt(0), 
                            spawnsDisponiveis.ElementAt(randomIndex).ElementAt(1)
                        ] = DB.todosTiposItens[(int)_tipoItem]._modelo;
                        _itemNoMapa = true;
                        break;

                    }else if(DB.modelosPersonagem.Contains(
                        Game.GetMapa()[
                            spawnsDisponiveis.ElementAt(randomIndex).ElementAt(0),
                            spawnsDisponiveis.ElementAt(randomIndex).ElementAt(1)
                        ]
                    )){
                        Personagem.inventario.AcrescentarItemInventario(_tipoItem);
                        break;
                    } else {
                        spawnsDisponiveis.RemoveAt(randomIndex);
                    }
                }
                
                _spawnando = false;
                Mapa.CheckMapaIsRenderizando();
            }
        }
    }
}