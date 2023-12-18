namespace jogoInicial
{
    public class FaseStatus {
        public string[,] _mapa;
        public string _modoJogo;
        public int _qntInimigosTipo1;
        public int _qntInimigosTipo2;
        public int _qntInimigosTipo3;
        public int _qntInimigosTipo4;
        public int _qntInimigosTipo5;
        //Inimigo Evolutivo
        public int _qntInimigosTipo6;
        public int _qntInimigosTipo7;
        public int _qntInimigosTipo8;
        public int _qntInimigosTipo9;
        public bool _espada = true;
        public bool _escudo;
        public bool _picareta;
        public bool _arco;
        public FaseStatus(
            string[,] mapa,
            bool escudo, 
            bool picareta,
            bool arco,
            string modoJogo,
            int qntInimigosTipo1 = 0, 
            int qntInimigosTipo2 = 0, 
            int qntInimigosTipo3 = 0,
            int qntInimigosTipo4 = 0,
            int qntInimigosTipo5 = 0,
            int qntInimigosTipo6 = 0,
            int qntInimigosTipo7 = 0,
            int qntInimigosTipo8 = 0,
            int qntInimigosTipo9 = 0
        ) {
            this._mapa = mapa;
            this._qntInimigosTipo1 = qntInimigosTipo1;
            this._qntInimigosTipo2 = qntInimigosTipo2;
            this._qntInimigosTipo3 = qntInimigosTipo3;
            this._qntInimigosTipo4 = qntInimigosTipo4;
            this._qntInimigosTipo5 = qntInimigosTipo5;
            this._qntInimigosTipo6 = qntInimigosTipo6;
            this._qntInimigosTipo7 = qntInimigosTipo7;
            this._qntInimigosTipo8 = qntInimigosTipo8;
            this._qntInimigosTipo9 = qntInimigosTipo9;
            this._escudo = escudo;
            this._picareta = picareta;
            this._arco = arco;
            this._modoJogo = modoJogo;
        }

        public FaseStatus CopiaFase(){
            return new FaseStatus(
                (string[,])_mapa.Clone(),
                this._escudo, 
                this._picareta,
                this._arco,
                this._modoJogo,
                this._qntInimigosTipo1,
                this._qntInimigosTipo2,
                this._qntInimigosTipo3,
                this._qntInimigosTipo4,
                this._qntInimigosTipo5,
                this._qntInimigosTipo6,
                this._qntInimigosTipo7,
                this._qntInimigosTipo8,
                this._qntInimigosTipo9
            );
        }
    }
    
}