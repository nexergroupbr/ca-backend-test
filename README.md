# BillingApi

BillingApi é uma API de faturamento desenvolvida em C# e .NET Core.

## Tecnologias Utilizadas

- .NET Core 3.1
- Entity Framework Core
- Swagger para documentação de API
- Injeção de Dependência
- SQL Server (ou qualquer outro banco de dados compatível com EF Core)

## Pré-requisitos

Antes de começar, certifique-se de ter o seguinte instalado:

- .NET Core SDK
- Visual Studio ou Visual Studio Code
- SQL Server ou outro banco de dados compatível

## Instalação

Siga os passos abaixo para configurar e executar o projeto localmente:

1. Clone o repositório:
    ```sh
    git clone https://github.com/edermargotti/BillingApi.git
    cd BillingApi
    ```

2. Restaure as dependências do projeto:
    ```sh
    dotnet restore
    ```

3. Compile o projeto:
    ```sh
    dotnet build
    ```

4. Configure a string de conexão com o banco de dados no arquivo `appsettings.json`:
    ```json
    "ConnectionStrings": {
      "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=BillingApiDb;Trusted_Connection=True;MultipleActiveResultSets=true"
    }
    ```

5. Aplique as migrações do Entity Framework Core para criar o banco de dados:
    ```sh
    dotnet ef database update
    ```

## Uso

Para executar a API localmente, utilize o comando:
```sh
dotnet run

## Autenticação

Para acessar os endpoints protegidos da API, é necessário obter um token JWT. Utilize o endpoint de autenticação com as seguintes credenciais:

    Login: "usuarioteste"
    Senha: "1"
    
    Endpoint de autenticação:
    
    POST /api/authenticate
    {
      "username": "usuarioteste",
      "password": "1"
    }
    
O token JWT recebido deve ser incluído no cabeçalho Authorization para acessar os demais endpoints:

    Authorization: Bearer <seu_token_jwt>