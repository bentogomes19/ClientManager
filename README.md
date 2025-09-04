### 🧑‍💼 ClientManager

Um sistema de **administração de clientes** desenvolvido em **C# Web API** com **arquitetura em camadas**, utilizando **Entity Framework Core** e **SQL Server** rodando em **Docker**.

---

## 🚀 Tecnologias utilizadas

- **C# .NET 8 Web API**
- **Entity Framework Core** (Code-First + Migrations)
- **SQL Server** (Docker Container)
- **Arquitetura em camadas** (`Controllers`, `DTOs`, `Models`, `Infrastructure`, `Repositories`, `Services`)

## Pré-requisitos

Verifique em sua máquina:

- [Docker Desktop](https://www.docker.com/products/docker-desktop/)
- [.NET 8 SDK](https://builds.dotnet.microsoft.com/dotnet/Sdk/8.0.413/dotnet-sdk-8.0.413-win-x64.exe)
- [Git](https://git-scm.com/downloads/win)

---

### Instalação e Configuração

#### 1. Clone o projeto:

```bash
git clone https://github.com/bentogomes19/ClientManager.git
cd ClientManager
```

#### 2. Configurar SQL Server via Docker

```bash
# Windows PowerShell ou cmd
docker run -d --name sqlserver -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=Your_Strong_Password123" -p 1433:1433 -v mssql_data:/var/opt/mssql mcr.microsoft.com/mssql/server:2022-latest
```

- Substitura Your_Strong_Password123 para uma senha de sua preferência.

  Verifique se o container está rodando:

  ```bash
  docker ps
  ```

#### 3. Configurar a conexão com o banco (`appsettings.json`):

Edite o arquivo appsettings.json da API:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost,1433;Database=ClientManagerDB;User Id=sa;Password=Your_Strong_Password123;TrustServerCertificate=True;MultipleActiveResultSets=True"
  }
}
```

##### 4. Instalação do .NET 8.0 SDK

- Execute o instalador do [.net](<(https://builds.dotnet.microsoft.com/dotnet/Sdk/8.0.413/dotnet-sdk-8.0.413-win-x64.exe)>).

- Certifique-se de que o instalador funcionou corretamente.
  ```bash
  dotnet --version
  ```

#### 5. Criar banco e rodar a API

Instale a ferramenta dotnet-ef (se ainda não tiver):

```bash
dotnet tool install --global dotnet-ef
```

Crie a migration inicial e atualize o banco:

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

Rode a API:

```bash
dotnet run --urls http://localhost:5156
```

## 📑Documentação da API Swagger

Para verificar as rotas da api consulte o swagger
http://localhost:5156/swagger

## 🧰 Troubleshooting

- **Erro 10061 (conexão recusada):**

  - Confirme que o contêiner está **up**: `docker ps`.
  - Verifique se a porta **1433** não está ocupada.
  - Use `Server=localhost,1433` (vírgula), não `localhost:1433` na connection string do **SqlClient**.
  - Garanta `TrustServerCertificate=True` em dev.

- **Senha inválida ao iniciar o contêiner:**

  - O `MSSQL_SA_PASSWORD` precisa ter **mínimo 8 caracteres**, com **maiúsculas, minúsculas, número e símbolo**.

- **`dotnet-ef` não encontrado:**

  - Instale a CLI: `dotnet tool install --global dotnet-ef` e reinicie o terminal.

- **Swagger não abre:**

  - Confirme a URL usada em `dotnet run --urls` e acesse `http://localhost:5000/swagger`.

- **CORS no frontend:**
  - Habilite CORS na API para `http://localhost:5173`.

## 📝 Observações

- Este projeto segue arquitetura em camadas, separando responsabilidades de forma clara.
- Ideal para estudos, protótipos e pequenos sistemas de gestão.
- Você pode conectar qualquer frontend ao backend, como React, Angular ou Blazor.
