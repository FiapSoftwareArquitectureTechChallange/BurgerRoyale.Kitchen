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

# Kubernetes Deployment Scripts

Este repositório contém scripts para implantar recursos no Kubernetes usando o Docker Desktop.
Os scripts estão disponíveis tanto para ambientes Windows (PowerShell) quanto para ambientes Unix-like (Bash).

## Pré-requisitos

- Docker Desktop instalado e configurado.
- `kubectl` instalado e configurado.
- Acesso ao PowerShell (para usuários Windows) ou a um terminal Unix-like (como Git Bash no Windows, ou terminal padrão em sistemas Linux ou macOS).

## Instruções de Execução

### PowerShell Script (`deploy_kubernetes_poweshell.ps1`)

1. **Abra o PowerShell**.
2. **Navegue até o diretório do script**:
   ```powershell
   cd Kubernetes
   ```
3. **Execute o script com a política de execução `Bypass`**:
   ```powershell
   PowerShell -ExecutionPolicy Bypass -File .\deploy_kubernetes_poweshell.ps1
   ```

### Bash Script (`deploy_kubernetes_bash.sh`)

1. **Abra o terminal Unix-like** (Git Bash no Windows, terminal padrão em Linux/MacOS).
2. **Navegue até o diretório do script**:
   ```bash
   cd /Kubernetes
   ```
3. **Torne o script executável (se necessário)**:
   ```bash
   chmod +x deploy_kubernetes_bash.sh
   ```
4. **Execute o script**:
   ```bash
   ./deploy_kubernetes_bash.sh
   ```

## Notas Adicionais

- Para usuários do PowerShell: Execute scripts com a política `Bypass` apenas quando tiver certeza da segurança e origem do script.
- Para usuários do Bash: Certifique-se de que o `kubectl` esteja acessível no seu PATH e que os caminhos dos arquivos estejam corretamente formatados para o ambiente Unix-like.
