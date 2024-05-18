# API de Preparação de Pedidos
O objetivo desta API é gerenciar a preparação dos pedidos recebidos pelo sistema.

## Funcionalidades
1. Recebe pedidos via fila SQS.
2. Salva os pedidos no banco de dados MongoDB.
3. Permite atualizar o status dos pedidos.

### OrdersController

- **GET api/Order**: Retorna uma lista de pedidos.
- **POST /api/Order/{id:Guid}/update**: Atualiza o status de um pedido.

### RequestPreparationController (somente para simular um pedido chegando)
- **POST /api/RequestPreparation**: Solicita a preparação de um pedido.
