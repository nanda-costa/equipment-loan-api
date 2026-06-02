# Sistema de Gestão de Empréstimos de Equipamentos 🛠️

Este projeto consiste em uma API REST desenvolvida em **.NET** para o controle de empréstimos, devoluções, reservas e manutenção de equipamentos em instituições, empresas ou laboratórios. 

[cite_start]O sistema foi estruturado seguindo uma **Arquitetura em Camadas** para garantir a separação clara de responsabilidades, utilizando **PostgreSQL** para persistência de dados, **Entity Framework Core** como ORM e autenticação via **JWT Bearer Token**[cite: 5].

---

## 🏗️ Estrutura da Solution e Arquitetura

[cite_start]A solução está organizada de forma modular através de múltiplos projetos dedicados, respeitando a separação estrita de responsabilidades[cite: 28, 35]:

* **`EquipmentLoan.API`**: Camada de entrada (HTTP). [cite_start]Contém Controllers, configurações de Autenticação JWT, Middlewares de tratamento e documentação Swagger.
* **`EquipmentLoan.Application`**: Camada de aplicação. [cite_start]Contém os Casos de Uso, Serviços de negócio, interfaces, validações e DTOs (evitando exposição direta das entidades).
* **`EquipmentLoan.Domain`**: O coração do sistema. [cite_start]Contém as Entidades de negócio, Enums e regras fundamentais, totalmente livre de dependências de infraestrutura.
* **`EquipmentLoan.Infrastructure`**: Camada de acesso a dados. [cite_start]Contém o `DbContext`, Repositories e os arquivos de Migrations do EF Core.
* [cite_start]**`EquipmentLoan.Exceptions`**: Camada dedicada para exceções customizadas e padronização dos fluxos de erro da API[cite: 35, 52].

---

## 📊 Modelagem do Domínio (Entidades Obrigatórias)

[cite_start]O sistema conta com as seguintes entidades principais para o funcionamento das regras de negócio[cite: 27, 82]:

1. **User**: Usuário do sistema (com perfis diferenciados de `Admin` ou `User`).
2. **Equipment**: Equipamento físico disponível para movimentação.
3. **Category**: Categoria do equipamento (Ex: Notebooks, Projetores, Câmeras).
4. **Loan**: Registro detalhado e histórico de empréstimos efetuados.
5. **Reservation**: Agendamento futuro de equipamentos para uso planejado.
6. **Maintenance**: Registro e controle de indisponibilidade para manutenção preventiva ou corretiva.

### Relacionamentos Principais (EF Core)
* [cite_start]`User (1) : (N) Loan` -> Um usuário pode realizar múltiplos empréstimos[cite: 67].
* [cite_start]`Equipment (1) : (N) Loan` -> Um equipamento mantém seu histórico completo de empréstimos[cite: 67].
* [cite_start]`Category (1) : (N) Equipment` -> Uma categoria agrupa vários equipamentos[cite: 67].
* [cite_start]`Equipment (1) : (N) Maintenance` -> Um equipamento pode registrar várias manutenções corretivas[cite: 67].

---

## 🔒 Regras de Negócio, Segurança & Exceptions

### Mecanismo de Segurança de Senhas
* [cite_start]Em conformidade com as diretrizes de segurança, **nenhuma senha é armazenada em texto puro**[cite: 58].
* Utilizamos o algoritmo **BCrypt** (`BCrypt.Net-Next`) para aplicar uma função de Hash assimétrica com *salt* embutido de forma automática no momento do registro do usuário, garantindo a proteção criptográfica dos dados na tabela `Users`.

### Tratamento Global de Erros (Exceptions Middleware)
* [cite_start]A API possui um `ExceptionHandlingMiddleware` acoplado na raiz do pipeline HTTP.
* [cite_start]Exceções de regras de negócio (como `BadHttpRequestException` ou `BusinessException`) são interceptadas antes de quebrar a requisição[cite: 52].
* [cite_start]Isso impede a exposição de *Stack Traces* (Erro 500) e padroniza as respostas de erro em formatos JSON limpos com os status HTTP corretos (`400 Bad Request` ou `404 Not Found`)[cite: 52].

### Perfis de Acesso (RBAC)
* [cite_start]**Usuário Comum**: Registrar-se (`POST /api/auth/register`), autenticar-se (`POST /api/auth/login`), solicitar empréstimos de itens disponíveis e visualizar seu histórico.
* [cite_start]**Administrador**: Cadastro e gerenciamento total de equipamentos, categorias, aprovação/recusa de empréstimos, e envio/finalização de itens para manutenção (rotas protegidas por `[Authorize]`).

---

## 🛠️ Como Executar o Projeto Localmente

### Pré-requisitos
* [.NET 8.0 SDK](https://dotnet.microsoft.com/download) instalado.
* [cite_start][Docker Desktop](https://www.docker.com/products/docker-desktop/) rodando (para o banco de dados).

### Passo 1: Clonar o repositório e acessar a branch de desenvolvimento
```bash
git clone <url-do-repositorio>
git checkout develop