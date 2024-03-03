using System.Linq;

namespace API.Dtos;

public class TransacaoIn
{
    /// <summary>
    /// Representa os centavos da transação.
    /// </summary>
    public int Valor { get; set; }

    /// <summary>
    /// c -> crédito | d -> débito
    /// </summary>
    public string? Tipo { get; set; }

    /// <summary>
    /// String de 1 a 10 caractéres.
    /// </summary>
    public string? Descricao { get; set; }

    private readonly static string[] TIPOS = ["c", "d"];
    public bool EhValido()
    {
        return TIPOS.Contains(Tipo)
            && !string.IsNullOrEmpty(Descricao)
            && Descricao.Length <= 10
            && Valor > 0;
    }
}
