using System;

namespace API.Domain;

public class Transacao
{
    public int Id { get; set; }
    public int ClienteId { get; set; }
    public int Valor { get; set; }
    public char Tipo { get; set; }
    public string Descricao { get; set; }
    public DateTime RealizadaEm { get; set; }
}
