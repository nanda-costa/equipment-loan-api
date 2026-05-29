# Sistema de Gestão de Empréstimos de Equipamentos 🛠️

Este projeto consiste em uma API REST desenvolvida em **.NET** para o controle de empréstimos, devoluções, reservas e manutenção de equipamentos em instituições, empresas ou laboratórios. 

O sistema foi estruturado seguindo uma **Arquitetura em Camadas** para garantir a separação clara de responsabilidades, utilizando **PostgreSQL** para persistência de dados, **Entity Framework Core** como ORM e autenticação via **JWT Bearer Token**.

---

## 🏗️ Estrutura da Solution e Arquitetura

A solução está organizada de forma modular através de múltiplos projetos dedicados:

* **`EquipmentLoan.API`**: Camada de entrada (HTTP). Contém Controllers, configurações de Autenticação JWT, Middlewares de tratamento e documentação Swagger.
* **`EquipmentLoan.Application`**: Camada de aplicação. Contém os Casos de Uso, Serviços de negócio, interfaces, validações e DTOs (evitando exposição direta das entidades).
* **`EquipmentLoan.Domain`**: O coração do sistema. Contém as Entidades de negócio, Enums e regras fundamentais, totalmente livre de dependências de infraestrutura.
* **`EquipmentLoan.Infrastructure`**: Camada de acesso a dados. Contém o `DbContext`, Repositories e os arquivos de Migrations do EF Core.
* **`EquipmentLoan.Exceptions`**: Camada dedicada para exceções customizadas e padronização dos fluxos de erro da API.

---

## 📊 Modelagem do Domínio (Entidades Obrigatórias)

O sistema conta com as seguintes entidades principais para o funcionamento das regras de negócio:

1. **User**: Usuário do sistema (com perfis diferenciados de `Admin` ou `User`).
2. **Equipment**: Equipamento físico disponível para movimentação.
3. **Category**: Categoria do equipamento (Ex: Notebooks, Projetores, Câmeras).
4. **Loan**: Registro detalhado e histórico de empréstimos efetuados.
5. **Reservation**: Agendamento futuro de equipamentos para uso planejado.
6. **Maintenance**: Registro e controle de indisponibilidade para manutenção preventiva ou corretiva.

### Relacionamentos Principais (EF Core)
* `User (1) : (N) Loan` -> Um usuário pode realizar múltiplos empréstimos.
* `Equipment (1) : (N) Loan` -> Um equipamento mantém seu histórico completo de empréstimos.
* `Category (1) : (N) Equipment` -> Uma categoria agrupa vários equipamentos.
* `Equipment (1) : (N) Maintenance` -> Um equipamento pode registrar várias manutenções corretivas.

---

## 🔒 Regras de Negócio & Níveis de Acesso

### Perfis de Acesso (RBAC)
* **Usuário Comum**: Registrar-se, autenticar-se, solicitar empréstimos de itens disponíveis e visualizar seu histórico pessoal.
* **Administrador**: Cadastro e gerenciamento de equipamentos, aprovação/recusa de empréstimos, encerramento de contratos e envio de itens para manutenção.

---

## 🛠️ Como Executar o Projeto Localmente

### Passo 1: Clonar o repositório e acessar a branch de desenvolvimento
```bash
git clone <url-do-repositorio>
git checkout develop
