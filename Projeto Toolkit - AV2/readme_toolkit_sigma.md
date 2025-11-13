# 🔧 Toolkit Sigma - Projeto AV2

**Disciplina:** Computação Científica  
**Instituição:** UNIFESO  
**Professor:** Dr. André Campos

---

## 👥 Equipe

**Nome Completo:** [SEU NOME AQUI]  
**Matrícula:** [SUA MATRÍCULA AQUI]

**Nome Completo:** [NOME DO COLEGA - se houver]  
**Matrícula:** [MATRÍCULA DO COLEGA - se houver]

---

## 📝 Descrição do Projeto

Este projeto implementa um **Toolkit console em C# (.NET 9)** contendo 5 módulos educacionais que demonstram conceitos fundamentais de Teoria da Computação, abordando:

- Diferenciação entre problemas e instâncias
- Linguagens decidíveis e reconhecíveis
- Detecção heurística de loops
- Simulação de Autômatos Finitos Determinísticos (AFD)

---

## 🎯 Módulos Implementados

### 📌 **Módulo 1: Problema × Instância** (2,0 pts)

**Objetivo:** Classificar frases como Problema (P) ou Instância (I)

**Funcionalidades:**
- Carrega 10 itens de JSON embutido
- Interface interativa para classificação
- Feedback imediato com gabarito
- Estatísticas de acertos/erros com percentual

**Como usar:**
1. Selecione opção 1 no menu
2. Para cada item exibido, digite `P` ou `I`
3. Veja o resultado final com estatísticas

---

### 📌 **Módulo 2: Linguagens Decidíveis** (2,0 pts)

**Objetivo:** Implementar decisores sobre Σ = {a, b}

**Linguagens:**
- **L_fim_b:** Cadeias que terminam com 'b'
- **L_mult3_b:** Cadeias com número de 'b' múltiplo de 3

**Características:**
- Sempre terminam em tempo finito
- Resposta definitiva (SIM/NÃO) para toda entrada válida
- Validação de alfabeto

**Como usar:**
1. Selecione opção 2 no menu
2. Escolha a linguagem (1 ou 2)
3. Digite a cadeia a ser testada
4. Veja a decisão com explicação

**Exemplos:**
```
L_fim_b:
"abb" → SIM
"aba" → NÃO
"" → NÃO

L_mult3_b:
"bbb" → SIM (3 b's)
"abbbbb" → SIM (6 b's)
"ab" → NÃO (1 b)
```

---

### 📌 **Módulo 3: Linguagens Reconhecíveis** (2,0 pts)

**Objetivo:** Demonstrar reconhecedor que pode não terminar

**Linguagem:** L_contem_ab = { w | w contém "ab" como substring }

**Comportamento:**
- **ACEITA** quando encontra "ab"
- **INDETERMINADO** quando não encontra (simulando não terminação)
- Limite de passos configurável

**Como usar:**
1. Selecione opção 3 no menu
2. Digite a cadeia
3. Configure o limite de passos (ex: 100)
4. Observe o resultado

**Exemplos:**
```
"ab" com limite 10 → ACEITA
"bbb" com limite 50 → INDETERMINADO
"aba" com limite 20 → ACEITA
```

---

### 📌 **Módulo 4: Detector Ingênuo de Loop** (2,0 pts)

**Objetivo:** Experimentar heurística de detecção de loops

**Modos de teste:**
1. **Termina após N passos** (sem loop)
2. **Entra em ciclo repetitivo** (loop detectável)
3. **Cresce indefinidamente** (sem repetição)

**Técnica:**
- Memoriza estados visitados
- Detecta repetição como indicativo de loop
- Limite configurável de passos

**Como usar:**
1. Selecione opção 4 no menu
2. Escolha o modo (1, 2 ou 3)
3. Configure limite de observação
4. Analise o resultado e a reflexão

**Reflexão incluída sobre:**
- ⚠️ **Falsos positivos:** repetição temporária sem loop real
- ⚠️ **Falsos negativos:** loops com período maior que limite
- 💡 **Conclusão:** limitações das heurísticas vs. Problema da Parada

---

### 📌 **Módulo 5: Simulador de AFD** (2,0 pts)

**Objetivo:** Simular Autômatos Finitos Determinísticos

**AFDs disponíveis:**
1. **L_par_a:** Número par de 'a'
   - Estados: {Par, Impar}
   - Aceita: Par
   
2. **L_a_b_estrela:** a seguido de zero ou mais b
   - Estados: {Inicio, AceitaB, Rejeita}
   - Aceita: AceitaB

**Funcionalidades:**
- Execução passo a passo
- Exibição do estado atual
- Indicação de transições
- Resultado final (ACEITA/REJEITA)

**Como usar:**
1. Selecione opção 5 no menu
2. Escolha o AFD (1 ou 2)
3. Digite a cadeia
4. Acompanhe a execução passo a passo

**Exemplos:**
```
L_par_a:
"aa" → ACEITA (2 a's - par)
"aba" → REJEITA (1 a - ímpar)
"" → ACEITA (0 a's - par)

L_a_b_estrela:
"a" → ACEITA
"abbb" → ACEITA
"ba" → REJEITA
"" → REJEITA
```

---

## 🛠️ Tecnologias Utilizadas

- **Linguagem:** C# 11
- **Framework:** .NET 9
- **Serialização:** System.Text.Json
- **Paradigma:** Procedural com tipos explícitos

---

## 🚀 Como Executar

### **Pré-requisitos:**
- .NET SDK 9.0 ou superior
- Visual Studio 2022, VS Code ou Rider

### **Passos:**

1. **Clone o repositório:**
```bash
git clone https://github.com/[seu-usuario]/toolkit-sigma.git
cd toolkit-sigma
```

2. **Compile o projeto:**
```bash
dotnet build
```

3. **Execute:**
```bash
dotnet run
```

4. **Navegue pelo menu:**
```
╔════════════════════════════════════════════╗
║         TOOLKIT SIGMA - AV2                ║
║    Computacao Cientifica - UNIFESO         ║
╚════════════════════════════════════════════╝

1) Problema x Instancia (JSON)
2) Decidiveis: L_fim_b e L_mult3_b
3) Reconheciveis: demonstracao
4) Detector ingenuo de loop
5) Simulador de AFD
0) Sair

Escolha o modulo: _
```

---

## 📂 Estrutura do Código

```
ToolkitSigma/
│
├── Program.cs                  # Arquivo principal
│   ├── Classes Auxiliares
│   │   ├── ItemProblemaInstancia
│   │   ├── TransicaoAFD
│   │   └── AutomatoFinitoDeterministico
│   │
│   ├── Módulo 1: ProblemaInstancia()
│   ├── Módulo 2: Decidiveis()
│   ├── Módulo 3: Reconheciveis()
│   ├── Módulo 4: DetectorLoop()
│   ├── Módulo 5: SimuladorAFD()
│   │
│   └── Funções Auxiliares
│       ├── PertenceAoAlfabeto()
│       ├── LerOpcao()
│       └── LerInteiroPositivo()
│
└── README.md                   # Este arquivo
```

---

## ✅ Critérios Atendidos

### **Critérios Técnicos:**
- ✅ Nomes de variáveis e funções em português
- ✅ Tipos explícitos (sem `var`)
- ✅ Uso de `System.Text.Json`
- ✅ Validação de entradas antes do processamento
- ✅ Mensagens objetivas e diretas
- ✅ Organização modular com menu
- ✅ Sem bibliotecas externas
- ✅ Comentários essenciais

### **Critérios Funcionais:**
- ✅ Todos os 5 módulos implementados
- ✅ Cada módulo atende aos requisitos específicos
- ✅ Comportamento correto conforme especificação
- ✅ Interface amigável e intuitiva

---

## 🧪 Casos de Teste

### **Módulo 1 - Problema × Instância:**
| Item | Tipo Correto | Justificativa |
|------|--------------|---------------|
| "Ordenar uma lista de numeros" | P | Descrição genérica |
| "A lista [12, 7, 3, 9]" | I | Caso concreto |
| "O numero 37" | I | Exemplo específico |

### **Módulo 2 - Decidíveis:**
| Cadeia | L_fim_b | L_mult3_b |
|--------|---------|-----------|
| "abb" | SIM | SIM (2 b's não é múltiplo) |
| "bbb" | SIM | SIM (3 b's) |
| "" | NÃO | SIM (0 é múltiplo de 3) |
| "aaa" | NÃO | SIM (0 b's) |

### **Módulo 3 - Reconhecíveis:**
| Cadeia | Limite | Resultado Esperado |
|--------|--------|-------------------|
| "ab" | 10 | ACEITA |
| "bbb" | 50 | INDETERMINADO |
| "xab" | qualquer | Entrada inválida |

### **Módulo 4 - Detector Loop:**
| Modo | Comportamento | Resultado Esperado |
|------|---------------|-------------------|
| 1 | Termina em 10 passos | TERMINOU |
| 2 | Ciclo 0→1→2→3→4→0 | LOOP DETECTADO |
| 3 | Cresce indefinidamente | INDETERMINADO |

### **Módulo 5 - AFD:**
| AFD | Cadeia | Resultado | Estado Final |
|-----|--------|-----------|--------------|
| L_par_a | "aa" | ACEITA | Par |
| L_par_a | "aaa" | REJEITA | Impar |
| L_a_b* | "abbb" | ACEITA | AceitaB |
| L_a_b* | "ba" | REJEITA | Rejeita |

---

## 📚 Conceitos Teóricos Aplicados

### **Decidibilidade:**
- Uma linguagem é **decidível** quando existe algoritmo que sempre termina e responde corretamente
- Exemplos: L_fim_b, L_mult3_b

### **Reconhecibilidade:**
- Uma linguagem é **reconhecível** quando o algoritmo aceita se pertence, mas pode não terminar se não pertence
- Exemplo: L_contem_ab

### **Problema da Parada:**
- Não existe algoritmo que decida para todo programa se ele para ou não
- Detector de loop é apenas heurística, não solução geral

### **Autômatos Finitos:**
- Modelo computacional com estados finitos
- Transições determinísticas
- Aceitação por estado final

---

## 🎓 Aprendizados

Este projeto demonstra:

1. **Diferença entre problema e instância** na prática
2. **Comportamento de decisores** (sempre terminam)
3. **Limitações de reconhecedores** (podem não terminar)
4. **Impossibilidade de resolver** o Problema da Parada
5. **Funcionamento de AFDs** com execução visual

---

## 📄 Licença

Este projeto é parte de atividade acadêmica da disciplina de Computação Científica do UNIFESO.

---

## 📧 Contato

Para dúvidas sobre o projeto:
- **Email:** [seu-email@exemplo.com]
- **GitHub:** [@seu-usuario](https://github.com/seu-usuario)

---

## 🙏 Agradecimentos

- Prof. Dr. André Campos pela elaboração do material didático
- UNIFESO pelo suporte acadêmico
- Colegas de turma pelas discussões enriquecedoras

---

**Desenvolvido com 💻 e ☕ para a disciplina de Computação Científica - UNIFESO 2024**