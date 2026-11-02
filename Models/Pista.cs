namespace fiap_nuclear_case.Models;

public class Pista
{
    public int Id { get; set; }
    public int CasoId { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Conteudo { get; set; } = string.Empty;
    public int Ordem { get; set; }

    public Pista() { }

    public Pista(int id, int casoId, string titulo, string conteudo, int ordem)
    {
        ValidarAtributos(casoId, titulo, conteudo, ordem);

        Id = id;
        CasoId = casoId;
        Titulo = titulo.Trim();
        Conteudo = conteudo.Trim();
        Ordem = ordem;
    }

    public static void ValidarAtributos(int casoId, string titulo, string conteudo, int ordem)
    {
        if (casoId <= 0)
            throw new ArgumentException("O ID do Caso deve ser um número inteiro maior que zero.");

        if (string.IsNullOrWhiteSpace(titulo) || titulo.Trim().Length < 3)
            throw new ArgumentException("O título da pista é obrigatório e deve ter pelo menos 3 caracteres.");

        if (string.IsNullOrWhiteSpace(conteudo) || conteudo.Trim().Length < 10)
            throw new ArgumentException("O conteúdo da pista deve ter pelo menos 10 caracteres para oferecer contexto educativo.");

        if (ordem <= 0)
            throw new ArgumentException("A ordem da pista na investigação deve ser maior que zero.");
    }

    public override string ToString()
    {
        return $"[ID: {Id}] Caso #{CasoId} | {Ordem}ª Pista: {Titulo}\n   Evidência: {Conteudo}";
    }
}