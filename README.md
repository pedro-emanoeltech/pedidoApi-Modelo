# Pedidos.Api

API para gerenciamento de pedidos com autenticação JWT.

## Funcionalidades

- CRUD de pedidos e usuários.
- Autenticação via JWT.
- Cancelamento de pedidos com regras de negócio (não é possível cancelar pedidos pagos).
- Cálculo automático do valor total do pedido baseado nos itens.
- Endpoints para consultar pedidos por status ou por ID.

## Estrutura do Projeto

Pedidos.Api/
│
├─ Pedidos.Api/ → API principal
│ ├─ Controller/ → Controllers (Pedidos, Usuários, Autenticação)
│ ├─ Configuracao/ → Configurações e DI
│ └─ Program.cs
│
├─ Pedidos.App/ → Camada de aplicação (serviços e DTOs)
├─ Pedidos.Domain/ → Entidades e regras de negócio (Pedido, ItemPedido, Usuario)
├─ Pedidos.Infra/ → Contexto do EF Core e mapeamentos
└─ Pedidos.Testes/ → Testes unitários xUnit

## Configuração Inicial

### 1. Clone o repositório:

 
git clone <URL_DO_REPOSITORIO>
cd Pedidos.Api


### Configure a string de conexão no appsettings.json:
```
"ConnectionStrings": {
    "BasePostgreSQL": "Host=localhost;Database=PedidosDb;Username=postgres;Password=senha"
}
 ```
###  Rode as migrations para criar o banco:
```
dotnet ef database update --project Pedidos.Infra --startup-project Pedidos.Api
 ```
### Usuário padrão
Ao iniciar a aplicação, será criado automaticamente um usuário administrador:
```
Nome: Administrador
Email: admin@exemplo.com
Senha: Admin@123
 ```
Você pode usar este usuário para autenticar e manipular a API, ou criar novos usuários via endpoint /api/usuarios.


### Autenticação JWT
Faça login usando o endpoint:
```
POST /api/autenticacao/login
{
    "email": "admin@exemplo.com",
    "senha": "Admin@123"
}
 ```
O retorno será um token JWT, que deve ser enviado no header Authorization:
```
Authorization: Bearer <TOKEN>
Endpoints Principais
 ```
###  Pedidos
```
POST /api/pedidos → Criar novo pedido
GET /api/pedidos → Listar pedidos (opcional por status)
GET /api/pedidos/{id} → Obter pedido por ID
PUT /api/pedidos/{id}/cancelar → Cancelar pedido (respeita regras de status)
 ```
###  Usuários
```
POST /api/usuarios → Criar novo usuário
GET /api/usuarios/{id} → Buscar usuário por ID
 ```
### Testes
O projeto Pedidos.Testes contém testes unitários das entidades (Pedido e ItemPedido) e regras de negócio.
Execute os testes com:
```
dotnet test
 ```
