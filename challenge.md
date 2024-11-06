# Desafio: Simulação de Seguro Veicular

## Objetivo

Colocar em prática os conhecimentos adquiridos ao longo do curso, desenvolvendo uma aplicação que simula um processo de seguro veicular. A ideia é trabalhar com um cenário de negócio fictício, aplicando conceitos de ASP.NET Core, Workflow Core, Entity Framework, design patterns e boas práticas de arquitetura.

## Descrição do Desafio

O processo simulará uma solicitação de seguro para um veículo, composta por etapas de coleta de dados, processamento de informações complementares, cálculo de valores, aprovação e emissão de uma apólice.

## Funcionalidades e Regras de Negócio

### 1. Recebimento de Requisição

A aplicação deve receber uma requisição para a simulação de seguro com os dados do veículo e do condutor. Estes dados incluirão:

- **Veículo**: marca, modelo e ano
- **Proprietário**: cpf, nome, data nascimento, residência
- **Condutor**: cpf, data nascimento, residência.
- **Coberturas**: coberturas (roubo/furto, colisão, terceiros, proteção residencial) selecionadas pelo usuário

### 2. Processamento de Informações Complementares

Após receber os dados, o sistema deve consultar e complementar a simulação com informações adicionais, incluindo:

- **Tabela Fipe**: verificar o valor de mercado do veículo com base em uma API simulada ou existente
- **Historico acidentes**: consultar um histórico de acidentes do condutor com base em uma API simulada ou existente
- **Nível de Risco**: calcular um índice de risco com base na idade do condutor, histórico de direção e localidade.

#### Cálculo do Nível de Risco

O nível de risco é determinado com base em variáveis comuns em avaliações de seguros veiculares, como idade do condutor, histórico de direção e localidade de residência. Cada variável tem uma pontuação que aumenta ou reduz o nível de risco.

#### Variáveis e Pontuações

| Variável                | Critério                                         | Pontuação              |
|-------------------------|--------------------------------------------------|------------------------|
| **Idade do Condutor**   | Condutores mais jovens ou idosos têm maior risco | 18-25 anos: 15 pontos <br>26-40 anos: 5 pontos <br>41-60 anos: 3 pontos <br>Acima de 60 anos: 10 pontos |
| **Histórico de Acidentes** | Quantidade de acidentes nos últimos 3 anos     | Nenhum acidente: 0 pontos <br>1 acidente: 10 pontos <br>2 acidentes: 20 pontos <br>3 ou mais acidentes: 30 pontos |
| **Localidade de Residência** | Risco associado à região                    | Baixo risco: 5 pontos <br>Médio risco: 10 pontos <br>Alto risco: 20 pontos |

#### Classificação do Nível de Risco

| Pontuação Total         | Nível de Risco |
|-------------------------|----------------|
| 0 - 10 pontos           | 1 (Baixo)      |
| 11 - 25 pontos          | 2              |
| 26 - 40 pontos          | 3              |
| 41 - 55 pontos          | 4              |
| 56 pontos ou mais       | 5 (Alto)       |

##### Exemplo de Cálculo de Nível de Risco

Para um condutor com as seguintes características:
- Idade: 24 anos (15 pontos)
- Histórico de Acidentes: 1 acidente nos últimos 3 anos (10 pontos)
- Localidade de Residência: Alto risco (20 pontos)

**Pontuação Total** = 15 + 10 + 20 = **45 pontos**, correspondendo ao **Nível de Risco 4**.

### 3. Cálculo do Valor do Seguro

O cálculo do seguro deve ser feito com base nas informações complementares e nas coberturas escolhidas. A aplicação deve considerar:

- **Valor de Mercado (Tabela Fipe)**: o valor do veículo serve como base para algumas coberturas.
- **Nível de Risco**: o nível de risco influencia diretamente o custo das coberturas, aplicando um ajuste percentual sobre o custo base.

#### Coberturas Básicas e Custo Base

Cada cobertura tem um custo base que é aplicado sobre o valor de mercado do veículo:

| Cobertura              | Custo Base (%) sobre o Valor de Mercado |
|------------------------|-----------------------------------------|
| Roubo/Furto            | 3%                                      |
| Colisão                | 4%                                      |
| Terceiros              | 1.5%                                    |
| Proteção Residencial   | Taxa fixa de R$ 100                     |

#### Ajuste pelo Nível de Risco

O custo das coberturas é ajustado com base no nível de risco do condutor:

- Nível de Risco 1: sem ajuste (100% do custo base)
- Nível de Risco 2: +5% sobre o custo base
- Nível de Risco 3: +10% sobre o custo base
- Nível de Risco 4: +20% sobre o custo base
- Nível de Risco 5: +30% sobre o custo base

#### Exemplo de Cálculo

Para um veículo de R$ 50.000 e um condutor com Nível de Risco 3, o cálculo seria:

| Cobertura            | Custo Base | Ajuste Nível de Risco 3 (+10%) | Custo Ajustado |
|----------------------|------------|--------------------------------|----------------|
| Roubo/Furto          | R$ 1.500   | R$ 1.650                       | R$ 1.650       |
| Colisão              | R$ 2.000   | R$ 2.200                       | R$ 2.200       |
| Terceiros            | R$ 750     | R$ 825                         | R$ 825         |
| Proteção Residencial | R$ 100     | Sem ajuste                     | R$ 100         |

**Seguro Total** = R$ 1.650 + R$ 2.200 + R$ 825 + R$ 100 = **R$ 4.775**

### 4. Aprovação do Seguro

Após o cálculo, o sistema aguarda uma aprovação do usuário para a proposta. Que pode ser aprovada ou rejeitada via uma requisição HTTP.

### 5. Emissão da Apólice

Uma vez aprovada a simulação, o sistema deve emitir uma apólice fictícia com todas as informações relevantes sobre as coberturas, valor total, vigência, entre outros.

## Requisitos

1. Escolha do **design** e **arquitetura** é opção de cada aluno.
2. A aplicação deve ser desenvolvida em .NET 8.0, utilizando asp.net core
3. Escolha de banco de dados é livre.
4. Preferência para uso de EF Core no domínio. 
5. **Não é necessário** endppints para gerenciar (CRUD) dados de apoio como tabela de veículos, marcas, tabela de preços, cidades e etc.
6. **Não é necessário** user interfaces.
7. Testes de unidade e integração são obrigatórios.