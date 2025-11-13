using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace ToolkitSigma
{
    // ========== CLASSES AUXILIARES ==========

    public class ItemProblemaInstancia
    {
        public string Texto { get; set; } = "";
        public string TipoCorreto { get; set; } = "";
    }

    public class TransicaoAFD
    {
        public string EstadoOrigem { get; set; } = "";
        public string Simbolo { get; set; } = "";
        public string EstadoDestino { get; set; } = "";
    }

    public class AutomatoFinitoDeterministico
    {
        public string Nome { get; set; } = "";
        public string[] Estados { get; set; } = Array.Empty<string>();
        public string EstadoInicial { get; set; } = "";
        public string[] EstadosDeAceitacao { get; set; } = Array.Empty<string>();
        public string[] Alfabeto { get; set; } = Array.Empty<string>();
        public List<TransicaoAFD> Transicoes { get; set; } = new List<TransicaoAFD>();
    }

    // ========== PROGRAMA PRINCIPAL ==========

    public static class Programa
    {
        public static void Main(string[] argumentos)
        {
            bool continuar = true;

            while (continuar)
            {
                ExibirMenuPrincipal();
                int opcao = LerOpcao(0, 5);

                Console.WriteLine();

                switch (opcao)
                {
                    case 1:
                        ExecutarModulo1_ProblemaInstancia();
                        break;
                    case 2:
                        ExecutarModulo2_Decidiveis();
                        break;
                    case 3:
                        ExecutarModulo3_Reconheciveis();
                        break;
                    case 4:
                        ExecutarModulo4_DetectorLoop();
                        break;
                    case 5:
                        ExecutarModulo5_SimuladorAFD();
                        break;
                    case 0:
                        continuar = false;
                        Console.WriteLine("Encerrando Toolkit Sigma. Ate logo!");
                        break;
                }

                if (continuar)
                {
                    Console.WriteLine();
                    Console.WriteLine("Pressione qualquer tecla para continuar...");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
        }

        private static void ExibirMenuPrincipal()
        {
            Console.WriteLine("╔════════════════════════════════════════════╗");
            Console.WriteLine("║         TOOLKIT SIGMA - AV2                ║");
            Console.WriteLine("║    Computacao Cientifica - UNIFESO         ║");
            Console.WriteLine("╚════════════════════════════════════════════╝");
            Console.WriteLine();
            Console.WriteLine("1) Problema x Instancia (JSON)");
            Console.WriteLine("2) Decidiveis: L_fim_b e L_mult3_b");
            Console.WriteLine("3) Reconheciveis: demonstracao");
            Console.WriteLine("4) Detector ingenuo de loop");
            Console.WriteLine("5) Simulador de AFD");
            Console.WriteLine("0) Sair");
            Console.WriteLine();
            Console.Write("Escolha o modulo: ");
        }

        // ========== MÓDULO 1: PROBLEMA × INSTÂNCIA ==========

        private static void ExecutarModulo1_ProblemaInstancia()
        {
            Console.WriteLine("=== MODULO 1: Problema x Instancia ===");
            Console.WriteLine();
            Console.WriteLine("Classifique cada item como:");
            Console.WriteLine("P = Problema (descricao generica)");
            Console.WriteLine("I = Instancia (caso concreto)");
            Console.WriteLine();

            string json = ObterJsonProblemaInstancia();
            List<ItemProblemaInstancia>? itens =
                JsonSerializer.Deserialize<List<ItemProblemaInstancia>>(json);

            if (itens is null || itens.Count == 0)
            {
                Console.WriteLine("Erro ao carregar dados.");
                return;
            }

            int acertos = 0;
            int erros = 0;

            for (int i = 0; i < itens.Count; i++)
            {
                ItemProblemaInstancia item = itens[i];
                Console.WriteLine($"[{i + 1}/{itens.Count}] {item.Texto}");

                string resposta = LerProblemaOuInstancia();
                bool correto = resposta.Equals(item.TipoCorreto,
                    StringComparison.OrdinalIgnoreCase);

                if (correto)
                {
                    acertos++;
                    Console.WriteLine("✓ Correto!");
                }
                else
                {
                    erros++;
                    Console.WriteLine($"✗ Incorreto. Gabarito: {item.TipoCorreto}");
                }
                Console.WriteLine();
            }

            Console.WriteLine("────────────────────────────────");
            Console.WriteLine($"Acertos: {acertos}");
            Console.WriteLine($"Erros: {erros}");
            Console.WriteLine($"Total: {itens.Count}");
            Console.WriteLine($"Percentual: {(acertos * 100.0 / itens.Count):F1}%");
        }

        private static string ObterJsonProblemaInstancia()
        {
            return @"[
                {""Texto"": ""Ordenar uma lista de numeros"", ""TipoCorreto"": ""P""},
                {""Texto"": ""A lista [12, 7, 3, 9]"", ""TipoCorreto"": ""I""},
                {""Texto"": ""Verificar se um numero eh primo"", ""TipoCorreto"": ""P""},
                {""Texto"": ""O numero 37"", ""TipoCorreto"": ""I""},
                {""Texto"": ""Decidir se cadeia sobre {a,b} termina com b"", ""TipoCorreto"": ""P""},
                {""Texto"": ""A cadeia 'abb'"", ""TipoCorreto"": ""I""},
                {""Texto"": ""Problema da parada"", ""TipoCorreto"": ""P""},
                {""Texto"": ""O par (programa X, entrada '101')"", ""TipoCorreto"": ""I""},
                {""Texto"": ""Satisfatibilidade booleana (SAT)"", ""TipoCorreto"": ""P""},
                {""Texto"": ""A formula (P AND Q) OR (NOT R)"", ""TipoCorreto"": ""I""}
            ]";
        }

        private static string LerProblemaOuInstancia()
        {
            while (true)
            {
                Console.Write("Sua resposta (P/I): ");
                string? texto = Console.ReadLine();
                if (texto is null) continue;

                texto = texto.Trim().ToUpperInvariant();
                if (texto == "P" || texto == "I")
                {
                    return texto;
                }

                Console.WriteLine("Opcao invalida. Digite P ou I.");
            }
        }

        // ========== MÓDULO 2: DECIDÍVEIS ==========

        private static void ExecutarModulo2_Decidiveis()
        {
            Console.WriteLine("=== MODULO 2: Linguagens Decidiveis ===");
            Console.WriteLine("Alfabeto Sigma = {a, b}");
            Console.WriteLine();
            Console.WriteLine("Escolha a linguagem:");
            Console.WriteLine("1) L_fim_b = { w | w termina com 'b' }");
            Console.WriteLine("2) L_mult3_b = { w | numero de 'b' eh multiplo de 3 }");
            Console.WriteLine();

            int opcao = LerOpcao(1, 2);

            Console.WriteLine("Digite a cadeia (Enter = vazia):");
            string? entrada = Console.ReadLine();
            string cadeia = entrada is null ? string.Empty : entrada;

            if (!PertenceAoAlfabeto(cadeia))
            {
                Console.WriteLine("Cadeia invalida. Use apenas 'a' ou 'b'.");
                return;
            }

            bool resultado;
            string nomeDecisao;

            if (opcao == 1)
            {
                resultado = DecisorTerminaComB(cadeia);
                nomeDecisao = "L_fim_b";
            }
            else
            {
                resultado = DecisorMultiploDe3B(cadeia);
                nomeDecisao = "L_mult3_b";
            }

            Console.WriteLine();
            Console.WriteLine($"Linguagem: {nomeDecisao}");
            Console.WriteLine($"Cadeia: \"{cadeia}\"");
            Console.WriteLine($"Comprimento: {cadeia.Length}");
            Console.WriteLine($"Decisao: {(resultado ? "SIM (aceita)" : "NAO (rejeita)")}");
            Console.WriteLine();
            Console.WriteLine("Observacao: Este decisor sempre termina para qualquer");
            Console.WriteLine("entrada valida sobre o alfabeto {a, b}.");
        }

        private static bool DecisorTerminaComB(string cadeia)
        {
            // Pre-condicao: cadeia sobre {a,b}
            // Pos-condicao: retorna true se termina com 'b', false caso contrario

            if (cadeia.Length == 0)
            {
                return false;
            }

            char ultimoSimbolo = cadeia[cadeia.Length - 1];
            bool terminaComB = ultimoSimbolo == 'b';
            return terminaComB;
        }

        private static bool DecisorMultiploDe3B(string cadeia)
        {
            // Pre-condicao: cadeia sobre {a,b}
            // Pos-condicao: retorna true se numero de 'b' eh multiplo de 3

            int contadorB = 0;

            for (int i = 0; i < cadeia.Length; i++)
            {
                if (cadeia[i] == 'b')
                {
                    contadorB = contadorB + 1;
                }
            }

            bool multiploDeTres = (contadorB % 3) == 0;
            return multiploDeTres;
        }

        // ========== MÓDULO 3: RECONHECÍVEIS ==========

        private static void ExecutarModulo3_Reconheciveis()
        {
            Console.WriteLine("=== MODULO 3: Linguagens Reconheciveis ===");
            Console.WriteLine();
            Console.WriteLine("Linguagem: L_contem_ab = { w | w contem 'ab' como substring }");
            Console.WriteLine("Alfabeto Sigma = {a, b}");
            Console.WriteLine();
            Console.WriteLine("Este reconhecedor:");
            Console.WriteLine("- ACEITA e termina se encontrar 'ab'");
            Console.WriteLine("- Pode NAO TERMINAR se nao houver 'ab' (simulado com limite)");
            Console.WriteLine();

            Console.WriteLine("Digite a cadeia (Enter = vazia):");
            string? entrada = Console.ReadLine();
            string cadeia = entrada is null ? string.Empty : entrada;

            if (!PertenceAoAlfabeto(cadeia))
            {
                Console.WriteLine("Cadeia invalida. Use apenas 'a' ou 'b'.");
                return;
            }

            Console.Write("Limite de passos (ex: 100): ");
            int limite = LerInteiroPositivo();

            Console.WriteLine();
            Console.WriteLine("Executando reconhecedor...");

            string resultado = ReconhecedorContemAB(cadeia, limite);

            Console.WriteLine();
            Console.WriteLine($"Resultado: {resultado}");

            if (resultado == "INDETERMINADO")
            {
                Console.WriteLine();
                Console.WriteLine("Interpretacao: O reconhecedor foi interrompido apos");
                Console.WriteLine($"{limite} passos sem encontrar 'ab'. Isto simula um");
                Console.WriteLine("reconhecedor que pode nao terminar quando a cadeia");
                Console.WriteLine("nao pertence a linguagem.");
            }
        }

        private static string ReconhecedorContemAB(string cadeia, int limitePassos)
        {
            // Reconhecedor que procura 'ab' ciclicamente
            // Se encontrar, aceita. Se nao encontrar, pode nao terminar.

            if (cadeia.Length < 2)
            {
                // Cadeias com menos de 2 simbolos nunca contem "ab"
                // O reconhecedor permanece buscando indefinidamente
                int passos = 0;
                while (passos < limitePassos)
                {
                    passos++;
                }
                return "INDETERMINADO (simulando nao terminacao)";
            }

            int indice = 0;
            int passosExecutados = 0;

            while (passosExecutados < limitePassos)
            {
                char atual = cadeia[indice];
                char proximo = cadeia[(indice + 1) % cadeia.Length];

                if (atual == 'a' && proximo == 'b')
                {
                    return "ACEITA";
                }

                indice = (indice + 1) % cadeia.Length;
                passosExecutados++;
            }

            return "INDETERMINADO (simulando nao terminacao)";
        }

        // ========== MÓDULO 4: DETECTOR INGÊNUO DE LOOP ==========

        private static void ExecutarModulo4_DetectorLoop()
        {
            Console.WriteLine("=== MODULO 4: Detector Ingenuo de Loop ===");
            Console.WriteLine();
            Console.WriteLine("Este modulo simula um processo discreto e detecta");
            Console.WriteLine("repeticao de estados como indicativo de loop.");
            Console.WriteLine();
            Console.WriteLine("Escolha o comportamento do processo:");
            Console.WriteLine("1) Termina apos N passos (sem loop)");
            Console.WriteLine("2) Entra em ciclo repetitivo (loop detectavel)");
            Console.WriteLine("3) Cresce indefinidamente (sem repeticao)");
            Console.WriteLine();

            int modo = LerOpcao(1, 3);

            Console.Write("Limite de passos para observacao: ");
            int limite = LerInteiroPositivo();

            Console.WriteLine();
            Console.WriteLine("Executando processo...");

            string resultado = ExecutarDetectorDeLoop(modo, limite);

            Console.WriteLine();
            Console.WriteLine($"Resultado: {resultado}");
            Console.WriteLine();
            Console.WriteLine("=== REFLEXAO ===");
            Console.WriteLine();
            Console.WriteLine("Falsos positivos:");
            Console.WriteLine("- Um processo pode repetir estado temporariamente");
            Console.WriteLine("  mas ainda assim terminar depois.");
            Console.WriteLine();
            Console.WriteLine("Falsos negativos:");
            Console.WriteLine("- Um loop com periodo maior que o limite nao sera");
            Console.WriteLine("  detectado (exemplo: ciclo de 1000 estados com");
            Console.WriteLine("  limite de 500 passos).");
            Console.WriteLine();
            Console.WriteLine("Conclusao:");
            Console.WriteLine("- Heuristicas de timeout ou deteccao de estado sao");
            Console.WriteLine("  uteis na pratica, mas NAO RESOLVEM o problema da");
            Console.WriteLine("  parada em geral. Sempre existem casos limites.");
        }

        private static string ExecutarDetectorDeLoop(int modo, int limite)
        {
            HashSet<int> estadosVisitados = new HashSet<int>();
            int estadoAtual = 0;
            int passos = 0;

            while (passos < limite)
            {
                // Verificar se o estado ja foi visitado
                if (estadosVisitados.Contains(estadoAtual))
                {
                    return $"LOOP DETECTADO no passo {passos} (estado {estadoAtual} repetido)";
                }

                estadosVisitados.Add(estadoAtual);

                // Simular transicao de estado conforme o modo
                if (modo == 1)
                {
                    // Modo 1: Termina em 10 passos
                    estadoAtual++;
                    if (estadoAtual >= 10)
                    {
                        return $"TERMINOU no passo {passos + 1}";
                    }
                }
                else if (modo == 2)
                {
                    // Modo 2: Ciclo de 5 estados (0->1->2->3->4->0)
                    estadoAtual = (estadoAtual + 1) % 5;
                }
                else
                {
                    // Modo 3: Cresce indefinidamente
                    estadoAtual = estadoAtual + passos + 1;
                }

                passos++;
            }

            return $"INDETERMINADO (limite de {limite} passos atingido sem conclusao)";
        }

        // ========== MÓDULO 5: SIMULADOR DE AFD ==========

        private static void ExecutarModulo5_SimuladorAFD()
        {
            Console.WriteLine("=== MODULO 5: Simulador de AFD ===");
            Console.WriteLine();
            Console.WriteLine("Alfabeto Sigma = {a, b}");
            Console.WriteLine();
            Console.WriteLine("Escolha o AFD:");
            Console.WriteLine("1) L_par_a = { w | numero de 'a' eh par }");
            Console.WriteLine("2) L_a_b_estrela = { w | w = a b* }");
            Console.WriteLine();

            int opcaoAFD = LerOpcao(1, 2);

            AutomatoFinitoDeterministico afd = (opcaoAFD == 1)
                ? ObterAFD_ParDeA()
                : ObterAFD_ABEstrela();

            Console.WriteLine();
            Console.WriteLine($"AFD selecionado: {afd.Nome}");
            Console.WriteLine($"Estado inicial: {afd.EstadoInicial}");
            Console.WriteLine($"Estados de aceitacao: {string.Join(", ", afd.EstadosDeAceitacao)}");
            Console.WriteLine();

            Console.WriteLine("Digite a cadeia (Enter = vazia):");
            string? entrada = Console.ReadLine();
            string cadeia = entrada is null ? string.Empty : entrada;

            if (!PertenceAoAlfabeto(cadeia))
            {
                Console.WriteLine("Cadeia invalida. Use apenas 'a' ou 'b'.");
                return;
            }

            Console.WriteLine();
            Console.WriteLine("=== EXECUCAO PASSO A PASSO ===");
            Console.WriteLine();

            string estadoAtual = afd.EstadoInicial;
            Console.WriteLine($"Estado inicial: {estadoAtual}");

            for (int i = 0; i < cadeia.Length; i++)
            {
                char simbolo = cadeia[i];
                string? proximoEstado = ObterProximoEstado(afd, estadoAtual, simbolo);

                Console.WriteLine($"Passo {i + 1}: lendo '{simbolo}' no estado {estadoAtual}");

                if (proximoEstado is null)
                {
                    Console.WriteLine("  -> SEM TRANSICAO DEFINIDA");
                    Console.WriteLine();
                    Console.WriteLine("REJEITA (transicao inexistente)");
                    return;
                }

                estadoAtual = proximoEstado;
                Console.WriteLine($"  -> transicao para {estadoAtual}");
            }

            bool aceita = EstadoEhDeAceitacao(afd, estadoAtual);

            Console.WriteLine();
            Console.WriteLine($"Estado final: {estadoAtual}");
            Console.WriteLine();
            Console.WriteLine(aceita ? "ACEITA" : "REJEITA");
        }

        private static AutomatoFinitoDeterministico ObterAFD_ParDeA()
        {
            AutomatoFinitoDeterministico afd = new AutomatoFinitoDeterministico
            {
                Nome = "L_par_a (numero par de 'a')",
                Estados = new string[] { "Par", "Impar" },
                EstadoInicial = "Par",
                EstadosDeAceitacao = new string[] { "Par" },
                Alfabeto = new string[] { "a", "b" },
                Transicoes = new List<TransicaoAFD>
                {
                    new TransicaoAFD { EstadoOrigem = "Par", Simbolo = "a", EstadoDestino = "Impar" },
                    new TransicaoAFD { EstadoOrigem = "Par", Simbolo = "b", EstadoDestino = "Par" },
                    new TransicaoAFD { EstadoOrigem = "Impar", Simbolo = "a", EstadoDestino = "Par" },
                    new TransicaoAFD { EstadoOrigem = "Impar", Simbolo = "b", EstadoDestino = "Impar" }
                }
            };
            return afd;
        }

        private static AutomatoFinitoDeterministico ObterAFD_ABEstrela()
        {
            AutomatoFinitoDeterministico afd = new AutomatoFinitoDeterministico
            {
                Nome = "L_a_b_estrela (a seguido de zero ou mais b)",
                Estados = new string[] { "Inicio", "AceitaB", "Rejeita" },
                EstadoInicial = "Inicio",
                EstadosDeAceitacao = new string[] { "AceitaB" },
                Alfabeto = new string[] { "a", "b" },
                Transicoes = new List<TransicaoAFD>
                {
                    new TransicaoAFD { EstadoOrigem = "Inicio", Simbolo = "a", EstadoDestino = "AceitaB" },
                    new TransicaoAFD { EstadoOrigem = "Inicio", Simbolo = "b", EstadoDestino = "Rejeita" },
                    new TransicaoAFD { EstadoOrigem = "AceitaB", Simbolo = "b", EstadoDestino = "AceitaB" },
                    new TransicaoAFD { EstadoOrigem = "AceitaB", Simbolo = "a", EstadoDestino = "Rejeita" },
                    new TransicaoAFD { EstadoOrigem = "Rejeita", Simbolo = "a", EstadoDestino = "Rejeita" },
                    new TransicaoAFD { EstadoOrigem = "Rejeita", Simbolo = "b", EstadoDestino = "Rejeita" }
                }
            };
            return afd;
        }

        private static string? ObterProximoEstado(
            AutomatoFinitoDeterministico afd,
            string estadoAtual,
            char simbolo)
        {
            string simboloStr = simbolo.ToString();

            for (int i = 0; i < afd.Transicoes.Count; i++)
            {
                TransicaoAFD t = afd.Transicoes[i];
                if (t.EstadoOrigem == estadoAtual && t.Simbolo == simboloStr)
                {
                    return t.EstadoDestino;
                }
            }

            return null;
        }

        private static bool EstadoEhDeAceitacao(
            AutomatoFinitoDeterministico afd,
            string estado)
        {
            for (int i = 0; i < afd.EstadosDeAceitacao.Length; i++)
            {
                if (afd.EstadosDeAceitacao[i] == estado)
                {
                    return true;
                }
            }
            return false;
        }

        // ========== FUNÇÕES AUXILIARES ==========

        private static bool PertenceAoAlfabeto(string cadeia)
        {
            for (int i = 0; i < cadeia.Length; i++)
            {
                char s = cadeia[i];
                if (s != 'a' && s != 'b')
                {
                    return false;
                }
            }
            return true;
        }

        private static int LerOpcao(int minimo, int maximo)
        {
            while (true)
            {
                string? texto = Console.ReadLine();
                if (int.TryParse(texto, out int valor) &&
                    valor >= minimo && valor <= maximo)
                {
                    return valor;
                }
                Console.Write($"Opcao invalida. Digite um valor entre {minimo} e {maximo}: ");
            }
        }

        private static int LerInteiroPositivo()
        {
            while (true)
            {
                string? texto = Console.ReadLine();
                if (int.TryParse(texto, out int valor) && valor > 0)
                {
                    return valor;
                }
                Console.Write("Digite um inteiro positivo: ");
            }
        }
    }
}
