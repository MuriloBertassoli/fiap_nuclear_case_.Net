using System.Text.Encodings.Web;
using System.Text.Json;
using fiap_nuclear_case.Models;

namespace fiap_nuclear_case.Services;

public class PistaService
{
    private readonly string _caminhoArquivo;
    private List<Pista> _pistas = [];

    private static readonly JsonSerializerOptions _jsonOptions = new()
    {
        WriteIndented = true,
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
    };

    public PistaService()
    {
        string diretorioBase = AppContext.BaseDirectory;
        DirectoryInfo? dirProjeto = Directory.GetParent(diretorioBase)?.Parent?.Parent?.Parent;
        _caminhoArquivo = dirProjeto != null
            ? Path.Combine(dirProjeto.FullName, "pistas.json")
            : "pistas.json";

        CarregarDados();
    }

    public string ObterCaminhoArquivo() => _caminhoArquivo;

    public void CadastrarPista(int casoId, string titulo, string conteudo, int ordem)
    {
        Pista.ValidarAtributos(casoId, titulo, conteudo, ordem);

        bool ordemOcupada = _pistas.Any(p => p.CasoId == casoId && p.Ordem == ordem);
        if (ordemOcupada)
        {
            throw new InvalidOperationException(
                $"Regra de Negócio violada: O Caso #{casoId} já possui uma pista cadastrada na {ordem}ª posição.");
        }

        bool tituloDuplicado = _pistas.Any(p =>
            p.CasoId == casoId &&
            p.Titulo.Equals(titulo.Trim(), StringComparison.OrdinalIgnoreCase));

        if (tituloDuplicado)
        {
            throw new InvalidOperationException(
                $"Regra de Negócio violada: Já existe uma pista com o título '{titulo.Trim()}' no Caso #{casoId}.");
        }

        int novoId = _pistas.Count > 0 ? _pistas.Max(p => p.Id) + 1 : 1;
        var novaPista = new Pista(novoId, casoId, titulo, conteudo, ordem);

        _pistas.Add(novaPista);
        SalvarDados();
    }

    public List<Pista> ListarTodas()
    {
        return _pistas
            .OrderBy(p => p.CasoId)
            .ThenBy(p => p.Ordem)
            .ToList();
    }

    public List<Pista> BuscarPorCasoId(int casoId)
    {
        return _pistas
            .Where(p => p.CasoId == casoId)
            .OrderBy(p => p.Ordem)
            .ToList();
    }

    public List<Pista> BuscarPorTermo(string termo)
    {
        if (string.IsNullOrWhiteSpace(termo))
            return [];

        return _pistas
            .Where(p => p.Titulo.Contains(termo.Trim(), StringComparison.OrdinalIgnoreCase) ||
                        p.Conteudo.Contains(termo.Trim(), StringComparison.OrdinalIgnoreCase))
            .OrderBy(p => p.CasoId)
            .ThenBy(p => p.Ordem)
            .ToList();
    }

    private void CarregarDados()
    {
        try
        {
            if (File.Exists(_caminhoArquivo))
            {
                string json = File.ReadAllText(_caminhoArquivo);
                _pistas = JsonSerializer.Deserialize<List<Pista>>(json, _jsonOptions) ?? [];
            }
            else
            {
                InicializarDadosDemonstracao();
                SalvarDados();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Aviso] Falha ao ler o ficheiro JSON ({ex.Message}). Iniciando lista padrão.");
            _pistas = [];
        }
    }

    private void SalvarDados()
    {
        string json = JsonSerializer.Serialize(_pistas, _jsonOptions);
        File.WriteAllText(_caminhoArquivo, json);
    }

    private void InicializarDadosDemonstracao()
    {
        _pistas =
        [
            new Pista(1, 1, "Selo Radura na embalagem",
                "O alimento passou por feixes de elétrons ou raios gama para eliminar microrganismos, sem entrar em contato com material radioativo.", 1),
            new Pista(2, 1, "Análise de laboratório pós-irradiação",
                "Assim como um raio-X médico não torna o paciente radioativo, a energia atravessa o alimento sem deixar resíduos nucleares.", 2),
            new Pista(3, 2, "Meia-vida curta do radiofármaco",
                "O Tecnécio-99m utilizado em exames de imagem possui meia-vida de apenas 6 horas, decaindo rapidamente após o diagnóstico.", 1),
            new Pista(4, 3, "Balanço de emissões de carbono",
                "A geração termonuclear opera em base contínua (24/7) com baixíssima emissão de gases de efeito estufa durante a operação.", 1)
        ];
    }
}