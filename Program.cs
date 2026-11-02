using fiap_nuclear_case.Models;
using fiap_nuclear_case.Services;

Console.OutputEncoding = System.Text.Encoding.UTF8;

var service = new PistaService();
bool executando = true;

while (executando)
{
    Console.WriteLine("\n=======================================================");
    Console.WriteLine("   NUCLEAR CASE — GERENCIADOR DE PISTAS (.NET 10.0)    ");
    Console.WriteLine("=======================================================");
    Console.WriteLine("Casos de Referência:");
    Console.WriteLine("  [1] O alimento radioativo? | [2] O segredo do diagnóstico | [3] O futuro energético");
    Console.WriteLine("-------------------------------------------------------");
    Console.WriteLine("1 - Cadastrar nova pista investigativa");
    Console.WriteLine("2 - Listar todas as pistas cadastradas");
    Console.WriteLine("3 - Buscar pistas por ID do Caso");
    Console.WriteLine("4 - Buscar pistas por palavra-chave (Título/Conteúdo)");
    Console.WriteLine("0 - Sair");
    Console.Write("\nEscolha uma opção: ");

    string? opcao = Console.ReadLine();

    try
    {
        switch (opcao)
        {
            case "1":
                CadastrarPistaUI(service);
                break;
            case "2":
                ExibirPistas(service.ListarTodas(), "TODAS AS PISTAS CADASTRADAS");
                break;
            case "3":
                BuscarPorCasoUI(service);
                break;
            case "4":
                BuscarPorTermoUI(service);
                break;
            case "0":
                executando = false;
                Console.WriteLine($"\nEncerrando aplicação. Dados persistidos em:\n{service.ObterCaminhoArquivo()}");
                break;
            default:
                Console.WriteLine("\n[!] Opção inválida. Digite um número de 0 a 4.");
                break;
        }
    }
    catch (ArgumentException ex)
    {
        Console.WriteLine($"\n[Erro de Validação]: {ex.Message}");
    }
    catch (InvalidOperationException ex)
    {
        Console.WriteLine($"\n[Bloqueio de Regra de Negócio]: {ex.Message}");
    }
    catch (FormatException)
    {
        Console.WriteLine("\n[Erro de Entrada]: Digite apenas números inteiros válidos para campos numéricos.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"\n[Erro Inesperado]: Ocorreu um problema ({ex.Message}), mas a aplicação segue em execução.");
    }
}

static void CadastrarPistaUI(PistaService service)
{
    Console.WriteLine("\n--- CADASTRO DE NOVA PISTA ---");

    Console.Write("ID do Caso (ex: 1, 2 ou 3): ");
    int casoId = int.Parse(Console.ReadLine() ?? "0");

    Console.Write("Ordem de exibição da pista no caso (ex: 1, 2, 3): ");
    int ordem = int.Parse(Console.ReadLine() ?? "0");

    Console.Write("Título curto da pista: ");
    string titulo = Console.ReadLine() ?? string.Empty;

    Console.Write("Conteúdo/Evidência científica da pista: ");
    string conteudo = Console.ReadLine() ?? string.Empty;

    service.CadastrarPista(casoId, titulo, conteudo, ordem);
    Console.WriteLine("\n[Sucesso] Pista validada, cadastrada e salva em 'pistas.json'!");
}

static void BuscarPorCasoUI(PistaService service)
{
    Console.Write("\nDigite o ID do Caso para filtrar as pistas: ");
    int casoId = int.Parse(Console.ReadLine() ?? "0");

    var resultado = service.BuscarPorCasoId(casoId);
    ExibirPistas(resultado, $"PISTAS DO CASO #{casoId}");
}

static void BuscarPorTermoUI(PistaService service)
{
    Console.Write("\nDigite uma palavra-chave para buscar no título ou conteúdo: ");
    string termo = Console.ReadLine() ?? string.Empty;

    var resultado = service.BuscarPorTermo(termo);
    ExibirPistas(resultado, $"RESULTADOS PARA O TERMO '{termo}'");
}

static void ExibirPistas(List<Pista> pistas, string cabecalho)
{
    Console.WriteLine($"\n--- {cabecalho} ({pistas.Count} encontrada(s)) ---");
    if (pistas.Count == 0)
    {
        Console.WriteLine("Nenhum registo encontrado para este critério.");
        return;
    }

    foreach (var pista in pistas)
    {
        Console.WriteLine(pista);
        Console.WriteLine(new string('-', 55));
    }
}