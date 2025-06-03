# 📦 API de Gerenciamento de Reservas de Hotel
## 📖 Descrição**

Este projeto consiste em uma API RESTful desenvolvida em .NET 8 utilizando o Minimal API para o gerenciamento de reservas de hotel. A aplicação permite o cadastro, consulta, atualização e remoção de reservas, bem como o gerenciamento de detalhes como data de entrada, data de saída, número do quarto, valor da diária e identificação do hóspede.
## 🛠️ Tecnologias Utilizadas

    .NET 8

    C#

    Entity Framework Core (com migrations)

    SQLite (ou outro banco relacional)

    Swagger para documentação

    Minimal APIs para simplicidade e produtividade

    Dependency Injection nativo do ASP.NET Core
## 🚀 Funcionalidades
- Cadastrar novas reservas
- Listar todas as reservas
- Buscar reserva por ID
- Atualizar informações de uma reserva existente
- Remover uma reserva
- Validação automática de entrada de dados
- Documentação interativa com Swagger

## 📦 Instalação e Configuração
✅ Pré-requisitos

    .NET SDK 8.0+

    SQLite ou outro banco compatível

    Visual Studio Code ou Visual Studio 2022+ recomendado

✅ Clone o repositório
```
- git clone https://github.com/seu-usuario/ApiHotel.git
- cd ApiHotel
```
✅ Restaure os pacotes
```
dotnet restore
```
✅ Execute as migrações (se aplicável)
```
dotnet ef database update
```
Obs: Certifique-se que a string de conexão está corretamente configurada no appsettings.json ou diretamente no DbContext.
✅ Executando a aplicação
```
dotnet run
```
A aplicação estará disponível em:

    HTTP: http://localhost:5000

    HTTPS: https://localhost:5001

(As portas podem variar conforme o launchSettings.json ou argumentos de execução.)

## 🧭 Endpoints Disponíveis
➡️ POST /reservas

Cria uma nova reserva.

Body (JSON):

```
{
  "nomeCliente": "Ruan Pereira",
  "numeroQuarto": 100,
  "tipoId": 2,
  "entrada": "2025-06-03T14:27:26.158Z",
  "saida": "2025-06-03T14:27:26.158Z"
}
```
➡️ GET /reservas

```
[
  {
    "id": 1,
    "nomeCliente": "Ana Paula",
    "numeroQuarto": 4,
    "tipo": "Deluxe",
    "price": 350,
    "entrada": "2025-06-15T14:00:00",
    "saida": "2025-06-18T12:00:00"
  },
  {
    "id": 2,
    "nomeCliente": "Pedro Paulo",
    "numeroQuarto": 10,
    "tipo": "Suite Presidencial",
    "price": 1000,
    "entrada": "2025-06-15T14:00:00",
    "saida": "2025-06-18T12:00:00"
  },
  {
    "id": 3,
    "nomeCliente": "Mell Fontinele",
    "numeroQuarto": 14,
    "tipo": "Suite Presidencial",
    "price": 1000,
    "entrada": "2025-06-15T14:10:00",
    "saida": "2025-06-20T12:23:00"
  },
```
➡️ GET /reservas/{id}
```
[
  {
    "id": 1,
    "nomeCliente": "Ana Paula",
    "numeroQuarto": 4,
    "tipo": "Deluxe",
    "price": 350,
    "entrada": "2025-06-15T14:00:00",
    "saida": "2025-06-18T12:00:00"
  }
```
➡️ PUT /reservas/{id}
```
{
  "nomeCliente": "string",
  "numeroQuarto": 0,
  "price": 0,
  "entrada": "2025-06-03T14:56:14.569Z",
  "saida": "2025-06-03T14:56:14.569Z"
}
```
➡️ DELETE /reservas/{id}
Remove uma reserva.

## 📄 Documentação da API com Swagger

Após executar o projeto, acesse:

- `http://localhost:5000/swagger`
- `https://localhost:5001/swagger`

**Como configurar o Swagger:**

```csharp
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

app.UseSwagger();
app.UseSwaggerUI();
```

---

## 🔧 Configuração de Portas

A configuração de portas pode ser alterada no arquivo:

```
Properties/launchSettings.json
```

Por exemplo:

```json
"applicationUrl": "https://localhost:5001;http://localhost:5000"
```



## 📝 Considerações

- O projeto utiliza **Minimal APIs** para tornar a aplicação mais leve e direta.
- O **Entity Framework Core** é utilizado para o mapeamento objeto-relacional (ORM).
- Todas as entidades são mapeadas via `DbSet`.
- A aplicação segue o padrão **DTO** para entrada e saída de dados, garantindo segurança e encapsulamento.
- O **Swagger** é integrado para facilitar o desenvolvimento e testes.



## 🧑‍💻 Autor

**Ruan Pereira**  
[ruanpereira@alu.ufc.br](mailto:ruanpereira@alu.ufc.br)  
Engenharia de Computação - UFC  



## 🤝 Contribuições

Contribuições são bem-vindas!  

1. Faça um fork.  
2. Crie uma branch (`git checkout -b feature/nova-feature`).  
3. Commit suas alterações (`git commit -m 'Adiciona nova feature'`).  
4. Push para a branch (`git push origin feature/nova-feature`).  
5. Abra um **Pull Request**.



## 🏆 Licença

Este projeto está licenciado sob os termos da **MIT License**. Consulte o arquivo `LICENSE` para mais informações.
