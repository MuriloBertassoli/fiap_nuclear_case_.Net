Nuclear Case — Gerenciador de Pistas Investigativas (.NET Console PoC)

Entrega da 1ª Sprint (Sprint Flash) da disciplina Advanced Business Development with .NET

Curso: Análise e Desenvolvimento de Sistemas — FIAP

Challenge 2026: Marinha do Brasil / CTMSP

👥 Grupo e Integrantes

Nome do Projeto: Nuclear Case ("Descubra o que você não vê")

Integrantes:

Murilo Massini — RM: 567383

Giovanni Sacristan — RM: 567548

Gabriel Deotti — RM: 567258

💡 Descrição Resumida da Solução

O Nuclear Case é uma solução educativa gamificada desenvolvida com o objetivo de aproximar a sociedade da tecnologia nuclear, desmistificando preconceitos históricos e apresentando suas aplicações benéficas e pacíficas na medicina (radiofármacos e diagnósticos), na agricultura/alimentação (irradiação de alimentos e controle de pragas), na indústria e na geração de energia limpa com baixa emissão de carbono.

Em vez de operar como um quiz genérico, o Nuclear Case adota a dinâmica de investigação científica: o usuário seleciona um caso misterioso do cotidiano, analisa pistas e evidências técnicas progressivas, formula sua hipótese e, por fim, recebe a elucidação científica detalhada com fontes confiáveis.

🛠️ O que foi desenvolvido nesta Sprint (.NET)

Para esta Sprint Flash, o escopo concentrou-se na criação de uma prova de conceito funcional em formato Console Application, atuando de maneira estritamente complementar à API desenvolvida na disciplina de Java Advanced:

Entidade de Domínio (Pista): Modelagem da classe com atributos coerentes ao contexto investigativo (Id, CasoId, Titulo, Conteudo e Ordem).

Validações de Atributos: Verificação rigorosa contra entradas inválidas (IDs positivos, obrigatoriedade de campos, título com no mínimo 3 caracteres e conteúdo explicativo com no mínimo 10 caracteres).

Regra de Negócio: Implementação da garantia de consistência da trilha pedagógica:

Bloqueio de duplicidade de ordem (não permite duas pistas ocupando a mesma posição investigativa dentro de um mesmo caso).

Bloqueio de títulos repetidos dentro do mesmo caso investigativo.

Operações Disponíveis:

Cadastro interativo de novas evidências com validação em tempo de execução.

Listagem completa ordenada por caso e sequência pedagógica.

Consulta filtrada por CasoId.

Consulta por busca textual (palavra-chave em título ou conteúdo).

Tratamento de Exceções (try/catch): Tratamento estruturado para ArgumentException, InvalidOperationException e FormatException, assegurando que erros de preenchimento ou violações de regra exibam alertas amigáveis sem derrubar a aplicação.

Persistência de Dados em JSON: Implementação com System.Text.Json, gravando e lendo do arquivo pistas.json para garantir que os dados cadastrados se mantenham preservados entre diferentes execuções. A aplicação já inicializa com 4 pistas demonstrativas cobrindo os 3 casos investigativos da proposta.

🏗️ Arquitetura do Projeto

A solução foi estruturada no Visual Studio seguindo a separação de responsabilidades em camadas simples:

fiap_nuclear_case/
├── fiap_nuclear_case.sln            # Arquivo de Solução do Visual Studio
├── README.md                        # Documentação completa da entrega
├── .gitignore                       # Ignora pastas de compilação (bin/, obj/, .vs/)
└── fiap_nuclear_case/
    ├── fiap_nuclear_case.csproj     # Configuração do projeto (.NET Console)
    ├── Models/
    │   └── Pista.cs                 # Entidade do domínio e validações de atributos
    ├── Services/
    │   └── PistaService.cs          # Regras de negócio, consultas LINQ e persistência JSON
    ├── Program.cs                   # Menu de interação via Console e tratamento de erros
    └── pistas.json                  # Base de dados persistida em formato JSON


▶️ Instruções de Execução

Opção 1 — Pelo Visual Studio (Recomendado)

Clone este repositório público:

git clone <URL_DO_SEU_REPOSITORIO>


Dê um duplo clique no arquivo fiap_nuclear_case.sln para abrir o projeto no Visual Studio.

Certifique-se de que a configuração está definida como Debug e o projeto de inicialização é fiap_nuclear_case.

Pressione F5 (ou clique no botão verde ▶ fiap_nuclear_case) para compilar e iniciar o console interativo.

Opção 2 — Via Terminal (.NET CLI)

Abra o terminal na pasta do projeto onde reside o arquivo .csproj:

cd fiap_nuclear_case/fiap_nuclear_case


Execute o comando de compilação e inicialização:

dotnet run


(Nota: Na primeira execução, o arquivo pistas.json será automaticamente verificado ou gerado na pasta do projeto com os dados iniciais de demonstração).

🎯 Como o .NET se aplica à solução do grupo

Dentro da arquitetura global do projeto Nuclear Case, a plataforma .NET desempenha o papel fundamental de módulo de inteligência de conteúdo, curadoria investigativa e validação de regras de domínio. Enquanto a API em Java Advanced assume o gerenciamento central do catálogo de casos, o ecossistema .NET atua como ferramenta especializada na estruturação lógica das evidências científicas que compõem cada mistério. Por meio de algoritmos de ordenação e checagens de integridade em C#, a aplicação garante que cada caso possua uma trilha de descoberta coesa, sem pistas duplicadas ou conteúdos incoerentes antes da exposição aos jogadores no aplicativo móvel. Nas fases seguintes de desenvolvimento, esta base em .NET evoluirá para uma camada de validação científica automatizada e processamento analítico do fluxo de aprendizagem dos usuários.
