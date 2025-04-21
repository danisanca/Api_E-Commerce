# API de Estoque para um Projeto E-Commerce

## 📄 Descrição
Esta API foi desenvolvida como parte de um projeto de gerenciamento de estoque para um site de e-commerce. A aplicação utiliza o framework .NET 8.0 e segue princípios de **Clean Code** para garantir legibilidade, manutenção e organização do código.

A modelagem das entidades foi baseada em um esquema cuidadosamente planejado, levando em consideração os comportamentos esperados no sistema.

## 🛠️ Tecnologias e Ferramentas

### **Principais Ferramentas e Extensões**
- **Entity Framework**: Para mapeamento objeto-relacional (ORM).
- **AutoMapper**: Para mapeamento automático entre objetos.
- **SQL Server**: Banco de dados relacional.
- **JWT Bearer**: Para autenticação segura com tokens.

### **Ferramentas Utilizadas nos Testes**
- **xUnit**: Framework de testes.
- **AutoMapper**: Teste de mapeamentos.
- **Faker.NetCore**: Geração de dados fictícios para testes.
- **Moq**: Criação de mocks para simulação de dependências.

## 🧪 Testes Automatizados
Os testes foram desenvolvidos para validar as principais funcionalidades da API, garantindo confiabilidade e estabilidade. Confira o repositório dedicado aos testes:
[Repositório de Testes](https://github.com/danisanca/Api_E-Commerce_Tests)

## 🚀 Funcionalidades Implementadas
- Cadastro, edição, exclusão e consulta de produtos.
- Autenticação com JWT.
- Controle de estoque com movimentações de entrada e saída.
- ORM para gerenciamento das entidades e seus relacionamentos.
- Integração com Mercado Pago para pagamentos.

## 📂 Estrutura do Projeto
O projeto segue uma arquitetura organizada, focada em **Clean Architecture**:
src/ │-- Controllers/ │-- Data/ │-- DTOs/ │-- Helpers/ │-- Migrations/ │-- Models/ │-- Repositories/ │-- Service/ 

## 🌟 Diferenciais
- Segue os princípios de **SOLID** para design de software.
- Uso de boas práticas como injeção de dependência e segregação de responsabilidades.
- Foco em extensibilidade e manutenção.
