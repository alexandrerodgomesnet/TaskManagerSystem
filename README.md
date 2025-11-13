# 🗂️ TaskManagerSystem

Sistema de gerenciamento de tarefas.

## 🚀 Tecnologias Utilizadas

- **.NET 9**
- **Entity Framework Core (InMemory)**
- **AutoMapper**
- **FluentValidation**
- **FluentAssertions**, **xUnit**, **Moq** e **Bogus** (para testes)
- **Minimal API**
- **Swagger / OpenAPI** (para documentação)

---

## 🧱 Estrutura do Projeto

```
TaskManagerSystem/
│
├── TaskManagerSystem.sln
│
├── src/
│ ├── TaskManagerSystem.Api/ # Camada de apresentação (endpoints)
│ ├── TaskManagerSystem.Application/ # Regras de negócio e DTOs
│ ├── TaskManagerSystem.Core/ # Entidades e contratos
│ └── TaskManagerSystem.Infrastructure/ # Persistência e repositórios (EF Core)
│
└── tests/
└── TaskManagerSystem.Tests/ # Testes unitários e de integração
```

---

## 🧠 Funcionalidades Implementadas
# 📌 Gerenciamento de Tarefas
```
Método	        Endpoint	                    Descrição
POST	        /api/tasks	                    Cria uma nova tarefa
GET	            /api/tasks/{userId}	            Lista todas as tarefas de um usuário
PUT	            /api/tasks/{id}/complete	    Marca uma tarefa como concluída
DELETE	        /api/tasks/{id}	                Remove uma tarefa existente
```

---

## ⚙️ Como Executar o Projeto

### ✅ Pré-requisitos

- [.NET SDK 9.0](https://dotnet.microsoft.com/download/dotnet/9.0) (ou superior)
- IDE recomendada: **Visual Studio 2022** ou **VS Code**

---

### 🔧 Passos para rodar o projeto

1. **Clone o repositório:**

git clone https://github.com/alexandrerodgomesnet/TaskManagerSystem.git

cd TaskManagerSystem


2. **Restaure as dependências:**

dotnet restore


3. **Compile a solução:**

dotnet build


4. **Execute a aplicação:**

dotnet run --project src/TaskManagerSystem.Api


5. **Acesse no navegador:**

http://localhost:5075/swagger


## 🧪 Testes Automatizados

**O projeto inclui testes unitários e de integração, garantindo a qualidade e estabilidade da API.**


## ▶️ Rodar os testes

dotnet test



## 👨‍💻 Autor

**Alexandre Rodrigues Gomes**
```
Desenvolvedor .NET | C# | APIs REST | Clean Architecture

📧 [alexandrerodgomes@hotmail.com]
🌐 www.linkedin.com/in/alexandre-rodrigues-gomes-55a366161
```
---