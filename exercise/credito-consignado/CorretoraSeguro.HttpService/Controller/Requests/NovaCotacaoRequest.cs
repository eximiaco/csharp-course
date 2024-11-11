using System;
using System.Collections.Generic;

public record NovaCotacaoRequest(
    VeiculoRequest Veiculo,
    ProprietarioRequest Proprietario,
    CondutorRequest Condutor,
    List<string> Coberturas
);

public record VeiculoRequest(
    string Marca, 
    string Modelo, 
    int Ano
);

public record ProprietarioRequest(
    string Cpf, 
    string Nome, 
    DateTime DataNascimento, 
    EnderecoRequest Residencia
);

public record CondutorRequest(
    string Cpf, 
    DateTime DataNascimento, 
    EnderecoRequest Residencia
);

public record EnderecoRequest(
    string Cep, 
    string Cidade, 
    string UF
); 