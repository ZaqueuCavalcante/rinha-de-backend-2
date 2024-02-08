namespace API.Dtos;

public class TransacaoOut
{
    /// <summary>
    /// Limite do cliente crebitado.
    /// </summary>
    public int Limite { get; set; }

    /// <summary>
    /// Novo saldo do cliente após a conclusão da transação.
    /// </summary>
    public int Saldo { get; set; }
}
