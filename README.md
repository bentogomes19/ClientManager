### 🧑‍💼 ClientManager

Um sistema de **administração de clientes** desenvolvido em **C# Web API** com **arquitetura em camadas**, utilizando **Entity Framework Core** e **SQL Server** rodando em **Docker**.  

O projeto foi criado como parte de um exercício prático de **Programação Web**, com foco em boas práticas de back-end, responsividade no front-end e uso de validações HTML5.

---

## 🚀 Tecnologias utilizadas
- **C# .NET 8 Web API**
- **Entity Framework Core** (Code-First + Migrations)
- **SQL Server** (Docker Container)
- **Arquitetura em camadas** (`Controllers`, `DTOs`, `Models`, `Infrastructure`, `Repositories`, `Services`)
- **HTML5 + CSS3 + Bootstrap** (front-end responsivo)
- **jQuery** para interações simples no front
- **Docker Engine** para orquestrar o banco de dados

---

## ⚙️ Funcionalidades
### 📝 Cadastro de Clientes
- [x] Inserir, listar, editar e excluir clientes
- [x] Validações com HTML5 (`required`, `maxlength`, `type`, `min`, `max`)
- [x] Regras de negócio:
  - Nome: até 150 caracteres
  - CPF: somente dígitos, até 10 caracteres, único e válido
  - Data de nascimento: obrigatória e ≤ data atual
  - Data de cadastro: preenchida automaticamente (somente leitura)
  - Renda familiar: opcional, valor mínimo 0

### 📋 Listagem de Clientes
- [x] Pesquisa por nome
- [x] Exibição da **renda familiar** em um **badge customizado**:
  - Classe A → R$ ≤ 980 → fundo vermelho
  - Classe B → R$ 980,01 a R$ 2500 → fundo amarelo
  - Classe C → R$ > 2500 → fundo verde
- [x] Formatação monetária (`R$`, sem decimais, separador de milhar)

### 📊 Relatórios
- [x] Quantidade de clientes maiores de 18 anos com renda acima da renda média
- [x] Quantidade de clientes Classe A, B e C
- [x] Filtros de período: **Hoje, Semana, Mês**
- [x] Exibição em **cards responsivos**

---

## 📂 Estrutura do Projeto