using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace API.Dtos;

[JsonSerializable(typeof(int))]
[JsonSerializable(typeof(string))]
[JsonSerializable(typeof(DateTime))]
[JsonSerializable(typeof(ExtratoOut))]
[JsonSerializable(typeof(TransacaoIn))]
[JsonSerializable(typeof(TransacaoOut))]
[JsonSerializable(typeof(ProblemDetails))]
[JsonSerializable(typeof(ExtratoSaldoOut))]
[JsonSerializable(typeof(ExtratoTransacaoOut))]
[JsonSerializable(typeof(List<ExtratoTransacaoOut>))]
public partial class RinhaJsonSerializerContext : JsonSerializerContext { }
