# Sprint4Dotnet - ML

## Integrantes
- Fernando Paparelli Aracena rm551022
- Filipe de Oliveira Mendes rm98959
- Miron Gonçalves Martins rm551801
- Victor Luca do Nascimento Queiroz rm551886
- Vinicius Pedro de Souza rm550907

## Implementação da API

### Tecnologias Utilizadas
- **ASP.NET Core Web API:** Framework para desenvolvimento da API.
- **Banco de Dados Oracle:** Para operações CRUD (Create, Read, Update, Delete).
- **Swagger/OpenAPI:** Documentação interativa dos endpoints.
- **Padrão de Criação:** Utilização do padrão Singleton para o gerenciador de configurações.
- **Machine Learning:** Implementação de um modelo de previsão de produtos utilizando o ML.NET.

### Arquitetura da API

A arquitetura adotada é **monolítica**. Esta escolha foi feita devido à simplicidade e ao escopo atual da aplicação, que é suficiente para um sistema centralizado:

- **Simplicidade:** Toda a aplicação é gerida como uma única unidade, facilitando a configuração e manutenção.
- **Desempenho Interno:** A comunicação entre componentes é mais eficiente pois ocorre dentro de um único processo.
- **Facilidade de Manutenção:** Ideal para o tamanho atual do projeto e a equipe envolvida.

Essa abordagem é adequada para projetos de menor escala ou fases iniciais, onde a simplicidade e a eficiência são priorizadas.

### Padrão de Criação

Foi utilizado o padrão **Singleton** para o gerenciador de configurações. Este padrão assegura que haja uma única instância da configuração, gerenciando a configuração da aplicação de maneira centralizada e eficiente.

### Machine Learning

Implementamos um modelo de previsão de produtos utilizando o ML.NET. A API carrega um modelo treinado a partir de um arquivo zip e, caso o modelo não exista, inicia um processo de treinamento utilizando dados armazenados em um arquivo CSV. O modelo prevê o nome do produto com base em suas características (categoria e preço).

- **Dados de Treinamento:** Os dados de produtos são carregados a partir de um arquivo CSV. 
- **Pipeline de Treinamento:** O pipeline inclui transformação de dados, codificação categórica e treinamento de um classificador.
- **Previsões:** Através de um endpoint específico, é possível enviar dados de um novo produto e receber uma previsão do nome do produto com base no modelo treinado.

### Diferenças entre as Arquiteturas

- **Monolítica:** Toda a aplicação é desenvolvida e executada como uma unidade coesa. A comunicação entre os diferentes módulos é direta e rápida.
- **Microservices:** Em contraste, essa abordagem divide a aplicação em serviços independentes, o que não é necessário para o tamanho e escopo atual do projeto.

### Testes

- **Testes Unitários:** Valida a lógica dos endpoints CRUD.
- **Testes de Integração:** Verifica a comunicação com o banco de dados Oracle.

### Endpoints CRUD

A API oferece endpoints para os recursos **Clientes**, **Lojas** e **Produtos**. Os métodos disponíveis são:

- **GET:** Recupera recursos.
- **POST:** Cria novos recursos.
- **PUT:** Atualiza recursos existentes.
- **DELETE:** Exclui recursos.

### Exemplos de Endpoints

#### Clientes

- **GET** `/api/Clientes`
  - **Descrição:** Retorna uma lista de clientes.
  - **Exemplo de Resposta:**
    ```json
    [
      {
        "id": 1,
        "nome": "João da Silva",
        "dataNascimento": "1985-10-23T00:00:00Z",
        "cpf": "12345678900",
        "telefone": "11987654321",
        "endereco": "Rua Exemplo, 123",
        "email": "joao@example.com"
      }
    ]
    ```

- **POST** `/api/Clientes`
  - **Descrição:** Adiciona um novo cliente.
  - **Exemplo de Requisição:**
    ```json
    {
      "nome": "Maria Oliveira",
      "dataNascimento": "1990-05-12T00:00:00Z",
      "cpf": "98765432100",
      "telefone": "11912345678",
      "endereco": "Avenida Teste, 456",
      "email": "maria@example.com"
    }
    ```

- **GET** `/api/Clientes/{id}`
  - **Descrição:** Retorna um cliente pelo ID.
  - **Exemplo de Resposta:**
    ```json
    {
      "id": 2,
      "nome": "Carlos Pereira",
      "dataNascimento": "1978-11-15T00:00:00Z",
      "cpf": "12345678901",
      "telefone": "11923456789",
      "endereco": "Rua Amostra, 789",
      "email": "carlos@example.com"
    }
    ```

- **PUT** `/api/Clientes/{id}`
  - **Descrição:** Atualiza as informações de um cliente.
  - **Exemplo de Requisição:**
    ```json
    {
      "nome": "Carlos Pereira",
      "dataNascimento": "1978-11-15T00:00:00Z",
      "cpf": "12345678901",
      "telefone": "11923456789",
      "endereco": "Rua Atualizada, 789",
      "email": "carlos.novo@example.com"
    }
    ```

- **DELETE** `/api/Clientes/{id}`
  - **Descrição:** Remove um cliente pelo ID.
  - **Exemplo de Resposta:** Não há corpo na resposta.

#### Lojas

- **GET** `/api/Lojas`
  - **Descrição:** Retorna uma lista de lojas.
  - **Exemplo de Resposta:**
    ```json
    [
      {
        "id": 1,
        "nome": "Loja Exemplo",
        "cnpj": "12345678000195"
      }
    ]
    ```

- **POST** `/api/Lojas`
  - **Descrição:** Adiciona uma nova loja.
  - **Exemplo de Requisição:**
    ```json
    {
      "id": "1",
      "nome": "Phamais",
      "cnpj": "98765432000198"
    }
    ```

- **GET** `/api/Lojas/{id}`
  - **Descrição:** Retorna uma loja pelo ID.
  - **Exemplo de Resposta:**
    ```json
    {
      "id": 2,
      "nome": "Loja Atualizada",
      "cnpj": "12345678000195"
    }
    ```

- **PUT** `/api/Lojas/{id}`
  - **Descrição:** Atualiza as informações de uma loja.
  - **Exemplo de Requisição:**
    ```json
    {
      "nome": "Loja Atualizada",
      "cnpj": "12345678000195"
    }
    ```

- **DELETE** `/api/Lojas/{id}`
  - **Descrição:** Remove uma loja pelo ID.
  - **Exemplo de Resposta:** Não há corpo na resposta.

#### Produtos

- **GET** `/api/Produtos`
  - **Descrição:** Retorna uma lista de produtos.
  - **Exemplo de Resposta:**
    ```json
    [
      {
        "id": 1,
        "nomeProduto": "Advil",
        "categoria": "Remedios",
        "preco": 13.99
      }
    ]
    ```

- **POST** `/api/Produtos`
  - **Descrição:** Adiciona um novo produto.
  - **Exemplo de Requisição:**
    ```json
    {
      "id": 1,
      "nomeProduto": "Advil",
      "categoria": "Remedios",
      "preco": 13.99
    }
    ```

- **GET** `/api/Produtos/{id}`
  - **Descrição:** Retorna um produto pelo ID.
  - **Exemplo de Resposta:**
    ```json
    {
      "id": 2,
      "nomeProduto": "Produto Atualizado",
      "categoria": "Categoria Atualizada",
      "preco": 79.99
    }
    ```

- **PUT** `/api/Produtos/{id}`
  - **Descrição:** Atualiza as informações de um produto.
  - **Exemplo de Requisição:**
    ```json
    {
      "nomeProduto": "Produto Atualizado",
      "categoria": "Categoria Atualizada",
      "preco": 79.99
    }
    ```

- **DELETE** `/api/Produtos/{id}`
  - **Descrição:** Remove um produto pelo ID.
  - **Exemplo de Resposta:** Não há corpo na resposta.

### Códigos de Resposta HTTP

| Código | Descrição |
|--------|-----------|
| 200    | Requisição bem-sucedida |
| 400    | Requisição malformada |
| 404    | Recurso não encontrado |
| 500    | Erro interno do servidor |

---
