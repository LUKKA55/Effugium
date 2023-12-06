namespace jogoInicial
{
    public class FaseStatus {
        public string[,] _mapa;
        public int _qntInimigosTipo1;
        public int _qntInimigosTipo2;
        public int _qntInimigosTipo3;
        public bool _temItemAtaque = true;
        public bool _temItemDefesa;
        public FaseStatus(
            string[,] mapa,
            bool temItemDefesa, 
            int qntInimigosTipo1 = 0, 
            int qntInimigosTipo2 = 0, 
            int qntInimigosTipo3 = 0
        ) {
            this._mapa = mapa;
            this._qntInimigosTipo1 = qntInimigosTipo1;
            this._qntInimigosTipo2 = qntInimigosTipo2;
            this._qntInimigosTipo3 = qntInimigosTipo3;
            this._temItemDefesa = temItemDefesa;
        }
    }
    
}