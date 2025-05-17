# 🛒 API de Estoque para uma plataforma de E-Commerce


## 📄 Descrição

Esta API foi desenvolvida como parte de um sistema de gerenciamento de estoque para uma plataforma de e-commerce. 
A aplicação é construída com **.NET 8.0**, seguindo os princípios de **Clean Code** e **Clean Architecture** para garantir legibilidade, manutenção e escalabilidade do código.

Inclui também um conjunto de **testes automatizados**, integrados à solução, com foco em garantir a integridade e confiabilidade da aplicação.

---

## 🛠️ Tecnologias e Ferramentas

### 🧩 Backend
- **.NET 8.0**
- **Entity Framework Core**
- **AutoMapper**
- **SQL Server**
- **JWT Bearer**
- **Mercado Pago**

### 🧪 Testes Automatizados
- **xUnit**
- **Moq**
- **Faker.NetCore**
- **AutoMapper**

---

## 🚀 Funcionalidades Implementadas

- Cadastro, edição, exclusão e consulta de produtos
- Autenticação e autorização com JWT
- Controle de estoque com movimentações de entrada e saída
- Integração com o Mercado Pago para pagamentos
- Testes unitários cobrindo múltiplas camadas (Controllers, Services, Repositories, AutoMapper)

---

## 📂 Estrutura da Solução
Api/
│-- Controllers/
│-- Data/
│-- DTOs/
│-- Helpers/
│-- Migrations/
│-- Models/
│-- Repositories/
│-- Services/

ApiTests/
│-- Controllers/
│-- Repositories/
│-- Services/

---

## 🧪 Escopo dos Testes

1. **Controllers**: Validação das entradas e respostas.
2. **Services**: Regras de negócio com `Moq`.
3. **Repositórios**: Persistência no banco de dados.
4. **AutoMapper**: Validação dos mapeamentos DTO <-> Model.

---

## 🔁 Integração Contínua

Este repositório possui integração contínua com **GitHub Actions**, que realiza:

- Build do projeto
- Execução dos testes unitários

---

## 🌟 Conceitos Principais

- Princípios **SOLID**
- Injeção de dependência
- Testes integrados à solução
- Arquitetura extensível e testável
