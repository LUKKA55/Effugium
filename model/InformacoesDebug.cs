namespace Effugium
{
    class InformacoesDebug {
        public Inventario inventario = new();
        public FaseStatus faseAtual = new FaseStatus(
            new string[13,20],
            Game.FaseAtual._escudo,
            Game.FaseAtual._picareta,
            Game.FaseAtual._arco,
            Game.FaseAtual._modoJogo,
            Game.FaseAtual._qntInimigosTipo1, 
            Game.FaseAtual._qntInimigosTipo2, 
            Game.FaseAtual._qntInimigosTipo3,
            Game.FaseAtual._qntInimigosTipo4,
            Game.FaseAtual._qntInimigosTipo5,
            Game.FaseAtual._qntInimigosTipo6,
            Game.FaseAtual._qntInimigosTipo7,
            Game.FaseAtual._qntInimigosTipo8,
            Game.FaseAtual._qntInimigosTipo9
        );
        public bool telaGameOver = Game.telaGameOver;
        public InformacoesDebug() {
            ModeloBaseItem[] itensPersonagem = new ModeloBaseItem[]{
               Personagem.inventario.espada,
               Personagem.inventario.arco,
               Personagem.inventario.picareta,
               Personagem.inventario.escudo,
            };

            ModeloBaseItem[] itensDebug = new ModeloBaseItem[]{
               inventario.espada,
               inventario.arco,
               inventario.picareta,
               inventario.escudo,
            };

            for (int idx = 0; idx < itensPersonagem.Length; idx++) {
                itensDebug[idx]._itemNoMapa = itensPersonagem[idx]._itemNoMapa;
                itensDebug[idx]._quantidade = itensPersonagem[idx]._quantidade;
                itensDebug[idx]._spawnando = itensPersonagem[idx]._spawnando;
                itensDebug[idx]._spawnInstantaneo = itensPersonagem[idx]._spawnInstantaneo;
                itensDebug[idx]._spawnsDisponiveis = new (itensPersonagem[idx]._spawnsDisponiveis);
                itensDebug[idx]._tipoItem = itensPersonagem[idx]._tipoItem;
            }

            inventario.espada.nmrPuloAtaqueValido = Personagem.inventario.espada.nmrPuloAtaqueValido;
            inventario.escudo._equipado = Personagem.inventario.escudo._equipado;
            inventario.picareta._equipado = Personagem.inventario.picareta._equipado;
            inventario.arco._equipado = Personagem.inventario.arco._equipado;

            Array.Copy(Game.FaseAtual._mapa, faseAtual._mapa, Game.FaseAtual._mapa.Length);
        }

        public void MostrarDebugLog() {
            Console.Clear();
            Console.WriteLine($"telaGameOver: {telaGameOver}");
            Console.WriteLine("Mapa Atual:");
            Console.WriteLine($"_espada: {Game.FaseAtual._espada}");
            Console.WriteLine($"_escudo: {Game.FaseAtual._escudo}");
            Console.WriteLine($"_picareta: {Game.FaseAtual._picareta}");
            Console.WriteLine($"_arco: {Game.FaseAtual._arco}");
            Console.WriteLine($"_modoJogo: {Game.FaseAtual._modoJogo}");
            Console.WriteLine($"qntInimigosTipo1: {Game.FaseAtual._qntInimigosTipo1}"); 
            Console.WriteLine($"qntInimigosTipo2: {Game.FaseAtual._qntInimigosTipo2}"); 
            Console.WriteLine($"qntInimigosTipo3: {Game.FaseAtual._qntInimigosTipo3}");
            Console.WriteLine($"qntInimigosTipo4: {Game.FaseAtual._qntInimigosTipo4}");
            Console.WriteLine($"qntInimigosTipo5: {Game.FaseAtual._qntInimigosTipo5}");
            Console.WriteLine($"qntInimigosTipo6: {Game.FaseAtual._qntInimigosTipo6}");
            Console.WriteLine($"qntInimigosTipo7: {Game.FaseAtual._qntInimigosTipo7}");
            Console.WriteLine($"qntInimigosTipo8: {Game.FaseAtual._qntInimigosTipo8}");
            Console.WriteLine($"qntInimigosTipo9: {Game.FaseAtual._qntInimigosTipo9}");
            Console.WriteLine("Inventario:");
            ModeloBaseItem[] itensDebug = new ModeloBaseItem[]{
               inventario.espada,
               inventario.arco,
               inventario.picareta,
               inventario.escudo,
            };

            for (int idx = 0; idx < itensDebug.Length; idx++) {
                Console.WriteLine();
                Console.WriteLine($"_tipoItem: {itensDebug[idx]._tipoItem}");
                switch (itensDebug[idx]._tipoItem) {
                    case EnumItens.espada:
                        Console.WriteLine($"_nmrPuloAtaqueValido: {inventario.espada.nmrPuloAtaqueValido}");
                        break;
                    case EnumItens.escudo:
                        Console.WriteLine($"_equipado: {inventario.escudo._equipado}");
                        break; 
                    case EnumItens.picareta:
                        Console.WriteLine($"_equipado: {inventario.picareta._equipado}");
                        break;       
                    case EnumItens.arco:
                        Console.WriteLine($"_equipado: {inventario.arco._equipado}");
                        break;                                                                 
                }
                Console.WriteLine($"_itemNoMapa: {itensDebug[idx]._itemNoMapa}");
                Console.WriteLine($"_quantidade: {itensDebug[idx]._quantidade}");
                Console.WriteLine($"_spawnando: {itensDebug[idx]._spawnando}");
                Console.WriteLine($"_spawnInstantaneo: {itensDebug[idx]._spawnInstantaneo}");
                string spawnsDisponiveisDisplay = "";
                for(
                    int idxSpawn = 0; 
                    idxSpawn < itensDebug[idx]._spawnsDisponiveis.Count;
                    idxSpawn++
                ) {
                   spawnsDisponiveisDisplay += $"{itensDebug[idx]._spawnsDisponiveis[idxSpawn][0]} : {itensDebug[idx]._spawnsDisponiveis[idxSpawn][1]}";
                    if (idxSpawn < itensDebug[idx]._spawnsDisponiveis.Count - 1)
                        spawnsDisponiveisDisplay += " || ";    
                }
                Console.WriteLine($"_spawnsDisponiveis: {spawnsDisponiveisDisplay}");
            }
        }
    }
}