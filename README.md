# Integrantes: <br>
Alissa Silva Cezero - RM552535 <br> Melissa Barbosa de Souza - RM552535 <br> Nicolas Paiffer do Carmo - RM554145

# FloodAlertAPI

API RESTful para monitoramento de enchentes, desenvolvida com .NET 6, ML.NET e RabbitMQ. O sistema visa prever enchentes com base em dados meteorológicos e níveis de água, oferecendo suporte a alertas em tempo real por mensageria.

---

## Visão Geral

FloodAlertAPI é um projeto acadêmico com o propósito de aplicar práticas modernas de desenvolvimento backend, incluindo:

- Arquitetura RESTful
- Integração com **Machine Learning (.zip)** para predição de enchentes
- Comunicação assíncrona via **RabbitMQ**
- Persistência com banco de dados (em memória ou Oracle)
- Documentação da API via **Swagger**
- Padrões de boas práticas e escalabilidade

---

## Tecnologias Utilizadas

| Ferramenta         | Descrição                                      |
|--------------------|------------------------------------------------|
| ASP.NET Core       | Framework Web para criação da API              |
| ML.NET             | Framework de Machine Learning da Microsoft    |
| RabbitMQ           | Broker de mensagens para comunicação assíncrona |
| Swagger / Swashbuckle | Documentação automática da API             |
| Entity Framework   | ORM para acesso ao banco de dados             |
| Banco em Memória   | Utilizado para testes e prototipação inicial   |

---

## Como Executar o Projeto

### Pré-requisitos:

- [.NET 6 SDK](https://dotnet.microsoft.com/en-us/download)
- [RabbitMQ Server](https://www.rabbitmq.com/download.html) instalado e em execução local (`localhost:5672`)

### Passos:

1. Clone o projeto:

```bash
git clone https://github.com/seu-usuario/FloodAlertAPI.git
cd FloodAlertAPI
```

2. Verifique se o `Model.zip` está no diretório `ML/`.

3. Execute o projeto:

```bash
dotnet run
```

4. Acesse a documentação Swagger:

```
https://localhost:5001/swagger
```

---

## Endpoints da API

### 🔹 `GET /api/usuario`

Lista todos os usuários cadastrados.

---

### 🔹 `POST /api/usuario`

Cadastra um novo usuário.

**Exemplo de JSON:**

```json
{
  "nome": "Maria",
  "email": "maria@email.com"
}
```

---

### 🔹 `POST /api/predicao`

Envia dados meteorológicos para prever enchente com ML.NET.

**Exemplo de JSON:**

```json
{
  "nivelAgua": 6.2,
  "precipitacao": 85.5,
  "umidade": 90,
  "enchente": true
}
```

> O campo `enchente` é necessário para o modelo validar internamente os dados.

**Resposta esperada:**

```json
{
  "predicao": true,
  "probabilidade": 0.9823
}
```

---

## Machine Learning com ML.NET

O modelo `Model.zip` foi treinado previamente com dados contendo:

- `nivelAgua`
- `precipitacao`
- `umidade`
- `enchente` (bool)

Na aplicação, ele é carregado pelo serviço `PredictService` e utilizado em `PredicaoController` para retornar a predição da ocorrência de enchente.

---

## RabbitMQ

O serviço `RabbitMQService` envia mensagens para a fila `flood_alert_queue` sempre que uma enchente é prevista.

### Já implementado:
- Conexão com RabbitMQ
- Declaração da fila
- Envio de mensagens com `BasicPublish`

---

## 👨‍💻 Desenvolvedores

| Nome                      | GitHub                                            |
|---------------------------|---------------------------------------------------|
| Alissa Silva Cezero      | [@lissCez](https://github.com/lissCez)             |
| Melissa Barbosa de Souza | [@melissabsouza](https://github.com/melissabsouza) |
| Nicolas Paiffer do Carmo | [@NPaiffer](https://github.com/NPaiffer)           |
