namespace jogoInicial
{
    public enum EnumInimigos {
        tipo1,
        tipo2,
        tipo3,
        tipo4,
        tipo5,
        //Inimigo Evolutivo
        tipo6,
        tipo7,
        tipo8,
        tipo9
    }
    public class Inimigos {
        // Variacao da movimentação do inimigo
        static public bool turnoInimigo4Atira = true;
        static public List<List<int>> altPosicaoNormal = new List<List<int>>{
            new List<int>{-1, 0}, // cima
            new List<int>{1, 0}, // baixo
            new List<int>{0, 1}, // direita
            new List<int>{0, -1}, // esquerda
        };
        static public List<List<int>> altPosicaoComDiagonal = new List<List<int>>{
            new List<int>{-1, 0}, // cima
            new List<int>{1, 0}, // baixo
            new List<int>{0, 1}, // direita
            new List<int>{0, -1}, // esquerda
            new List<int>{-1, -1}, // cima - esquerda
            new List<int>{-1, 1}, // cima - direita
            new List<int>{1, -1}, // baixo - esquerda
            new List<int>{1, 1}, // baixo - direita
        };
        static string LimpaLugarAntigoInimigo(int[,] posicaoInimigo, int i) => 
                Game.GetMapa()[posicaoInimigo[i, 0], posicaoInimigo[i, 1]] = "  ";
        public static void MovimentacaoInimigo (string tipoInimigo){
            if(Game.telaInfoAberta) return;
            int[,] posicoesInimigos = new int[Game.GetMapa().GetLength(0) * Game.GetMapa().GetLength(1), 2];

            int qntInimigos = 0;

            for (int i = 0; i < Game.GetMapa().GetLength(0); i++){
                for (int j = 0; j < Game.GetMapa().GetLength(1); j++){
                    if(tipoInimigo == Game.GetMapa()[i,j]){
                        posicoesInimigos[qntInimigos, 0] = i;
                        posicoesInimigos[qntInimigos, 1] = j;
                        qntInimigos++;
                    }
                }
            }

            if (tipoInimigo == "(>") 
                turnoInimigo4Atira = !turnoInimigo4Atira;

            for(int i = 0; i < qntInimigos; i++){
                enumDirecao direcaoProjetil = enumDirecao.Direita;

                bool inimigoTemVisaoJogador = VerificaInimigoTemVisaoJogador(new int[2]{ posicoesInimigos[i, 0], posicoesInimigos[i, 1] }, ref direcaoProjetil);
                // se for true && turnoInimigo4Atira for true tbm, não se movimenta, somente atira na direcao
                if (inimigoTemVisaoJogador && turnoInimigo4Atira && tipoInimigo == "(>") {
                    AtirarProjetil(direcaoProjetil, new List<int>{ posicoesInimigos[i, 0], posicoesInimigos[i, 1] });
                    continue;
                }

                var numeroAleatorio = new Random();
                List<List<int>> cloneAltPosicao = new List<List<int>>(
                    tipoInimigo == ";;" || DB.inimigosEvolutivos.FindIndex(e => e == tipoInimigo) > 0
                    ? altPosicaoComDiagonal
                    : altPosicaoNormal
                );
                int qntTentativas = cloneAltPosicao.Count;
                
                bool inimigoPodeEvoluir = 
                    DB.inimigosEvolutivos.FindIndex(i => i == tipoInimigo) > -1 
                    && DB.inimigosEvolutivos.FindIndex(i => i == tipoInimigo) < 3;

                //Inimigo Evolutivo tenta caçar um "XX"
                if(inimigoPodeEvoluir){
                    bool cacouInimigo = false;
                    for(int c = 0; c < qntTentativas; c++) {
                        int idxAleatorio = numeroAleatorio.Next(0, cloneAltPosicao.Count);
                        string destino = Game.GetMapa()[
                            cloneAltPosicao.ElementAt(idxAleatorio).ElementAt(0) + posicoesInimigos[i, 0],
                            cloneAltPosicao.ElementAt(idxAleatorio).ElementAt(1) + posicoesInimigos[i, 1]
                        ];

                        if(destino == "XX"){
                            LimpaLugarAntigoInimigo(posicoesInimigos, i);
                            string versaoEvoluida = "§§";

                            switch(tipoInimigo) {
                                case "çç":
                                    versaoEvoluida = "¢¢";
                                    bool jaExistemInimigos7 = Game.FaseAtual._qntInimigosTipo7 > 0;
                                    Game.FaseAtual._qntInimigosTipo7++;
                                    Game.FaseAtual._qntInimigosTipo6--;
                                    if(!jaExistemInimigos7) {
                                        Inimigos.IntervaloMovimentoInimigo7();
                                    }
                                break;
                                case "¢¢":
                                    versaoEvoluida = "$$";
                                    bool jaExistemInimigos8 = Game.FaseAtual._qntInimigosTipo8 > 0;
                                    Game.FaseAtual._qntInimigosTipo8++;
                                    Game.FaseAtual._qntInimigosTipo7--;
                                    if(!jaExistemInimigos8) {
                                        Inimigos.IntervaloMovimentoInimigo8();
                                    }
                                break;
                                case "$$":
                                    versaoEvoluida = "§§";
                                    bool jaExistemInimigos9 = Game.FaseAtual._qntInimigosTipo9 > 0;
                                    Game.FaseAtual._qntInimigosTipo9++;
                                    Game.FaseAtual._qntInimigosTipo8--;
                                    if(!jaExistemInimigos9) {
                                        Inimigos.IntervaloMovimentoInimigo9();
                                    }
                                break;
                            }

                            Game.FaseAtual._qntInimigosTipo1--;
                            Game.GetMapa()[
                                cloneAltPosicao.ElementAt(idxAleatorio).ElementAt(0) + posicoesInimigos[i, 0],
                                cloneAltPosicao.ElementAt(idxAleatorio).ElementAt(1) + posicoesInimigos[i, 1]
                            ] = versaoEvoluida;

                            cacouInimigo = true;
                            break;
                        }
                    }
                    
                    if (cacouInimigo)
                        continue;
                }
                
                for(int c = 0; c < qntTentativas; c++) {
                    int idxAleatorio = numeroAleatorio.Next(0, cloneAltPosicao.Count);
                    string destino = Game.GetMapa()[
                        cloneAltPosicao.ElementAt(idxAleatorio).ElementAt(0) + posicoesInimigos[i, 0],
                        cloneAltPosicao.ElementAt(idxAleatorio).ElementAt(1) + posicoesInimigos[i, 1]
                    ];

                    void acaoInimigo(bool destroiDefesaUsuario = false) => 
                        Game.GetMapa()[ 
                            cloneAltPosicao.ElementAt(idxAleatorio).ElementAt(0) + posicoesInimigos[i, 0],
                            cloneAltPosicao.ElementAt(idxAleatorio).ElementAt(1) + posicoesInimigos[i, 1]
                        ] = destroiDefesaUsuario 
                            ? 
                            Personagem.GetPersonagemEquipado() 
                            : 
                            tipoInimigo;
                    
                    if(
                        destino == "  " || 
                        DB.todosTiposItens.FindIndex(i => i._modelo == destino) >= 0
                    ){
                        if (tipoInimigo != "@@") {
                            LimpaLugarAntigoInimigo(posicoesInimigos, i);
                        } else {
                            Game.FaseAtual._qntInimigosTipo5++;
                        }

                        EnumItens tipoItem = (EnumItens)DB.todosTiposItens.FindIndex(i => i._modelo == destino);
                        switch(tipoItem) {
                            case EnumItens.espada:
                                Personagem.inventario.espada._itemNoMapa = false;
                            break;
                            case EnumItens.escudo:
                                Personagem.inventario.escudo._itemNoMapa = false;
                            break;
                            case EnumItens.picareta:
                                Personagem.inventario.picareta._itemNoMapa = false;
                            break;   
                            case EnumItens.arco:
                                Personagem.inventario.arco._itemNoMapa = false;
                            break;                                                         
                        }
                        
                        acaoInimigo();
                        break;
                        
                    } else if (DB.modelosPersonagem.Contains(destino)) {
                        if(DB.modelosPersonagemComEscudo.Contains(destino)){
                            Personagem.inventario.escudo._equipado = false;
                            acaoInimigo(true);
                            break;   
                        } else {
                            if (tipoInimigo != "@@") {
                                LimpaLugarAntigoInimigo(posicoesInimigos, i);
                            }
                            acaoInimigo();
                            Mapa.CheckMapaIsRenderizando();
                            MostrarMensagem.GameOver();
                            Environment.Exit(0);
                        }
                    } else if( 
                            tipoInimigo == "§§" &&
                            cloneAltPosicao.ElementAt(idxAleatorio).ElementAt(0) + posicoesInimigos[i, 0] > 0 && 
                            cloneAltPosicao.ElementAt(idxAleatorio).ElementAt(0) + posicoesInimigos[i, 0] < Game.GetMapa().GetLength(0)-1 && 
                            cloneAltPosicao.ElementAt(idxAleatorio).ElementAt(1) + posicoesInimigos[i, 1] > 0 && 
                            cloneAltPosicao.ElementAt(idxAleatorio).ElementAt(1) + posicoesInimigos[i, 1] < Game.GetMapa().GetLength(1)-1 && 
                            !DB.todosTiposInimigo.Contains(destino)
                        ){
                        LimpaLugarAntigoInimigo(posicoesInimigos, i);
                        acaoInimigo();
                        break;

                    } else {
                        cloneAltPosicao.RemoveAt(idxAleatorio);
                    }
                };
            }
            Mapa.CheckMapaIsRenderizando();
        }

        public static bool VerificaInimigoTemVisaoJogador(int[] posicaoInimigo, ref enumDirecao direcaoProjetil){
            if(
                posicaoInimigo[0] == Personagem.posicaoPersonagem[0] || 
                posicaoInimigo[1] == Personagem.posicaoPersonagem[1]
            ){
                int posicaoInicial;
                int posicaoFinal;
                bool estaoMesmaLinha = false;
                int posicaoLinhaOuColuna;
                // Verifica se está na mesma linha
                if (posicaoInimigo[0] == Personagem.posicaoPersonagem[0]) {
                    posicaoInicial = posicaoInimigo[1];
                    posicaoFinal = Personagem.posicaoPersonagem[1];
                    estaoMesmaLinha = true;
                    posicaoLinhaOuColuna = posicaoInimigo[0];

                // Verifica se está na mesma coluna
                } else {
                    posicaoInicial = posicaoInimigo[0];
                    posicaoFinal = Personagem.posicaoPersonagem[0];
                    posicaoLinhaOuColuna = posicaoInimigo[1];
                }

                bool posicaoFinalMaiorQueInicial = posicaoFinal > posicaoInicial;

                // Atribui direcao a variavel baseado na posicaoFinalMaiorQueInicial e estaoMesmaLinha
                if (estaoMesmaLinha) {
                    direcaoProjetil = posicaoFinalMaiorQueInicial ? enumDirecao.Direita : enumDirecao.Esquerda;
                } else {
                    direcaoProjetil = posicaoFinalMaiorQueInicial ? enumDirecao.Baixo : enumDirecao.Cima;
                }

                bool LocalEstaVazio(int p) {
                    if (estaoMesmaLinha) {
                        return Game.GetMapa()[posicaoLinhaOuColuna, p] == "  ";
                    }
                    return Game.GetMapa()[p, posicaoLinhaOuColuna] == "  ";
                }

                // acrescenta no for até chegar na final
                if(posicaoFinalMaiorQueInicial){
                    for (int p = posicaoInicial+1; p < posicaoFinal; p++) {
                        if (!LocalEstaVazio(p)) {
                            return false;
                        }
                    }

                // diminui no for até chegar na final
                }else{
                    for (int p = posicaoInicial-1; p > posicaoFinal; p--) {
                        if (!LocalEstaVazio(p)) {
                            return false;
                        }
                    }
                }
                return true;
            }
            return false;
        }
        public static async Task AtirarProjetil(enumDirecao direcao, List<int> posicaoProjetil) {
            static string limpaLugarAntigoProjetil(List<int> posicao) => 
                Game.GetMapa()[posicao[0], posicao[1]] = "  ";

            List<int> destinoPosicoes = new List<int>(posicaoProjetil);

            if (
                destinoPosicoes[0] == Game.GetMapa().GetLength(0) - 1
                || destinoPosicoes[1] == Game.GetMapa().GetLength(1) - 1
                || destinoPosicoes[0] == 0
                || destinoPosicoes[1] == 0
            ) {
                limpaLugarAntigoProjetil(posicaoProjetil);
                return;
            }

            switch (direcao) {
                case enumDirecao.Cima:
                    destinoPosicoes[0] = posicaoProjetil[0]-1;
                break;
                case enumDirecao.Baixo:
                    destinoPosicoes[0] = posicaoProjetil[0]+1;
                break;
                case enumDirecao.Direita:
                    destinoPosicoes[1] = posicaoProjetil[1]+1;
                break; 
                case enumDirecao.Esquerda:
                    destinoPosicoes[1] = posicaoProjetil[1]-1;
                break;       
            }
            
            string destinoString = Game.GetMapa()[destinoPosicoes[0], destinoPosicoes[1]];

            if (
                DB.modelosFlechas.Contains(destinoString)
                || DB.modelosFlechas.Contains(Game.GetMapa()[posicaoProjetil[0], posicaoProjetil[1]])
            ) {
                Game.GetMapa()[destinoPosicoes[0],destinoPosicoes[1]] = "  ";
                if (Game.GetMapa()[posicaoProjetil[0], posicaoProjetil[1]] != "(>") {
                    limpaLugarAntigoProjetil(posicaoProjetil); 
                }
                Mapa.CheckMapaIsRenderizando();       
                return;  
            }


            if (destinoString == "  ") {
                if (Game.GetMapa()[posicaoProjetil[0], posicaoProjetil[1]] != "(>") {
                    limpaLugarAntigoProjetil(posicaoProjetil);
                }
                Game.GetMapa()[destinoPosicoes[0], destinoPosicoes[1]] = "::";

                Mapa.CheckMapaIsRenderizando();
                await Task.Delay(300);
                if (Game.GetMapa()[destinoPosicoes[0], destinoPosicoes[1]] != "  ") {
                    await AtirarProjetil(direcao, destinoPosicoes);   
                }               
            }else{
                if(DB.modelosPersonagem.Contains(destinoString)) {
                    if(DB.modelosPersonagemComEscudo.Contains(destinoString)){
                        Personagem.inventario.escudo._equipado = false;
                        Game.GetMapa()[destinoPosicoes[0], destinoPosicoes[1]] = Personagem.GetPersonagemEquipado();

                    } else {
                        MostrarMensagem.GameOver();
                        Environment.Exit(0);
                    }

                }else if(DB.todosTiposItens.FindIndex((i) => i._modelo == destinoString) >= 0){
                    EnumItens tipoItem = (EnumItens)DB.todosTiposItens.FindIndex(i => i._modelo == destinoString);
                    switch(tipoItem) {
                        case EnumItens.espada:
                            Personagem.inventario.espada._itemNoMapa = false;
                        break;
                        case EnumItens.escudo:
                            Personagem.inventario.escudo._itemNoMapa = false;
                        break;
                        case EnumItens.picareta:
                            Personagem.inventario.picareta._itemNoMapa = false;
                        break;   
                        case EnumItens.arco:
                            Personagem.inventario.arco._itemNoMapa = false;
                        break;                                                         
                    }
                    Game.GetMapa()[destinoPosicoes[0],destinoPosicoes[1]] = "  ";
                }

                if (Game.GetMapa()[posicaoProjetil[0], posicaoProjetil[1]] != "(>") {
                    limpaLugarAntigoProjetil(posicaoProjetil); 
                }
                Mapa.CheckMapaIsRenderizando();         
            }        
        }
        public static async Task IntervaloMovimentoInimigo(){
            if (Game.FaseAtual._qntInimigosTipo1 > 0 && !Game.pararRenderizacoes) {              
                await Task.Delay((int)(1500 * Game.dificuldade));
                MovimentacaoInimigo(DB.todosTiposInimigo[(int)EnumInimigos.tipo1]);
                await IntervaloMovimentoInimigo();  
            }
        }
        public static async Task IntervaloMovimentoInimigo2(){
            if (Game.FaseAtual._qntInimigosTipo2 > 0 && !Game.pararRenderizacoes) {
                await Task.Delay((int)(750 * Game.dificuldade));
                MovimentacaoInimigo(DB.todosTiposInimigo[(int)EnumInimigos.tipo2]);
                await IntervaloMovimentoInimigo2();  
            }
        }
        public static async Task IntervaloMovimentoInimigo3(){
            if (Game.FaseAtual._qntInimigosTipo3 > 0 && !Game.pararRenderizacoes) {
                await Task.Delay((int)(1200 * Game.dificuldade));
                MovimentacaoInimigo(DB.todosTiposInimigo[(int)EnumInimigos.tipo3]);
                await IntervaloMovimentoInimigo3();  
            }
        }
        public static async Task IntervaloMovimentoInimigo4(){
            if (Game.FaseAtual._qntInimigosTipo4 > 0 && !Game.pararRenderizacoes) {              
                await Task.Delay((int)(1500 * Game.dificuldade));
                MovimentacaoInimigo(DB.todosTiposInimigo[(int)EnumInimigos.tipo4]);
                await IntervaloMovimentoInimigo4();  
            }
        }
        public static async Task IntervaloMovimentoInimigo5(){
            if (Game.FaseAtual._qntInimigosTipo5 > 0 && !Game.pararRenderizacoes) {           
                await Task.Delay((int)(1500 * Game.dificuldade));
                MovimentacaoInimigo(DB.todosTiposInimigo[(int)EnumInimigos.tipo5]);
                await IntervaloMovimentoInimigo5();  
            }
        }
        //Inimigo Evolutivo
        public static async Task IntervaloMovimentoInimigo6(){
            if (Game.FaseAtual._qntInimigosTipo6 > 0 && !Game.pararRenderizacoes) {           
                await Task.Delay((int)(750 * Game.dificuldade));
                MovimentacaoInimigo(DB.todosTiposInimigo[(int)EnumInimigos.tipo6]);
                await IntervaloMovimentoInimigo6();  
            }
        }
        public static async Task IntervaloMovimentoInimigo7(){
            if (Game.FaseAtual._qntInimigosTipo7 > 0 && !Game.pararRenderizacoes) {           
                await Task.Delay((int)(750 * Game.dificuldade));
                MovimentacaoInimigo(DB.todosTiposInimigo[(int)EnumInimigos.tipo7]);
                await IntervaloMovimentoInimigo7();  
            }
        }
        public static async Task IntervaloMovimentoInimigo8(){
            if (Game.FaseAtual._qntInimigosTipo8 > 0 && !Game.pararRenderizacoes) {           
                await Task.Delay((int)(450 * Game.dificuldade));
                MovimentacaoInimigo(DB.todosTiposInimigo[(int)EnumInimigos.tipo8]);
                await IntervaloMovimentoInimigo8();  
            }
        }
        public static async Task IntervaloMovimentoInimigo9(){
            if (Game.FaseAtual._qntInimigosTipo9 > 0 && !Game.pararRenderizacoes) {           
                await Task.Delay((int)(450 * Game.dificuldade));
                MovimentacaoInimigo(DB.todosTiposInimigo[(int)EnumInimigos.tipo9]);
                await IntervaloMovimentoInimigo9();  
            }
        }
    }
}