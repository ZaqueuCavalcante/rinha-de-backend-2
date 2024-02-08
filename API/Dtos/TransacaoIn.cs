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
    public char Tipo { get; set; }

    /// <summary>
    /// String de 1 a 10 caractéres.
    /// </summary>
    public string Descricao { get; set; }
}
